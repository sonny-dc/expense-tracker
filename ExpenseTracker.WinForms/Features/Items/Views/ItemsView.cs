using System.Text.Json;

using ExpenseTracker.WinForms.Features.Items.Api;
using ExpenseTracker.WinForms.Features.Items.Models;

using ExpenseTracker.WinForms.Infrastructure.Http;
using ExpenseTracker.WinForms.Infrastructure.Dialogs;

namespace ExpenseTracker.WinForms.Features.Items.Views;

public partial class ItemsView : UserControl
{
    private readonly ItemsApiClient _itemsApiClient;

    private IReadOnlyList<Item> _items = [];

    private bool _hasLoaded;
    private bool _isLoading;

    public ItemsView(
        ItemsApiClient itemsApiClient)
    {
        InitializeComponent();

        _itemsApiClient = itemsApiClient;

        Load += ItemsView_Load;
        refreshButton.Click += refreshButton_Click;
        searchTextBox.TextChanged += searchTextBox_TextChanged;
        addItemButton.Click += addItemButton_Click;
        editItemButton.Click += editItemButton_Click;
        deleteItemButton.Click += deleteItemButton_Click;
        itemsDataGridView.SelectionChanged +=
            itemsDataGridView_SelectionChanged;
    }

    private async void ItemsView_Load(
        object? sender,
        EventArgs e)
    {
        if (_hasLoaded)
        {
            return;
        }

        _hasLoaded = true;

        await LoadItemsAsync();
    }

    private async void refreshButton_Click(
        object? sender,
        EventArgs e)
    {
        await LoadItemsAsync();
    }

    private async void addItemButton_Click(
        object? sender,
        EventArgs e)
    {
        using var itemEditorForm =
            new ItemEditorForm();

        DialogResult dialogResult =
            itemEditorForm.ShowDialog(this);

        if (dialogResult != DialogResult.OK ||
            itemEditorForm.CreateRequest is null)
        {
            return;
        }

        await CreateItemAsync(
            itemEditorForm.CreateRequest);
    }

    private async void editItemButton_Click(
        object? sender,
        EventArgs e)
    {
        Item? selectedItem =
            GetSelectedItem();

        if (selectedItem is null)
        {
            return;
        }

        using var itemEditorForm =
            new ItemEditorForm(selectedItem);

        DialogResult dialogResult =
            itemEditorForm.ShowDialog(this);

        if (dialogResult != DialogResult.OK ||
            itemEditorForm.UpdateRequest is null)
        {
            return;
        }

        await UpdateItemAsync(
            selectedItem.ItemId,
            itemEditorForm.UpdateRequest);
    }

    private async void deleteItemButton_Click(
        object? sender,
        EventArgs e)
    {
        Item? selectedItem =
            GetSelectedItem();

        if (selectedItem is null)
        {
            return;
        }

        DialogResult confirmationResult =
            MessageBox.Show(
                this,
                $"Delete \"{selectedItem.Name}\"?\n\n" +
                "Existing expense records will keep their historical " +
                "item details.",
                "Delete Item",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

        if (confirmationResult != DialogResult.Yes)
        {
            return;
        }

        await DeleteItemAsync(selectedItem);
    }

    private void itemsDataGridView_SelectionChanged(
        object? sender,
        EventArgs e)
    {
        UpdateItemActionAvailability();
    }

    private void UpdateItemActionAvailability()
    {
        bool hasSelectedItem =
            GetSelectedItem() is not null;

        editItemButton.Enabled =
            !_isLoading &&
            hasSelectedItem;

        deleteItemButton.Enabled =
            !_isLoading &&
            hasSelectedItem;
    }

    private Item? GetSelectedItem()
    {
        return itemsDataGridView.CurrentRow?.DataBoundItem
            as Item;
    }

    private async Task CreateItemAsync(
        CreateItemRequest request)
    {
        try
        {
            SetLoadingState(isLoading: true);

            statusLabel.Text = "Creating item...";

            Item createdItem =
                await _itemsApiClient.CreateAsync(request);

            searchTextBox.Clear();

            await LoadItemsAfterMutationAsync();

            SelectItem(createdItem.ItemId);

            MessageBox.Show(
                this,
                $"The item \"{createdItem.Name}\" was created successfully.",
                "Item Created",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        catch (ApiClientException exception)
        {
            ShowApiError(exception);
        }
        catch (HttpRequestException)
        {
            statusLabel.Text =
                "Unable to connect to the ExpenseTracker API.";

            MessageBox.Show(
                this,
                "The item could not be created because the API is unavailable. " +
                "Make sure the ExpenseTracker API is running, then try again.",
                "Connection Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        catch (JsonException)
        {
            statusLabel.Text =
                "The API returned an unexpected response.";

            MessageBox.Show(
                this,
                "The item may have been submitted, but the API response " +
                "could not be read. Refresh the item list before trying again.",
                "Response Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        catch (TaskCanceledException)
        {
            statusLabel.Text =
                "The request timed out.";

            MessageBox.Show(
                this,
                "The create request took too long to complete. " +
                "Refresh the item list before trying again.",
                "Request Timeout",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
        finally
        {
            SetLoadingState(isLoading: false);
        }
    }

    private async Task UpdateItemAsync(
        int itemId,
        UpdateItemRequest request)
    {
        try
        {
            SetLoadingState(isLoading: true);

            statusLabel.Text = "Updating item...";

            Item updatedItem =
                await _itemsApiClient.UpdateAsync(
                    itemId,
                    request);

            searchTextBox.Clear();

            await LoadItemsAfterMutationAsync();

            SelectItem(updatedItem.ItemId);

            MessageBox.Show(
                this,
                $"The item \"{updatedItem.Name}\" was updated successfully.",
                "Item Updated",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        catch (ApiClientException exception)
        {
            ShowApiError(exception);
        }
        catch (HttpRequestException)
        {
            statusLabel.Text =
                "Unable to connect to the ExpenseTracker API.";

            MessageBox.Show(
                this,
                "The item could not be updated because the API is unavailable. " +
                "Make sure the ExpenseTracker API is running, then try again.",
                "Connection Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        catch (JsonException)
        {
            statusLabel.Text =
                "The API returned an unexpected response.";

            MessageBox.Show(
                this,
                "The item may have been updated, but the API response " +
                "could not be read. Refresh the item list before trying again.",
                "Response Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        catch (TaskCanceledException)
        {
            statusLabel.Text =
                "The request timed out.";

            MessageBox.Show(
                this,
                "The update request took too long to complete. " +
                "Refresh the item list before trying again.",
                "Request Timeout",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
        finally
        {
            SetLoadingState(isLoading: false);
        }
    }

    private async Task DeleteItemAsync(
        Item item)
    {
        try
        {
            SetLoadingState(isLoading: true);

            statusLabel.Text = "Deleting item...";

            await _itemsApiClient.DeleteAsync(
                item.ItemId);

            await LoadItemsAfterMutationAsync();

            MessageBox.Show(
                this,
                $"The item \"{item.Name}\" was deleted successfully.",
                "Item Deleted",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        catch (ApiClientException exception)
        {
            ShowApiError(exception);
        }
        catch (HttpRequestException)
        {
            statusLabel.Text =
                "Unable to connect to the ExpenseTracker API.";

            MessageBox.Show(
                this,
                "The item could not be deleted because the API is unavailable. " +
                "Make sure the ExpenseTracker API is running, then try again.",
                "Connection Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        catch (TaskCanceledException)
        {
            statusLabel.Text =
                "The request timed out.";

            MessageBox.Show(
                this,
                "The delete request took too long to complete. " +
                "Refresh the item list before trying again.",
                "Request Timeout",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
        finally
        {
            SetLoadingState(isLoading: false);
        }
    }

    private void searchTextBox_TextChanged(
        object? sender,
        EventArgs e)
    {
        DisplayFilteredItems();
    }

    private async Task LoadItemsAsync()
    {
        if (_isLoading)
        {
            return;
        }

        try
        {
            SetLoadingState(isLoading: true);

            _items = await _itemsApiClient.GetAllAsync();

            DisplayFilteredItems();
        }

        catch (ApiClientException exception)
        {
            _items = [];
            itemsDataGridView.DataSource = null;

            ShowApiError(exception);
        }

        catch (HttpRequestException)
        {
            _items = [];
            itemsDataGridView.DataSource = null;

            statusLabel.Text =
                "Unable to connect to the ExpenseTracker API.";

            MessageBox.Show(
                this,
                "The item list could not be loaded. " +
                "Make sure the ExpenseTracker API is running, then try again.",
                "Connection Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        catch (JsonException)
        {
            _items = [];
            itemsDataGridView.DataSource = null;

            statusLabel.Text =
                "The API returned an unexpected response.";

            MessageBox.Show(
                this,
                "The API response could not be read. " +
                "The client and API contracts may not match.",
                "Response Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        catch (TaskCanceledException)
        {
            statusLabel.Text =
                "The request timed out.";

            MessageBox.Show(
                this,
                "The API request took too long to complete. " +
                "Please try again.",
                "Request Timeout",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
        finally
        {
            SetLoadingState(isLoading: false);
        }
    }

    private void DisplayFilteredItems()
    {
        string searchTerm =
            searchTextBox.Text.Trim();

        IReadOnlyList<Item> filteredItems;

        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            filteredItems = _items;
        }
        else
        {
            filteredItems = _items
                .Where(item =>
                    ContainsSearchTerm(
                        item.Name,
                        searchTerm) ||
                    ContainsSearchTerm(
                        item.Code,
                        searchTerm) ||
                    ContainsSearchTerm(
                        item.Brand,
                        searchTerm))
                .ToList();
        }

        itemsDataGridView.DataSource = null;
        itemsDataGridView.DataSource = filteredItems;

        UpdateStatus(
            displayedItemCount: filteredItems.Count,
            totalItemCount: _items.Count,
            isSearching: !string.IsNullOrWhiteSpace(searchTerm));

        UpdateItemActionAvailability();
    }

    private void SetLoadingState(
        bool isLoading)
    {
        _isLoading = isLoading;

        UseWaitCursor = isLoading;

        refreshButton.Enabled = !isLoading;
        addItemButton.Enabled = !isLoading;
        searchTextBox.Enabled = !isLoading;
        itemsDataGridView.Enabled = !isLoading;

        UpdateItemActionAvailability();

        if (isLoading)
        {
            statusLabel.Text = "Loading items...";
        }
    }

    private void UpdateStatus(
        int displayedItemCount,
        int totalItemCount,
        bool isSearching)
    {
        if (totalItemCount == 0)
        {
            statusLabel.Text =
                "No items have been recorded.";

            return;
        }

        if (isSearching)
        {
            statusLabel.Text =
                $"{displayedItemCount} of {totalItemCount} items shown";

            return;
        }

        statusLabel.Text =
            totalItemCount == 1
                ? "1 item"
                : $"{totalItemCount} items";
    }

    private static bool ContainsSearchTerm(
        string value,
        string searchTerm)
    {
        return value.Contains(
            searchTerm,
            StringComparison.OrdinalIgnoreCase);
    }

    private async Task LoadItemsAfterMutationAsync()
    {
        _items = await _itemsApiClient.GetAllAsync();

        DisplayFilteredItems();
    }

    private void ShowApiError(
    ApiClientException exception)
    {
        statusLabel.Text = exception.Title;

        ApiErrorDialog.Show(
            this,
            exception);
    }

    private void SelectItem(
    int itemId)
    {
        foreach (DataGridViewRow row
            in itemsDataGridView.Rows)
        {
            if (row.DataBoundItem is Item item &&
                item.ItemId == itemId)
            {
                row.Selected = true;

                itemsDataGridView.CurrentCell =
                    row.Cells[0];

                return;
            }
        }
    }

}
