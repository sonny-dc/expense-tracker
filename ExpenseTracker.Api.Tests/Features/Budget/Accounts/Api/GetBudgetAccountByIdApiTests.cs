using System.Net;
using System.Net.Http.Json;

using ExpenseTracker.Api.Features.Budget.Accounts;
using ExpenseTracker.Api.Tests.Features.Budget.Helpers;
using ExpenseTracker.Api.Tests.Infrastructure;

namespace ExpenseTracker.Api.Tests.Features.Budget.Accounts.Api;

public sealed class GetBudgetAccountByIdApiTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public GetBudgetAccountByIdApiTests(
        CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetByIdAsync_WithExistingBudgetAccount_ShouldReturnBudgetAccount()
    {
        BudgetAccount createdAccount =
            await BudgetApiTestHelper.CreateBudgetAccountAsync(
                _client,
                balance: 875.00m);

        try
        {
            HttpResponseMessage response =
                await _client.GetAsync(
                    $"/api/v1/budget/accounts/" +
                    $"{createdAccount.BudgetAccountId}",
                    TestContext.Current.CancellationToken);

            Assert.Equal(
                HttpStatusCode.OK,
                response.StatusCode);

            BudgetAccount? retrievedAccount =
                await response.Content.ReadFromJsonAsync<
                    BudgetAccount>(
                        TestJsonSerializerOptions.Default,
                        TestContext.Current.CancellationToken);

            Assert.NotNull(retrievedAccount);

            Assert.Equal(
                createdAccount.BudgetAccountId,
                retrievedAccount.BudgetAccountId);

            Assert.Equal(
                createdAccount.Name,
                retrievedAccount.Name);

            Assert.Equal(
                createdAccount.Description,
                retrievedAccount.Description);

            Assert.Equal(
                createdAccount.Balance,
                retrievedAccount.Balance);

            Assert.Equal(
                createdAccount.Status,
                retrievedAccount.Status);

            Assert.Equal(
                createdAccount.CreatedDateTime,
                retrievedAccount.CreatedDateTime);

            Assert.Equal(
                createdAccount.LastUpdatedDateTime,
                retrievedAccount.LastUpdatedDateTime);
        }
        finally
        {
            await BudgetApiTestHelper
                .DeleteBudgetAccountIfPresentAsync(
                    _client,
                    createdAccount.BudgetAccountId);
        }
    }

    [Fact]
    public async Task GetByIdAsync_WithMissingBudgetAccount_ShouldReturnNotFound()
    {
        const int missingBudgetAccountId = int.MaxValue;

        HttpResponseMessage response =
            await _client.GetAsync(
                $"/api/v1/budget/accounts/" +
                $"{missingBudgetAccountId}",
                TestContext.Current.CancellationToken);

        Assert.Equal(
            HttpStatusCode.NotFound,
            response.StatusCode);
    }
}
