using ExpenseTracker.WinForms.Features.Expenses.Views;
using ExpenseTracker.WinForms.Features.Home.Views;
using ExpenseTracker.WinForms.Features.Items.Views;

namespace ExpenseTracker.WinForms.Shell;

public partial class MainForm : Form
{
    private readonly HomeView _homeView;
    private readonly ItemsView _itemsView;
    private readonly ExpensesView _expensesView;

    public MainForm(
        HomeView homeView,
        ItemsView itemsView,
        ExpensesView expensesView)
    {
        InitializeComponent();

        _homeView = homeView;
        _itemsView = itemsView;
        _expensesView = expensesView;

        AddView(_homeView);
        AddView(_itemsView);
        AddView(_expensesView);

        ShowView(_homeView);
        SetActiveButton(homeButton);
    }

    private void homeButton_Click(
        object? sender,
        EventArgs e)
    {
        ShowView(_homeView);
        SetActiveButton(homeButton);
    }

    private void itemsButton_Click(
        object? sender,
        EventArgs e)
    {
        ShowView(_itemsView);
        SetActiveButton(itemsButton);
    }

    private void expensesButton_Click(
        object? sender,
        EventArgs e)
    {
        ShowView(_expensesView);
        SetActiveButton(expensesButton);
    }

    private static void ShowView(UserControl view)
    {
        view.BringToFront();
    }

    private void AddView(UserControl view)
    {
        view.Dock = DockStyle.Fill;
        contentPanel.Controls.Add(view);
    }

    private void SetActiveButton(Button activeButton)
    {
        Color defaultColor = Color.Green;

        Color activeColor = Color.FromArgb(
            red: 0,
            green: 100,
            blue: 0);

        homeButton.BackColor = defaultColor;
        itemsButton.BackColor = defaultColor;
        expensesButton.BackColor = defaultColor;

        activeButton.BackColor = activeColor;
    }
}
