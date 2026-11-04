using FluentValidation.Results;

using ExpenseTracker.Api.Features.Budget.Accounts.Requests;
using ExpenseTracker.Api.Features.Budget.Accounts.Validators;

namespace ExpenseTracker.Api.Tests.Features.Budget.Accounts.Validators;

public sealed class CreateBudgetAccountRequestValidatorTests
{
    private readonly CreateBudgetAccountRequestValidator _validator =
        new();

    [Fact]
    public async Task ValidateAsync_WithValidRequest_ShouldBeValid()
    {
        CreateBudgetAccountRequest request =
            CreateValidRequest();

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
        CreateBudgetAccountRequest request =
            CreateValidRequest();

        request.Name = name;

        ValidationResult result =
            await _validator.ValidateAsync(
                request,
                TestContext.Current.CancellationToken);

        Assert.False(result.IsValid);

        Assert.Contains(
            result.Errors,
            error =>
                error.PropertyName ==
                nameof(CreateBudgetAccountRequest.Name));
    }

    [Fact]
    public async Task ValidateAsync_WithNameHaving100Characters_ShouldBeValid()
    {
        CreateBudgetAccountRequest request =
            CreateValidRequest();

        request.Name = new string('A', 100);

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
        CreateBudgetAccountRequest request =
            CreateValidRequest();

        request.Name = new string('A', 101);

        ValidationResult result =
            await _validator.ValidateAsync(
                request,
                TestContext.Current.CancellationToken);

        Assert.False(result.IsValid);

        Assert.Contains(
            result.Errors,
            error =>
                error.PropertyName ==
                nameof(CreateBudgetAccountRequest.Name));
    }

    [Fact]
    public async Task ValidateAsync_WithNullDescription_ShouldBeValid()
    {
        CreateBudgetAccountRequest request =
            CreateValidRequest();

        request.Description = null;

        ValidationResult result =
            await _validator.ValidateAsync(
                request,
                TestContext.Current.CancellationToken);

        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public async Task ValidateAsync_WithDescriptionHaving500Characters_ShouldBeValid()
    {
        CreateBudgetAccountRequest request =
            CreateValidRequest();

        request.Description = new string('A', 500);

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
        CreateBudgetAccountRequest request =
            CreateValidRequest();

        request.Description = new string('A', 501);

        ValidationResult result =
            await _validator.ValidateAsync(
                request,
                TestContext.Current.CancellationToken);

        Assert.False(result.IsValid);

        Assert.Contains(
            result.Errors,
            error =>
                error.PropertyName ==
                nameof(CreateBudgetAccountRequest.Description));
    }

    [Fact]
    public async Task ValidateAsync_WithZeroBalance_ShouldBeValid()
    {
        CreateBudgetAccountRequest request =
            CreateValidRequest();

        request.Balance = 0m;

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
        CreateBudgetAccountRequest request =
            CreateValidRequest();

        request.Balance = -500.00m;

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
        CreateBudgetAccountRequest request =
            CreateValidRequest();

        request.Balance = 1000.12m;

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
        CreateBudgetAccountRequest request =
            CreateValidRequest();

        request.Balance = 1000.123m;

        ValidationResult result =
            await _validator.ValidateAsync(
                request,
                TestContext.Current.CancellationToken);

        Assert.False(result.IsValid);

        Assert.Contains(
            result.Errors,
            error =>
                error.PropertyName ==
                nameof(CreateBudgetAccountRequest.Balance));
    }

    private static CreateBudgetAccountRequest CreateValidRequest()
    {
        return new CreateBudgetAccountRequest
        {
            Name = "Operating Budget",
            Description = "General operating budget",
            Balance = 1000.00m
        };
    }
}
