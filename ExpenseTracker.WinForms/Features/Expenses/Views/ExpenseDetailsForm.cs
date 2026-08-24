using ExpenseTracker.WinForms.Features.Expenses.Models;

using ExpenseTracker.WinForms.Infrastructure.Presentation;

namespace ExpenseTracker.WinForms.Features.Expenses.Views;

public partial class ExpenseDetailsForm : Form
{
    private readonly ExpenseResult _expense;

    private readonly DisplayFormatter _displayFormatter;

    public ExpenseDetailsForm(
        ExpenseResult expense,
        DisplayFormatter displayFormatter)
    {
        ArgumentNullException.ThrowIfNull(expense);
        ArgumentNullException.ThrowIfNull(displayFormatter);

        InitializeComponent();

        _expense = expense;
        _displayFormatter = displayFormatter;

        DisplayExpense();
    }

    private void DisplayExpense()
    {
        ExpenseEntry entry =
            _expense.ExpenseEntry;

        Text =
            $"Expense #{entry.ExpenseEntryId}";

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

        sourceStatusValueLabel.Text =
            GetSourceStatusText(
                deletedSourceCount);

        sourceStatusValueLabel.ForeColor =
            deletedSourceCount == 0
                ? Color.Green
                : Color.DarkOrange;

        notesTextBox.Text =
            string.IsNullOrWhiteSpace(entry.Notes)
                ? "No notes were provided."
                : entry.Notes.Trim();

        historicalItemsDataGridView.DataSource =
            _expense.ExpenseItems.ToList();

        historicalItemsDataGridView.ClearSelection();
        historicalItemsDataGridView.CurrentCell = null;
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

            e.Value = sourceDeleted
                ? "Deleted"
                : "Available";

            e.CellStyle.ForeColor =
                sourceDeleted
                    ? Color.Firebrick
                    : Color.Green;

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
