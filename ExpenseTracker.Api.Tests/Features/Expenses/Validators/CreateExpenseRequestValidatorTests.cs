using FluentValidation.Results;

using ExpenseTracker.Api.Features.Expenses.Items.Requests;
using ExpenseTracker.Api.Features.Expenses.Requests;
using ExpenseTracker.Api.Features.Expenses.Validators;

namespace ExpenseTracker.Api.Tests.Features.Expenses.Validators;

public sealed class CreateExpenseRequestValidatorTests
{
    private readonly CreateExpenseRequestValidator _validator = new();

    [Fact]
    public async Task ValidateAsync_WithValidRequest_ShouldBeValid()
    {
        CreateExpenseRequest request = CreateValidRequest();

        ValidationResult result =
            await _validator.ValidateAsync(
                request,
                TestContext.Current.CancellationToken);

        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public async Task ValidateAsync_WithNullNotes_ShouldBeValid()
    {
        CreateExpenseRequest request = CreateValidRequest();
        request.Notes = null;

        ValidationResult result =
            await _validator.ValidateAsync(
                request,
                TestContext.Current.CancellationToken);

        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public async Task ValidateAsync_WithEmptyNotes_ShouldBeValid()
    {
        CreateExpenseRequest request = CreateValidRequest();
        request.Notes = string.Empty;

        ValidationResult result =
            await _validator.ValidateAsync(
                request,
                TestContext.Current.CancellationToken);

        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public async Task ValidateAsync_WithNotesHaving500Characters_ShouldBeValid()
    {
        CreateExpenseRequest request = CreateValidRequest();
        request.Notes = new string('A', 500);

        ValidationResult result =
            await _validator.ValidateAsync(
                request,
                TestContext.Current.CancellationToken);

        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public async Task ValidateAsync_WithNotesLongerThan500Characters_ShouldBeInvalid()
    {
        CreateExpenseRequest request = CreateValidRequest();
        request.Notes = new string('A', 501);

        ValidationResult result =
            await _validator.ValidateAsync(
                request,
                TestContext.Current.CancellationToken);

        Assert.False(result.IsValid);

        Assert.Contains(
            result.Errors,
            error => error.PropertyName ==
                nameof(CreateExpenseRequest.Notes));
    }

    [Fact]
    public async Task ValidateAsync_WithNoItems_ShouldBeInvalid()
    {
        CreateExpenseRequest request = CreateValidRequest();
        request.Items = [];

        ValidationResult result =
            await _validator.ValidateAsync(
                request,
                TestContext.Current.CancellationToken);

        Assert.False(result.IsValid);

        Assert.Contains(
            result.Errors,
            error =>
                error.PropertyName ==
                    nameof(CreateExpenseRequest.Items) &&
                error.ErrorMessage ==
                    "At least one expense item must be provided.");
    }

    [Fact]
    public async Task ValidateAsync_WithZeroItemId_ShouldBeInvalid()
    {
        CreateExpenseRequest request = CreateValidRequest();
        request.Items[0].ItemId = 0;

        ValidationResult result =
            await _validator.ValidateAsync(
                request,
                TestContext.Current.CancellationToken);

        Assert.False(result.IsValid);

        Assert.Contains(
            result.Errors,
            error => error.PropertyName.EndsWith(
                nameof(CreateExpenseItemRequest.ItemId)));
    }

    [Fact]
    public async Task ValidateAsync_WithNegativeItemId_ShouldBeInvalid()
    {
        CreateExpenseRequest request = CreateValidRequest();
        request.Items[0].ItemId = -1;

        ValidationResult result =
            await _validator.ValidateAsync(
                request,
                TestContext.Current.CancellationToken);

        Assert.False(result.IsValid);

        Assert.Contains(
            result.Errors,
            error => error.PropertyName.EndsWith(
                nameof(CreateExpenseItemRequest.ItemId)));
    }

    [Fact]
    public async Task ValidateAsync_WithZeroQuantity_ShouldBeInvalid()
    {
        CreateExpenseRequest request = CreateValidRequest();
        request.Items[0].Quantity = 0m;

        ValidationResult result =
            await _validator.ValidateAsync(
                request,
                TestContext.Current.CancellationToken);

        Assert.False(result.IsValid);

        Assert.Contains(
            result.Errors,
            error => error.PropertyName.EndsWith(
                nameof(CreateExpenseItemRequest.Quantity)));
    }

    [Fact]
    public async Task ValidateAsync_WithNegativeQuantity_ShouldBeInvalid()
    {
        CreateExpenseRequest request = CreateValidRequest();
        request.Items[0].Quantity = -0.001m;

        ValidationResult result =
            await _validator.ValidateAsync(
                request,
                TestContext.Current.CancellationToken);

        Assert.False(result.IsValid);

        Assert.Contains(
            result.Errors,
            error => error.PropertyName.EndsWith(
                nameof(CreateExpenseItemRequest.Quantity)));
    }

    [Fact]
    public async Task ValidateAsync_WithQuantityHavingThreeDecimalPlaces_ShouldBeValid()
    {
        CreateExpenseRequest request = CreateValidRequest();
        request.Items[0].Quantity = 1.234m;

        ValidationResult result =
            await _validator.ValidateAsync(
                request,
                TestContext.Current.CancellationToken);

        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public async Task ValidateAsync_WithQuantityHavingMoreThanThreeDecimalPlaces_ShouldBeInvalid()
    {
        CreateExpenseRequest request = CreateValidRequest();
        request.Items[0].Quantity = 1.2345m;

        ValidationResult result =
            await _validator.ValidateAsync(
                request,
                TestContext.Current.CancellationToken);

        Assert.False(result.IsValid);

        Assert.Contains(
            result.Errors,
            error => error.PropertyName.EndsWith(
                nameof(CreateExpenseItemRequest.Quantity)));
    }

    [Fact]
    public async Task ValidateAsync_WithMultipleValidItems_ShouldBeValid()
    {
        CreateExpenseRequest request = CreateValidRequest();

        request.Items =
        [
            new CreateExpenseItemRequest
            {
                ItemId = 1,
                Quantity = 1.250m
            },
            new CreateExpenseItemRequest
            {
                ItemId = 2,
                Quantity = 2.500m
            }
        ];

        ValidationResult result =
            await _validator.ValidateAsync(
                request,
                TestContext.Current.CancellationToken);

        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    private static CreateExpenseRequest CreateValidRequest()
    {
        return new CreateExpenseRequest
        {
            Notes = "Office supply expense",
            Items =
            [
                new CreateExpenseItemRequest
                {
                    ItemId = 1,
                    Quantity = 2.000m
                }
            ]
        };
    }
}
