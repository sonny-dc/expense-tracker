using ExpenseTracker.WinForms.Features.Expenses.Models;

using ExpenseTracker.WinForms.Infrastructure.Presentation;

namespace ExpenseTracker.WinForms.Features.Expenses.Views;

public partial class ExpenseDetailsForm : Form
{
    private readonly ExpenseResult _expense;

    private readonly DisplayFormatter _displayFormatter;
    private readonly bool _showRecordedSuccess;

    public ExpenseDetailsForm(
        ExpenseResult expense,
        DisplayFormatter displayFormatter,
        bool showRecordedSuccess = false)
    {
        ArgumentNullException.ThrowIfNull(expense);
        ArgumentNullException.ThrowIfNull(displayFormatter);

        InitializeComponent();

        historicalItemsDataGridView.DataBindingComplete +=
            historicalItemsDataGridView_DataBindingComplete;

        _expense = expense;
        _displayFormatter = displayFormatter;
        _showRecordedSuccess = showRecordedSuccess;

        ConfigurePresentationMode();
        DisplayExpense();
    }

    private void DisplayExpense()
    {
        ExpenseEntry entry =
            _expense.ExpenseEntry;

        Text =
            _showRecordedSuccess
            ? "Expense Recorded"
            : $"Expense #{entry.ExpenseEntryId}";

        titleLabel.Text =
            entry.Title;

        expenseNumberLabel.Text =
            $"Expense #{entry.ExpenseEntryId}";

        dateLabel.Text =
            _displayFormatter.FormatUtcDateTimeLong(
                entry.ExpenseDateTime);

        totalValueLabel.Text =
            _displayFormatter.FormatCurrency(entry.TotalCost);

        itemCountValueLabel.Text =
            GetItemCountText(
                _expense.ExpenseItems.Count);

        int deletedSourceCount =
            _expense.ExpenseItems.Count(
                item => item.ItemId is null);

        if (_showRecordedSuccess)
        {
            sourceStatusValueLabel.Text =
                "Saved";

            sourceStatusValueLabel.ForeColor =
                Color.Green;
        }
        else
        {
            sourceStatusValueLabel.Text =
                GetSourceStatusText(
                    deletedSourceCount);

            sourceStatusValueLabel.ForeColor =
                deletedSourceCount == 0
                    ? Color.Green
                    : Color.DarkOrange;
        }

        notesTextBox.Text =
            string.IsNullOrWhiteSpace(entry.Notes)
                ? "No notes were provided."
                : entry.Notes.Trim();

        historicalItemsDataGridView.DataSource =
            _expense.ExpenseItems.ToList();

        historicalItemsDataGridView.ClearSelection();
        historicalItemsDataGridView.CurrentCell = null;
    }

    private void historicalItemsDataGridView_DataBindingComplete(
        object? sender,
        DataGridViewBindingCompleteEventArgs e)
    {
        historicalItemsDataGridView.ClearSelection();
        historicalItemsDataGridView.CurrentCell = null;
    }

    private void ConfigurePresentationMode()
    {
        if (!_showRecordedSuccess)
        {
            return;
        }

        headerPanel.BackColor =
            Color.FromArgb(232, 245, 233);

        sourceStatusCaptionLabel.Text =
            "RECORD STATUS";

        sourceStatusValueLabel.Text =
            "Saved";

        sourceStatusValueLabel.ForeColor =
            Color.Green;

        historicalItemsLabel.Text =
            "Backend-Confirmed Item Details";

        historicalNoticeLabel.Text =
            "The API calculated and saved these quantities, snapshots, and totals.";

        snapshotNoticeLabel.Text =
            "Final totals and historical item snapshots were confirmed by the API.";

        closeButton.Text =
            "Done";
    }

    private static string GetSourceStatusText(
        int deletedSourceCount)
    {
        return deletedSourceCount switch
        {
            0 =>
                "All available",

            1 =>
                "1 source deleted",

            _ =>
                $"{deletedSourceCount} sources deleted"
        };
    }

    private static string GetItemCountText(
        int itemCount)
    {
        return itemCount == 1
            ? "1 item"
            : $"{itemCount} items";
    }

    private void historicalItemsDataGridView_CellFormatting(
        object? sender,
        DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex < 0)
        {
            return;
        }

        DataGridViewRow row =
            historicalItemsDataGridView.Rows[
                e.RowIndex];

        if (row.DataBoundItem is not ExpenseItem item)
        {
            return;
        }

        if (e.ColumnIndex == quantityColumn.Index)
        {
            e.Value =
                item.Quantity.ToString("N3");

            e.FormattingApplied = true;

            return;
        }

        if (e.ColumnIndex == unitPriceColumn.Index)
        {
            e.Value =
                _displayFormatter.FormatCurrency(item.UnitPriceSnapshot);

            e.FormattingApplied = true;

            return;
        }

        if (e.ColumnIndex == lineTotalColumn.Index)
        {
            e.Value =
                _displayFormatter.FormatCurrency(item.LineTotal);

            e.FormattingApplied = true;

            return;
        }

        if (e.ColumnIndex == sourceStatusColumn.Index)
        {
            bool sourceDeleted =
                item.ItemId is null;

            Color statusColor =
                sourceDeleted
                    ? Color.Firebrick
                    : Color.Green;

            e.Value =
                sourceDeleted
                    ? "Deleted"
                    : "Available";

            e.CellStyle.ForeColor =
                statusColor;

            e.CellStyle.SelectionForeColor =
                statusColor;

            e.CellStyle.SelectionBackColor =
                Color.White;

            e.CellStyle.Font = new Font(
                historicalItemsDataGridView.Font,
                FontStyle.Bold);

            e.FormattingApplied = true;
        }
    }

    protected override void OnShown(
        EventArgs e)
    {
        base.OnShown(e);

        closeButton.Focus();

        notesTextBox.SelectionStart = 0;
        notesTextBox.SelectionLength = 0;

        historicalItemsDataGridView.ClearSelection();
        historicalItemsDataGridView.CurrentCell = null;
    }
}
