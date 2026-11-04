using FluentValidation.Results;

using ExpenseTracker.Api.Features.Budget.Accounts;
using ExpenseTracker.Api.Features.Budget.Accounts.Requests;
using ExpenseTracker.Api.Features.Budget.Accounts.Validators;

namespace ExpenseTracker.Api.Tests.Features.Budget.Accounts.Validators;

public sealed class UpdateBudgetAccountRequestValidatorTests
{
    private readonly UpdateBudgetAccountRequestValidator _validator =
        new();

    [Fact]
    public async Task ValidateAsync_WithOneFieldProvided_ShouldBeValid()
    {
        var request = new UpdateBudgetAccountRequest
        {
            Balance = 1500.00m
        };

        ValidationResult result =
            await _validator.ValidateAsync(
                request,
                TestContext.Current.CancellationToken);

        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public async Task ValidateAsync_WithAllFieldsProvided_ShouldBeValid()
    {
        var request = new UpdateBudgetAccountRequest
        {
            Name = "Updated Budget",
            Description = "Updated description",
            Balance = 1500.00m,
            Status = BudgetAccountStatus.Archived
        };

        ValidationResult result =
            await _validator.ValidateAsync(
                request,
                TestContext.Current.CancellationToken);

        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public async Task ValidateAsync_WithEmptyName_ShouldBeInvalid(
        string name)
    {
        var request = new UpdateBudgetAccountRequest
        {
            Name = name
        };

        ValidationResult result =
            await _validator.ValidateAsync(
                request,
                TestContext.Current.CancellationToken);

        Assert.False(result.IsValid);

        Assert.Contains(
            result.Errors,
            error =>
                error.PropertyName ==
                nameof(UpdateBudgetAccountRequest.Name));
    }

    [Fact]
    public async Task ValidateAsync_WithNameHaving100Characters_ShouldBeValid()
    {
        var request = new UpdateBudgetAccountRequest
        {
            Name = new string('A', 100)
        };

        ValidationResult result =
            await _validator.ValidateAsync(
                request,
                TestContext.Current.CancellationToken);

        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public async Task ValidateAsync_WithNameLongerThan100Characters_ShouldBeInvalid()
    {
        var request = new UpdateBudgetAccountRequest
        {
            Name = new string('A', 101)
        };

        ValidationResult result =
            await _validator.ValidateAsync(
                request,
                TestContext.Current.CancellationToken);

        Assert.False(result.IsValid);

        Assert.Contains(
            result.Errors,
            error =>
                error.PropertyName ==
                nameof(UpdateBudgetAccountRequest.Name));
    }

    [Fact]
    public async Task ValidateAsync_WithDescriptionHaving500Characters_ShouldBeValid()
    {
        var request = new UpdateBudgetAccountRequest
        {
            Description = new string('A', 500)
        };

        ValidationResult result =
            await _validator.ValidateAsync(
                request,
                TestContext.Current.CancellationToken);

        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public async Task ValidateAsync_WithDescriptionLongerThan500Characters_ShouldBeInvalid()
    {
        var request = new UpdateBudgetAccountRequest
        {
            Description = new string('A', 501)
        };

        ValidationResult result =
            await _validator.ValidateAsync(
                request,
                TestContext.Current.CancellationToken);

        Assert.False(result.IsValid);

        Assert.Contains(
            result.Errors,
            error =>
                error.PropertyName ==
                nameof(UpdateBudgetAccountRequest.Description));
    }

    [Fact]
    public async Task ValidateAsync_WithZeroBalance_ShouldBeValid()
    {
        var request = new UpdateBudgetAccountRequest
        {
            Balance = 0m
        };

        ValidationResult result =
            await _validator.ValidateAsync(
                request,
                TestContext.Current.CancellationToken);

        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public async Task ValidateAsync_WithNegativeBalance_ShouldBeValid()
    {
        var request = new UpdateBudgetAccountRequest
        {
            Balance = -500.00m
        };

        ValidationResult result =
            await _validator.ValidateAsync(
                request,
                TestContext.Current.CancellationToken);

        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public async Task ValidateAsync_WithBalanceHavingTwoDecimalPlaces_ShouldBeValid()
    {
        var request = new UpdateBudgetAccountRequest
        {
            Balance = 1000.12m
        };

        ValidationResult result =
            await _validator.ValidateAsync(
                request,
                TestContext.Current.CancellationToken);

        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public async Task ValidateAsync_WithBalanceHavingMoreThanTwoDecimalPlaces_ShouldBeInvalid()
    {
        var request = new UpdateBudgetAccountRequest
        {
            Balance = 1000.123m
        };

        ValidationResult result =
            await _validator.ValidateAsync(
                request,
                TestContext.Current.CancellationToken);

        Assert.False(result.IsValid);

        Assert.Contains(
            result.Errors,
            error =>
                error.PropertyName ==
                nameof(UpdateBudgetAccountRequest.Balance));
    }

    [Theory]
    [InlineData(BudgetAccountStatus.Open)]
    [InlineData(BudgetAccountStatus.Archived)]
    [InlineData(BudgetAccountStatus.Settled)]
    public async Task ValidateAsync_WithValidStatus_ShouldBeValid(
        BudgetAccountStatus status)
    {
        var request = new UpdateBudgetAccountRequest
        {
            Status = status
        };

        ValidationResult result =
            await _validator.ValidateAsync(
                request,
                TestContext.Current.CancellationToken);

        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public async Task ValidateAsync_WithInvalidStatus_ShouldBeInvalid()
    {
        var request = new UpdateBudgetAccountRequest
        {
            Status = (BudgetAccountStatus)999
        };

        ValidationResult result =
            await _validator.ValidateAsync(
                request,
                TestContext.Current.CancellationToken);

        Assert.False(result.IsValid);

        Assert.Contains(
            result.Errors,
            error =>
                error.PropertyName ==
                nameof(UpdateBudgetAccountRequest.Status));
    }
}
