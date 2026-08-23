using System.Net;
using System.Net.Http.Json;

using ExpenseTracker.Api.Features.Expenses;
using ExpenseTracker.Api.Features.Expenses.Items;
using ExpenseTracker.Api.Features.Expenses.Items.Requests;
using ExpenseTracker.Api.Features.Expenses.Requests;
using ExpenseTracker.Api.Features.Items;
using ExpenseTracker.Api.Tests.Features.Expenses.Helpers;
using ExpenseTracker.Api.Tests.Infrastructure;

namespace ExpenseTracker.Api.Tests.Features.Expenses.Api;

public sealed class CreateExpenseApiTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public CreateExpenseApiTests(
        CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateAsync_WithValidRequest_ShouldReturnCreatedExpense()
    {
        Item firstItem =
            await ExpenseApiTestHelper.CreateItemAsync(
                _client,
                unitPrice: 250.00m);

        Item secondItem =
            await ExpenseApiTestHelper.CreateItemAsync(
                _client,
                unitPrice: 15.50m);

        DateTime beforeCreation = DateTime.UtcNow;

        var request = new CreateExpenseRequest
        {
            Title = "Create Expense Test",
            Notes = "Valid expense creation test",
            Items =
            [
                new CreateExpenseItemRequest
                {
                    ItemId = firstItem.ItemId,
                    Quantity = 2.000m
                },
                new CreateExpenseItemRequest
                {
                    ItemId = secondItem.ItemId,
                    Quantity = 3.000m
                }
            ]
        };

        try
        {
            HttpResponseMessage response =
                await _client.PostAsJsonAsync(
                    "/api/v1/expenses",
                    request,
                    TestContext.Current.CancellationToken);

            DateTime afterCreation = DateTime.UtcNow;

            Assert.Equal(
                HttpStatusCode.Created,
                response.StatusCode);

            ExpenseResult? expense =
                await response.Content.ReadFromJsonAsync<ExpenseResult>(
                    TestContext.Current.CancellationToken);

            Assert.NotNull(expense);

            Assert.True(
                expense.ExpenseEntry.ExpenseEntryId > 0);

            Assert.Equal(
                request.Title,
                expense.ExpenseEntry.Title);

            Assert.Equal(
                request.Notes,
                expense.ExpenseEntry.Notes);

            Assert.Equal(
                546.50m,
                expense.ExpenseEntry.TotalCost);

            Assert.InRange(
                expense.ExpenseEntry.ExpenseDateTime,
                beforeCreation.AddSeconds(-1),
                afterCreation.AddSeconds(1));

            Assert.Equal(
                2,
                expense.ExpenseItems.Count);

            Assert.NotNull(response.Headers.Location);

            Assert.EndsWith(
                $"/api/v1/expenses/" +
                $"{expense.ExpenseEntry.ExpenseEntryId}",
                response.Headers.Location.ToString());

            ExpenseItem firstExpenseItem =
                Assert.Single(
                    expense.ExpenseItems,
                    item => item.ItemId == firstItem.ItemId);

            AssertExpenseItem(
                firstExpenseItem,
                expense.ExpenseEntry.ExpenseEntryId,
                firstItem,
                expectedQuantity: 2.000m,
                expectedLineTotal: 500.00m);

            ExpenseItem secondExpenseItem =
                Assert.Single(
                    expense.ExpenseItems,
                    item => item.ItemId == secondItem.ItemId);

            AssertExpenseItem(
                secondExpenseItem,
                expense.ExpenseEntry.ExpenseEntryId,
                secondItem,
                expectedQuantity: 3.000m,
                expectedLineTotal: 46.50m);
        }
        finally
        {
            await ExpenseApiTestHelper.DeleteItemIfPresentAsync(
                _client,
                firstItem.ItemId);

            await ExpenseApiTestHelper.DeleteItemIfPresentAsync(
                _client,
                secondItem.ItemId);
        }
    }

    [Fact]
    public async Task CreateAsync_WithDuplicateItemIds_ShouldNormalizeQuantities()
    {
        Item item =
            await ExpenseApiTestHelper.CreateItemAsync(
                _client,
                unitPrice: 250.00m);

        var request = new CreateExpenseRequest
        {
            Title = "Office Supplies",
            Notes = "Duplicate item normalization test",
            Items =
            [
                new CreateExpenseItemRequest
                {
                    ItemId = item.ItemId,
                    Quantity = 1.250m
                },
                new CreateExpenseItemRequest
                {
                    ItemId = item.ItemId,
                    Quantity = 2.750m
                }
            ]
        };

        try
        {
            HttpResponseMessage response =
                await _client.PostAsJsonAsync(
                    "/api/v1/expenses",
                    request,
                    TestContext.Current.CancellationToken);

            Assert.Equal(
                HttpStatusCode.Created,
                response.StatusCode);

            ExpenseResult? expense =
                await response.Content.ReadFromJsonAsync<ExpenseResult>(
                    TestContext.Current.CancellationToken);

            Assert.NotNull(expense);

            Assert.Equal(
                request.Title,
                expense.ExpenseEntry.Title);

            ExpenseItem expenseItem =
                Assert.Single(expense.ExpenseItems);

            Assert.Equal(
                item.ItemId,
                expenseItem.ItemId);

            Assert.Equal(
                4.000m,
                expenseItem.Quantity);

            Assert.Equal(
                250.00m,
                expenseItem.UnitPriceSnapshot);

            Assert.Equal(
                1000.00m,
                expenseItem.LineTotal);

            Assert.Equal(
                1000.00m,
                expense.ExpenseEntry.TotalCost);
        }
        finally
        {
            await ExpenseApiTestHelper.DeleteItemIfPresentAsync(
                _client,
                item.ItemId);
        }
    }

    [Fact]
    public async Task CreateAsync_WithFractionalQuantity_ShouldRoundLineTotalAwayFromZero()
    {
        Item item =
            await ExpenseApiTestHelper.CreateItemAsync(
                _client,
                unitPrice: 10.01m);

        var request = new CreateExpenseRequest
        {
            Title = "Office Supplies",
            Notes = "Rounding test",
            Items =
            [
                new CreateExpenseItemRequest
                {
                    ItemId = item.ItemId,
                    Quantity = 1.005m
                }
            ]
        };

        try
        {
            HttpResponseMessage response =
                await _client.PostAsJsonAsync(
                    "/api/v1/expenses",
                    request,
                    TestContext.Current.CancellationToken);

            Assert.Equal(
                HttpStatusCode.Created,
                response.StatusCode);

            ExpenseResult? expense =
                await response.Content.ReadFromJsonAsync<ExpenseResult>(
                    TestContext.Current.CancellationToken);

            Assert.NotNull(expense);

            Assert.Equal(
                request.Title,
                expense.ExpenseEntry.Title);

            ExpenseItem expenseItem =
                Assert.Single(expense.ExpenseItems);

            Assert.Equal(
                Math.Round(
                    10.01m * 1.005m,
                    decimals: 2,
                    mode: MidpointRounding.AwayFromZero),
                expenseItem.LineTotal);

            Assert.Equal(
                expenseItem.LineTotal,
                expense.ExpenseEntry.TotalCost);
        }
        finally
        {
            await ExpenseApiTestHelper.DeleteItemIfPresentAsync(
                _client,
                item.ItemId);
        }
    }

    [Fact]
    public async Task CreateAsync_WithoutNotes_ShouldReturnCreatedExpenseWithNullNotes()
    {
        Item item =
            await ExpenseApiTestHelper.CreateItemAsync(
                _client,
                unitPrice: 25.00m);

        var request = new CreateExpenseRequest
        {
            Title = "Office Supplies",
            Notes = null,
            Items =
            [
                new CreateExpenseItemRequest
                {
                    ItemId = item.ItemId,
                    Quantity = 1.000m
                }
            ]
        };

        try
        {
            HttpResponseMessage response =
                await _client.PostAsJsonAsync(
                    "/api/v1/expenses",
                    request,
                    TestContext.Current.CancellationToken);

            Assert.Equal(
                HttpStatusCode.Created,
                response.StatusCode);

            ExpenseResult? expense =
                await response.Content.ReadFromJsonAsync<ExpenseResult>(
                    TestContext.Current.CancellationToken);

            Assert.NotNull(expense);

            Assert.Equal(
                request.Title,
                expense.ExpenseEntry.Title);

            Assert.Null(expense.ExpenseEntry.Notes);
        }
        finally
        {
            await ExpenseApiTestHelper.DeleteItemIfPresentAsync(
                _client,
                item.ItemId);
        }
    }

    [Fact]
    public async Task CreateAsync_WithMissingItem_ShouldReturnNotFound()
    {
        const int missingItemId = int.MaxValue;

        var request = new CreateExpenseRequest
        {
            Title = "Office Supplies",
            Notes = "Missing item test",
            Items =
            [
                new CreateExpenseItemRequest
                {
                    ItemId = missingItemId,
                    Quantity = 1.000m
                }
            ]
        };

        HttpResponseMessage response =
            await _client.PostAsJsonAsync(
                "/api/v1/expenses",
                request,
                TestContext.Current.CancellationToken);

        Assert.Equal(
            HttpStatusCode.NotFound,
            response.StatusCode);
    }

    [Fact]
    public async Task CreateAsync_WithExistingAndMissingItems_ShouldReturnNotFound()
    {
        Item existingItem =
            await ExpenseApiTestHelper.CreateItemAsync(
                _client,
                unitPrice: 100.00m);

        const int missingItemId = int.MaxValue;

        var request = new CreateExpenseRequest
        {
            Title = "Office Supplies",
            Notes = "Partial missing item test",
            Items =
            [
                new CreateExpenseItemRequest
                {
                    ItemId = existingItem.ItemId,
                    Quantity = 1.000m
                },
                new CreateExpenseItemRequest
                {
                    ItemId = missingItemId,
                    Quantity = 1.000m
                }
            ]
        };

        try
        {
            HttpResponseMessage response =
                await _client.PostAsJsonAsync(
                    "/api/v1/expenses",
                    request,
                    TestContext.Current.CancellationToken);

            Assert.Equal(
                HttpStatusCode.NotFound,
                response.StatusCode);
        }
        finally
        {
            await ExpenseApiTestHelper.DeleteItemIfPresentAsync(
                _client,
                existingItem.ItemId);
        }
    }

    [Fact]
    public async Task CreateAsync_WithEmptyItems_ShouldReturnBadRequest()
    {
        var request = new CreateExpenseRequest
        {
            Title = "Office Supplies",
            Notes = "Empty items test",
            Items = []
        };

        HttpResponseMessage response =
            await _client.PostAsJsonAsync(
                "/api/v1/expenses",
                request,
                TestContext.Current.CancellationToken);

        Assert.Equal(
            HttpStatusCode.BadRequest,
            response.StatusCode);
    }

    [Fact]
    public async Task CreateAsync_WithInvalidItemId_ShouldReturnBadRequest()
    {
        var request = new CreateExpenseRequest
        {
            Title = "Office Supplies",
            Items =
            [
                new CreateExpenseItemRequest
                {
                    ItemId = 0,
                    Quantity = 1.000m
                }
            ]
        };

        HttpResponseMessage response =
            await _client.PostAsJsonAsync(
                "/api/v1/expenses",
                request,
                TestContext.Current.CancellationToken);

        Assert.Equal(
            HttpStatusCode.BadRequest,
            response.StatusCode);
    }

    [Fact]
    public async Task CreateAsync_WithZeroQuantity_ShouldReturnBadRequest()
    {
        var request = new CreateExpenseRequest
        {
            Title = "Office Supplies",
            Items =
            [
                new CreateExpenseItemRequest
                {
                    ItemId = 1,
                    Quantity = 0m
                }
            ]
        };

        HttpResponseMessage response =
            await _client.PostAsJsonAsync(
                "/api/v1/expenses",
                request,
                TestContext.Current.CancellationToken);

        Assert.Equal(
            HttpStatusCode.BadRequest,
            response.StatusCode);
    }

    [Fact]
    public async Task CreateAsync_WithQuantityHavingMoreThanThreeDecimalPlaces_ShouldReturnBadRequest()
    {
        var request = new CreateExpenseRequest
        {
            Title = "Office Supplies",
            Items =
            [
                new CreateExpenseItemRequest
                {
                    ItemId = 1,
                    Quantity = 1.2345m
                }
            ]
        };

        HttpResponseMessage response =
            await _client.PostAsJsonAsync(
                "/api/v1/expenses",
                request,
                TestContext.Current.CancellationToken);

        Assert.Equal(
            HttpStatusCode.BadRequest,
            response.StatusCode);
    }

    [Fact]
    public async Task CreateAsync_WithEmptyTitle_ShouldReturnBadRequest()
    {
        var request = new CreateExpenseRequest
        {
            Title = string.Empty,
            Items =
            [
                new CreateExpenseItemRequest
                {
                    ItemId = 1,
                    Quantity = 1.000m
                }
            ]
        };

        HttpResponseMessage response =
            await _client.PostAsJsonAsync(
                "/api/v1/expenses",
                request,
                TestContext.Current.CancellationToken);

        Assert.Equal(
            HttpStatusCode.BadRequest,
            response.StatusCode);
    }

    [Fact]
    public async Task CreateAsync_WithNotesLongerThan500Characters_ShouldReturnBadRequest()
    {
        var request = new CreateExpenseRequest
        {
            Title = "Office Supplies",
            Notes = new string('A', 501),
            Items =
            [
                new CreateExpenseItemRequest
                {
                    ItemId = 1,
                    Quantity = 1.000m
                }
            ]
        };

        HttpResponseMessage response =
            await _client.PostAsJsonAsync(
                "/api/v1/expenses",
                request,
                TestContext.Current.CancellationToken);

        Assert.Equal(
            HttpStatusCode.BadRequest,
            response.StatusCode);
    }

    [Fact]
    public async Task CreateAsync_WithUnknownParentProperty_ShouldReturnBadRequest()
    {
        var request = new
        {
            title = "Office Supplies",
            notes = "Unknown property test",
            unexpectedProperty = true,
            items = new[]
            {
                new
                {
                    itemId = 1,
                    quantity = 1.000m
                }
            }
        };

        HttpResponseMessage response =
            await _client.PostAsJsonAsync(
                "/api/v1/expenses",
                request,
                TestContext.Current.CancellationToken);

        Assert.Equal(
            HttpStatusCode.BadRequest,
            response.StatusCode);
    }

    [Fact]
    public async Task CreateAsync_WithUnknownNestedProperty_ShouldReturnBadRequest()
    {
        var request = new
        {
            title = "Office Supplies",
            notes = "Unknown nested property test",
            items = new[]
            {
                new
                {
                    itemId = 1,
                    quantity = 1.000m,
                    lineTotal = 100.00m
                }
            }
        };

        HttpResponseMessage response =
            await _client.PostAsJsonAsync(
                "/api/v1/expenses",
                request,
                TestContext.Current.CancellationToken);

        Assert.Equal(
            HttpStatusCode.BadRequest,
            response.StatusCode);
    }

    private static void AssertExpenseItem(
        ExpenseItem expenseItem,
        int expectedExpenseEntryId,
        Item expectedItem,
        decimal expectedQuantity,
        decimal expectedLineTotal)
    {
        Assert.True(
            expenseItem.ExpenseItemId > 0);

        Assert.Equal(
            expectedExpenseEntryId,
            expenseItem.ExpenseEntryId);

        Assert.Equal(
            expectedItem.ItemId,
            expenseItem.ItemId);

        Assert.Equal(
            expectedItem.Name,
            expenseItem.ItemNameSnapshot);

        Assert.Equal(
            expectedItem.Code,
            expenseItem.ItemCodeSnapshot);

        Assert.Equal(
            expectedItem.Brand,
            expenseItem.BrandSnapshot);

        Assert.Equal(
            expectedQuantity,
            expenseItem.Quantity);

        Assert.Equal(
            expectedItem.UnitPrice,
            expenseItem.UnitPriceSnapshot);

        Assert.Equal(
            expectedLineTotal,
            expenseItem.LineTotal);
    }
}
