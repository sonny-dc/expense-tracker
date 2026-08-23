using FluentValidation.Results;

using ExpenseTracker.Api.Features.Items;
using ExpenseTracker.Api.Features.Items.Validators;

namespace ExpenseTracker.Api.Tests.Features.Items.Validators;

public sealed class UpdateItemRequestValidatorTests
{
    private readonly UpdateItemRequestValidator _validator = new();

    [Fact]
    public async Task ValidateAsync_WithOneFieldProvided_ShouldBeValid()
    {
        var request = new UpdateItemRequest
        {
            UnitPrice = 275.00m
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
        var request = new UpdateItemRequest
        {
            Name = "Premium Bond Paper",
            Code = "BP-A4",
            Brand = "PaperOne",
            UnitPrice = 275.00m
        };

        ValidationResult result =
            await _validator.ValidateAsync(
                request,
                TestContext.Current.CancellationToken);

        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public async Task ValidateAsync_WithNoFieldsProvided_ShouldBeInvalid()
    {
        var request = new UpdateItemRequest();

        ValidationResult result =
            await _validator.ValidateAsync(
                request,
                TestContext.Current.CancellationToken);

        Assert.False(result.IsValid);
        Assert.Contains(
            result.Errors,
            error => error.ErrorMessage ==
                "At least one item field must be provided.");
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public async Task ValidateAsync_WithEmptyName_ShouldBeInvalid(
        string name)
    {
        var request = new UpdateItemRequest
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
            error => error.PropertyName == nameof(UpdateItemRequest.Name));
    }

    [Fact]
    public async Task ValidateAsync_WithNameLongerThan100Characters_ShouldBeInvalid()
    {
        var request = new UpdateItemRequest
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
            error => error.PropertyName == nameof(UpdateItemRequest.Name));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public async Task ValidateAsync_WithEmptyCode_ShouldBeInvalid(
        string code)
    {
        var request = new UpdateItemRequest
        {
            Code = code
        };

        ValidationResult result =
            await _validator.ValidateAsync(
                request,
                TestContext.Current.CancellationToken);

        Assert.False(result.IsValid);
        Assert.Contains(
            result.Errors,
            error => error.PropertyName == nameof(UpdateItemRequest.Code));
    }

    [Fact]
    public async Task ValidateAsync_WithCodeLongerThan30Characters_ShouldBeInvalid()
    {
        var request = new UpdateItemRequest
        {
            Code = new string('A', 31)
        };

        ValidationResult result =
            await _validator.ValidateAsync(
                request,
                TestContext.Current.CancellationToken);

        Assert.False(result.IsValid);
        Assert.Contains(
            result.Errors,
            error => error.PropertyName == nameof(UpdateItemRequest.Code));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public async Task ValidateAsync_WithEmptyBrand_ShouldBeInvalid(
        string brand)
    {
        var request = new UpdateItemRequest
        {
            Brand = brand
        };

        ValidationResult result =
            await _validator.ValidateAsync(
                request,
                TestContext.Current.CancellationToken);

        Assert.False(result.IsValid);
        Assert.Contains(
            result.Errors,
            error => error.PropertyName == nameof(UpdateItemRequest.Brand));
    }

    [Fact]
    public async Task ValidateAsync_WithBrandLongerThan100Characters_ShouldBeInvalid()
    {
        var request = new UpdateItemRequest
        {
            Brand = new string('A', 101)
        };

        ValidationResult result =
            await _validator.ValidateAsync(
                request,
                TestContext.Current.CancellationToken);

        Assert.False(result.IsValid);
        Assert.Contains(
            result.Errors,
            error => error.PropertyName == nameof(UpdateItemRequest.Brand));
    }

    [Fact]
    public async Task ValidateAsync_WithNegativeUnitPrice_ShouldBeInvalid()
    {
        var request = new UpdateItemRequest
        {
            UnitPrice = -0.01m
        };

        ValidationResult result =
            await _validator.ValidateAsync(
                request,
                TestContext.Current.CancellationToken);

        Assert.False(result.IsValid);
        Assert.Contains(
            result.Errors,
            error => error.PropertyName ==
                nameof(UpdateItemRequest.UnitPrice));
    }

    [Fact]
    public async Task ValidateAsync_WithUnitPriceHavingMoreThanTwoDecimalPlaces_ShouldBeInvalid()
    {
        var request = new UpdateItemRequest
        {
            UnitPrice = 250.123m
        };

        ValidationResult result =
            await _validator.ValidateAsync(
                request,
                TestContext.Current.CancellationToken);

        Assert.False(result.IsValid);

        Assert.Contains(
            result.Errors,
            error => error.PropertyName ==
                nameof(UpdateItemRequest.UnitPrice));
    }

    [Fact]
    public async Task ValidateAsync_WithZeroUnitPrice_ShouldBeValid()
    {
        var request = new UpdateItemRequest
        {
            UnitPrice = 0m
        };

        ValidationResult result =
            await _validator.ValidateAsync(
                request,
                TestContext.Current.CancellationToken);

        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }
}
