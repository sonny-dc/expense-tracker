using System.Globalization;

using ExpenseTracker.WinForms.Features.Expenses.Models;

using ItemModel =
    ExpenseTracker.WinForms.Features.Items.Models.Item;

namespace ExpenseTracker.WinForms.Features.Expenses.Views;

public partial class RecordExpenseForm : Form
{
    private readonly IReadOnlyList<ItemModel> _availableItems;

    private readonly Dictionary<
        int,
        SelectedExpenseItemControls> _selectedItems = [];

    public RecordExpenseForm(
        IReadOnlyList<ItemModel> availableItems)
    {
        ArgumentNullException.ThrowIfNull(
            availableItems);

        if (availableItems.Count == 0)
        {
            throw new ArgumentException(
                "At least one available item is required.",
                nameof(availableItems));
        }

        InitializeComponent();

        _availableItems = availableItems;

        selectedItemsFlowLayoutPanel.ClientSizeChanged +=
            selectedItemsFlowLayoutPanel_ClientSizeChanged;

        availableItemsFlowLayoutPanel.ClientSizeChanged +=
            availableItemsFlowLayoutPanel_ClientSizeChanged;

        DisplaySelectedItems();
        DisplayAvailableItems();
        UpdateEstimatedTotal();
    }

    public CreateExpenseRequest? ExpenseRequest
    {
        get;
        private set;
    }

    private void saveButton_Click(
        object? sender,
        EventArgs e)
    {
        if (!TryCreateRequest(
                out CreateExpenseRequest? request))
        {
            return;
        }

        ExpenseRequest = request;

        DialogResult = DialogResult.OK;
        Close();
    }

    private bool TryCreateRequest(
        out CreateExpenseRequest? request)
    {
        request = null;

        errorProvider.Clear();

        if (string.IsNullOrWhiteSpace(
                titleTextBox.Text))
        {
            errorProvider.SetError(
                titleTextBox,
                "Title is required.");

            titleTextBox.Focus();

            return false;
        }

        if (_selectedItems.Count == 0)
        {
            MessageBox.Show(
                this,
                "Select at least one item for the expense.",
                "No Expense Items",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            return false;
        }

        var expenseItems =
            new List<CreateExpenseItemRequest>(
                _selectedItems.Count);

        foreach (
            SelectedExpenseItemControls selectedItem
            in _selectedItems.Values)
        {
            decimal quantity =
                selectedItem.QuantityInput.Value;

            if (quantity <= 0)
            {
                MessageBox.Show(
                    this,
                    $"The quantity for \"{selectedItem.Item.Name}\" " +
                    "must be greater than zero.",
                    "Invalid Quantity",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                selectedItem.QuantityInput.Focus();

                return false;
            }

            expenseItems.Add(
                new CreateExpenseItemRequest
                {
                    ItemId = selectedItem.Item.ItemId,
                    Quantity = quantity
                });
        }

        string notes =
            notesTextBox.Text.Trim();

        request = new CreateExpenseRequest
        {
            Title = titleTextBox.Text.Trim(),
            Notes = string.IsNullOrWhiteSpace(notes)
                ? null
                : notes,
            Items = expenseItems
        };

        return true;
    }

    private void AddSelectedItem(
        ItemModel item)
    {
        if (_selectedItems.ContainsKey(item.ItemId))
        {
            return;
        }

        SelectedExpenseItemControls selectedItem =
            CreateSelectedItemControls(item);

        _selectedItems.Add(
            item.ItemId,
            selectedItem);

        DisplaySelectedItems();
        DisplayAvailableItems();
        UpdateEstimatedTotal();
    }

    private void RemoveSelectedItem(
        int itemId)
    {
        if (!_selectedItems.Remove(
                itemId,
                out SelectedExpenseItemControls? selectedItem))
        {
            return;
        }

        selectedItem.RowPanel.Dispose();

        DisplaySelectedItems();
        DisplayAvailableItems();
        UpdateEstimatedTotal();
    }

    private SelectedExpenseItemControls
        CreateSelectedItemControls(
            ItemModel item)
    {
        var rowPanel = new Panel
        {
            BackColor = Color.White,
            BorderStyle = BorderStyle.FixedSingle,
            Height = 76,
            Margin = new Padding(0, 0, 0, 8),
            Padding = new Padding(12),
            Width = GetFlowPanelContentWidth(
                selectedItemsFlowLayoutPanel)
        };

        var nameLabel = new Label
        {
            AutoEllipsis = true,
            Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Bold),
            Location = new Point(12, 9),
            Size = new Size(220, 25),
            Text = item.Name
        };

        var detailsLabel = new Label
        {
            AutoEllipsis = true,
            ForeColor = Color.DimGray,
            Location = new Point(12, 38),
            Size = new Size(250, 23),
            Text =
                $"{item.Code} | {item.Brand}"
        };

        var quantityLabel = new Label
        {
            Anchor =
                AnchorStyles.Top |
                AnchorStyles.Right,
            AutoSize = true,
            Location = new Point(
                rowPanel.Width - 500,
                11),
            Text = "Quantity"
        };

        var quantityInput = new NumericUpDown
        {
            Anchor =
                AnchorStyles.Top |
                AnchorStyles.Right,
            DecimalPlaces = 3,
            Increment = 0.001m,
            Minimum = 0.001m,
            Maximum = 999999999999999.999m,
            Location = new Point(
                rowPanel.Width - 500,
                35),
            Size = new Size(120, 27),
            TextAlign = HorizontalAlignment.Right,
            ThousandsSeparator = true,
            Value = 1m
        };

        var unitPriceLabel = new Label
        {
            Anchor =
                AnchorStyles.Top |
                AnchorStyles.Right,
            ForeColor = Color.DimGray,
            Location = new Point(
                rowPanel.Width - 362,
                11),
            Size = new Size(115, 23),
            Text =
                $"Price: {FormatPeso(item.UnitPrice)}"
        };

        var lineTotalLabel = new Label
        {
            Anchor =
                AnchorStyles.Top |
                AnchorStyles.Right,
            Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Bold),
            ForeColor = Color.Green,
            Location = new Point(
                rowPanel.Width - 362,
                37),
            Size = new Size(150, 25),
            Text = FormatPeso(item.UnitPrice)
        };

        var removeButton = new Button
        {
            Anchor =
                AnchorStyles.Top |
                AnchorStyles.Right,
            Cursor = Cursors.Hand,
            ForeColor = Color.Firebrick,
            Location = new Point(
                rowPanel.Width - 106,
                19),
            Size = new Size(92, 36),
            Text = "Remove",
            UseVisualStyleBackColor = true
        };

        var selectedItem =
            new SelectedExpenseItemControls(
                item: item,
                rowPanel: rowPanel,
                quantityInput: quantityInput,
                lineTotalLabel: lineTotalLabel);

        quantityInput.ValueChanged +=
            (_, _) =>
            {
                UpdateSelectedItemLineTotal(
                    selectedItem);

                UpdateEstimatedTotal();
            };

        removeButton.Click +=
            (_, _) =>
                RemoveSelectedItem(item.ItemId);

        rowPanel.Controls.Add(nameLabel);
        rowPanel.Controls.Add(detailsLabel);
        rowPanel.Controls.Add(quantityLabel);
        rowPanel.Controls.Add(quantityInput);
        rowPanel.Controls.Add(unitPriceLabel);
        rowPanel.Controls.Add(lineTotalLabel);
        rowPanel.Controls.Add(removeButton);

        return selectedItem;
    }

    private Control CreateAvailableItemRow(
        ItemModel item)
    {
        var rowPanel = new Panel
        {
            BackColor = Color.White,
            BorderStyle = BorderStyle.FixedSingle,
            Height = 68,
            Margin = new Padding(0, 0, 0, 8),
            Padding = new Padding(12),
            Width = GetFlowPanelContentWidth(
                availableItemsFlowLayoutPanel)
        };

        var nameLabel = new Label
        {
            AutoEllipsis = true,
            Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Bold),
            Location = new Point(12, 8),
            Size = new Size(
                Math.Max(rowPanel.Width - 350, 180),
                25),
            Text = item.Name
        };

        var detailsLabel = new Label
        {
            AutoEllipsis = true,
            ForeColor = Color.DimGray,
            Location = new Point(12, 35),
            Size = new Size(
                Math.Max(rowPanel.Width - 350, 180),
                23),
            Text =
                $"{item.Code} | {item.Brand}"
        };

        var priceLabel = new Label
        {
            Anchor =
                AnchorStyles.Top |
                AnchorStyles.Right,
            Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Bold),
            ForeColor = Color.Green,
            Location = new Point(
                rowPanel.Width - 230,
                21),
            Size = new Size(120, 25),
            Text = FormatPeso(item.UnitPrice),
            TextAlign =
                ContentAlignment.MiddleRight
        };

        var addButton = new Button
        {
            Anchor =
                AnchorStyles.Top |
                AnchorStyles.Right,
            BackColor = Color.Green,
            Cursor = Cursors.Hand,
            FlatStyle = FlatStyle.Flat,
            ForeColor = Color.White,
            Location = new Point(
                rowPanel.Width - 98,
                16),
            Size = new Size(84, 36),
            Text = "Add",
            UseVisualStyleBackColor = false
        };

        addButton.FlatAppearance.BorderSize = 0;

        addButton.Click +=
            (_, _) => AddSelectedItem(item);

        rowPanel.Controls.Add(nameLabel);
        rowPanel.Controls.Add(detailsLabel);
        rowPanel.Controls.Add(priceLabel);
        rowPanel.Controls.Add(addButton);

        return rowPanel;
    }

    private void DisplaySelectedItems()
    {
        selectedItemsFlowLayoutPanel.SuspendLayout();

        try
        {
            selectedItemsFlowLayoutPanel.Controls.Clear();

            if (_selectedItems.Count == 0)
            {
                selectedItemsFlowLayoutPanel.Controls.Add(
                    CreateEmptySelectedItemsLabel());

                return;
            }

            foreach (
                SelectedExpenseItemControls selectedItem
                in _selectedItems.Values)
            {
                selectedItem.RowPanel.Width =
                    GetFlowPanelContentWidth(
                        selectedItemsFlowLayoutPanel);

                selectedItemsFlowLayoutPanel.Controls.Add(
                    selectedItem.RowPanel);
            }
        }
        finally
        {
            selectedItemsFlowLayoutPanel.ResumeLayout();
        }
    }

    private void DisplayAvailableItems()
    {
        availableItemsFlowLayoutPanel.SuspendLayout();

        try
        {
            availableItemsFlowLayoutPanel.Controls.Clear();

            IReadOnlyList<ItemModel> unselectedItems =
                _availableItems
                    .Where(item =>
                        !_selectedItems.ContainsKey(
                            item.ItemId))
                    .OrderBy(item => item.Name)
                    .ThenBy(item => item.ItemId)
                    .ToList();

            if (unselectedItems.Count == 0)
            {
                availableItemsFlowLayoutPanel.Controls.Add(
                    CreateAllItemsSelectedLabel());

                return;
            }

            foreach (ItemModel item in unselectedItems)
            {
                availableItemsFlowLayoutPanel.Controls.Add(
                    CreateAvailableItemRow(item));
            }
        }
        finally
        {
            availableItemsFlowLayoutPanel.ResumeLayout();
        }
    }

    private Control CreateEmptySelectedItemsLabel()
    {
        return new Label
        {
            ForeColor = Color.DimGray,
            Height = 46,
            Margin = new Padding(12),
            Text =
                "No items selected. Choose an item from the list below.",
            TextAlign = ContentAlignment.MiddleLeft,
            Width = Math.Max(
                GetFlowPanelContentWidth(
                    selectedItemsFlowLayoutPanel) - 24,
                250)
        };
    }

    private Control CreateAllItemsSelectedLabel()
    {
        return new Label
        {
            ForeColor = Color.DimGray,
            Height = 46,
            Margin = new Padding(12),
            Text =
                "All available items have been added to this expense.",
            TextAlign = ContentAlignment.MiddleLeft,
            Width = Math.Max(
                GetFlowPanelContentWidth(
                    availableItemsFlowLayoutPanel) - 24,
                250)
        };
    }

    private void UpdateSelectedItemLineTotal(
        SelectedExpenseItemControls selectedItem)
    {
        decimal estimatedLineTotal =
            Math.Round(
                selectedItem.Item.UnitPrice *
                selectedItem.QuantityInput.Value,
                decimals: 2,
                mode: MidpointRounding.AwayFromZero);

        selectedItem.LineTotalLabel.Text =
            FormatPeso(estimatedLineTotal);
    }

    private void UpdateEstimatedTotal()
    {
        decimal estimatedTotal = 0m;

        foreach (
            SelectedExpenseItemControls selectedItem
            in _selectedItems.Values)
        {
            decimal estimatedLineTotal =
                Math.Round(
                    selectedItem.Item.UnitPrice *
                    selectedItem.QuantityInput.Value,
                    decimals: 2,
                    mode: MidpointRounding.AwayFromZero);

            estimatedTotal += estimatedLineTotal;
        }

        estimatedTotal = Math.Round(
            estimatedTotal,
            decimals: 2,
            mode: MidpointRounding.AwayFromZero);

        estimatedTotalLabel.Text =
            $"Estimated total: {FormatPeso(estimatedTotal)}";

        saveButton.Enabled =
            _selectedItems.Count > 0;
    }

    private void selectedItemsFlowLayoutPanel_ClientSizeChanged(
        object? sender,
        EventArgs e)
    {
        UpdateFlowPanelRowWidths(
            selectedItemsFlowLayoutPanel);
    }

    private void availableItemsFlowLayoutPanel_ClientSizeChanged(
        object? sender,
        EventArgs e)
    {
        UpdateFlowPanelRowWidths(
            availableItemsFlowLayoutPanel);
    }

    private static void UpdateFlowPanelRowWidths(
        FlowLayoutPanel flowLayoutPanel)
    {
        int rowWidth =
            GetFlowPanelContentWidth(flowLayoutPanel);

        foreach (Control control
            in flowLayoutPanel.Controls)
        {
            control.Width = rowWidth;
        }
    }

    private static int GetFlowPanelContentWidth(
        FlowLayoutPanel flowLayoutPanel)
    {
        int width =
            flowLayoutPanel.ClientSize.Width -
            flowLayoutPanel.Padding.Horizontal -
            SystemInformation.VerticalScrollBarWidth -
            4;

        return Math.Max(width, 320);
    }

    private static string FormatPeso(
        decimal amount)
    {
        return amount.ToString(
            "C2",
            CultureInfo.GetCultureInfo("en-PH"));
    }

    private sealed class SelectedExpenseItemControls
    {
        public SelectedExpenseItemControls(
            ItemModel item,
            Panel rowPanel,
            NumericUpDown quantityInput,
            Label lineTotalLabel)
        {
            Item = item;
            RowPanel = rowPanel;
            QuantityInput = quantityInput;
            LineTotalLabel = lineTotalLabel;
        }

        public ItemModel Item { get; }

        public Panel RowPanel { get; }

        public NumericUpDown QuantityInput { get; }

        public Label LineTotalLabel { get; }
    }
}
