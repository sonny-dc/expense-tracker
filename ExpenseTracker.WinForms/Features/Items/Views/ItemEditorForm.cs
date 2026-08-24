using ExpenseTracker.WinForms.Features.Items.Models;

namespace ExpenseTracker.WinForms.Features.Items.Views;

public partial class ItemEditorForm : Form
{
    private readonly Item? _item;

    public ItemEditorForm()
    {
        InitializeComponent();
    }

    public ItemEditorForm(
        Item item)
    {
        ArgumentNullException.ThrowIfNull(item);

        InitializeComponent();

        _item = item;

        ConfigureEditMode();
    }

    public CreateItemRequest? CreateRequest { get; private set; }

    public UpdateItemRequest? UpdateRequest { get; private set; }

    private void ConfigureEditMode()
    {
        if (_item is null)
        {
            return;
        }

        Text = "Edit Item";
        titleLabel.Text = "Edit Item";
        descriptionLabel.Text =
            "Update the details of the selected item.";
        saveButton.Text = "Save Changes";

        nameTextBox.Text = _item.Name;
        codeTextBox.Text = _item.Code;
        brandTextBox.Text = _item.Brand;
        unitPriceNumericUpDown.Value = _item.UnitPrice;
    }

    private void saveButton_Click(
        object? sender,
        EventArgs e)
    {
        if (!ValidateInput())
        {
            return;
        }

        string name = nameTextBox.Text.Trim();
        string code = codeTextBox.Text.Trim();
        string brand = brandTextBox.Text.Trim();
        decimal unitPrice = unitPriceNumericUpDown.Value;

        if (_item is null)
        {
            CreateRequest = new CreateItemRequest
            {
                Name = name,
                Code = code,
                Brand = brand,
                UnitPrice = unitPrice
            };
        }
        else
        {
            UpdateRequest = new UpdateItemRequest
            {
                Name = name,
                Code = code,
                Brand = brand,
                UnitPrice = unitPrice
            };
        }

        DialogResult = DialogResult.OK;
        Close();
    }

    private bool ValidateInput()
    {
        errorProvider.Clear();

        bool isValid = true;

        if (string.IsNullOrWhiteSpace(nameTextBox.Text))
        {
            errorProvider.SetError(
                nameTextBox,
                "Name is required.");

            isValid = false;
        }

        if (string.IsNullOrWhiteSpace(codeTextBox.Text))
        {
            errorProvider.SetError(
                codeTextBox,
                "Code is required.");

            isValid = false;
        }

        if (string.IsNullOrWhiteSpace(brandTextBox.Text))
        {
            errorProvider.SetError(
                brandTextBox,
                "Brand is required.");

            isValid = false;
        }

        return isValid;
    }
}
