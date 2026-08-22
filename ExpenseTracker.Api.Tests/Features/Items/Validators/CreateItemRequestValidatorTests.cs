using FluentValidation.Results;

using ExpenseTracker.Api.Features.Items;
using ExpenseTracker.Api.Features.Items.Validators;

namespace ExpenseTracker.Api.Tests.Features.Items.Validators;

public sealed class CreateItemRequestValidatorTests
{
    private readonly CreateItemRequestValidator _validator = new();

    [Fact]
    public async Task ValidateAsync_WithValidRequest_ShouldBeValid()
    {
        CreateItemRequest request = CreateValidRequest();

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
        CreateItemRequest request = CreateValidRequest();
        request.Name = name;

        ValidationResult result =
            await _validator.ValidateAsync(
                request,
                TestContext.Current.CancellationToken);

        Assert.False(result.IsValid);
        Assert.Contains(
            result.Errors,
            error => error.PropertyName == nameof(CreateItemRequest.Name));
    }

    [Fact]
    public async Task ValidateAsync_WithNameLongerThan100Characters_ShouldBeInvalid()
    {
        CreateItemRequest request = CreateValidRequest();
        request.Name = new string('A', 101);

        ValidationResult result =
            await _validator.ValidateAsync(
                request,
                TestContext.Current.CancellationToken);

        Assert.False(result.IsValid);
        Assert.Contains(
            result.Errors,
            error => error.PropertyName == nameof(CreateItemRequest.Name));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public async Task ValidateAsync_WithEmptyCode_ShouldBeInvalid(
        string code)
    {
        CreateItemRequest request = CreateValidRequest();
        request.Code = code;

        ValidationResult result =
            await _validator.ValidateAsync(
                request,
                TestContext.Current.CancellationToken);

        Assert.False(result.IsValid);
        Assert.Contains(
            result.Errors,
            error => error.PropertyName == nameof(CreateItemRequest.Code));
    }

    [Fact]
    public async Task ValidateAsync_WithCodeLongerThan30Characters_ShouldBeInvalid()
    {
        CreateItemRequest request = CreateValidRequest();
        request.Code = new string('A', 31);

        ValidationResult result =
            await _validator.ValidateAsync(
                request,
                TestContext.Current.CancellationToken);

        Assert.False(result.IsValid);
        Assert.Contains(
            result.Errors,
            error => error.PropertyName == nameof(CreateItemRequest.Code));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public async Task ValidateAsync_WithEmptyBrand_ShouldBeInvalid(
        string brand)
    {
        CreateItemRequest request = CreateValidRequest();
        request.Brand = brand;

        ValidationResult result =
            await _validator.ValidateAsync(
                request,
                TestContext.Current.CancellationToken);

        Assert.False(result.IsValid);
        Assert.Contains(
            result.Errors,
            error => error.PropertyName == nameof(CreateItemRequest.Brand));
    }

    [Fact]
    public async Task ValidateAsync_WithBrandLongerThan100Characters_ShouldBeInvalid()
    {
        CreateItemRequest request = CreateValidRequest();
        request.Brand = new string('A', 101);

        ValidationResult result =
            await _validator.ValidateAsync(
                request,
                TestContext.Current.CancellationToken);

        Assert.False(result.IsValid);
        Assert.Contains(
            result.Errors,
            error => error.PropertyName == nameof(CreateItemRequest.Brand));
    }

    [Fact]
    public async Task ValidateAsync_WithNegativeUnitPrice_ShouldBeInvalid()
    {
        CreateItemRequest request = CreateValidRequest();
        request.UnitPrice = -0.01m;

        ValidationResult result =
            await _validator.ValidateAsync(
                request,
                TestContext.Current.CancellationToken);

        Assert.False(result.IsValid);
        Assert.Contains(
            result.Errors,
            error => error.PropertyName == nameof(CreateItemRequest.UnitPrice));
    }

    [Fact]
    public async Task ValidateAsync_WithZeroUnitPrice_ShouldBeValid()
    {
        CreateItemRequest request = CreateValidRequest();
        request.UnitPrice = 0m;

        ValidationResult result =
            await _validator.ValidateAsync(
                request,
                TestContext.Current.CancellationToken);

        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    private static CreateItemRequest CreateValidRequest()
    {
        return new CreateItemRequest
        {
            Name = "Bond Paper",
            Code = "BP-A5",
            Brand = "PaperOne",
            UnitPrice = 250.00m
        };
    }
}
