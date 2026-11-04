using System.Net;
using System.Net.Http.Json;

using ExpenseTracker.Api.Features.Budget.Accounts;
using ExpenseTracker.Api.Tests.Features.Budget.Helpers;
using ExpenseTracker.Api.Tests.Infrastructure;

namespace ExpenseTracker.Api.Tests.Features.Budget.Accounts.Api;

public sealed class GetBudgetAccountsApiTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public GetBudgetAccountsApiTests(
        CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnOkWithBudgetAccounts()
    {
        HttpResponseMessage response =
            await _client.GetAsync(
                "/api/v1/budget/accounts",
                TestContext.Current.CancellationToken);

        Assert.Equal(
            HttpStatusCode.OK,
            response.StatusCode);

        IReadOnlyList<BudgetAccount>? budgetAccounts =
            await response.Content.ReadFromJsonAsync<
                IReadOnlyList<BudgetAccount>>(
                    TestJsonSerializerOptions.Default,
                    TestContext.Current.CancellationToken);

        Assert.NotNull(budgetAccounts);
    }

    [Fact]
    public async Task GetAllAsync_WithCreatedBudgetAccount_ShouldIncludeBudgetAccount()
    {
        BudgetAccount createdAccount =
            await BudgetApiTestHelper.CreateBudgetAccountAsync(
                _client,
                balance: 750.00m);

        try
        {
            HttpResponseMessage response =
                await _client.GetAsync(
                    "/api/v1/budget/accounts",
                    TestContext.Current.CancellationToken);

            Assert.Equal(
                HttpStatusCode.OK,
                response.StatusCode);

            IReadOnlyList<BudgetAccount>? budgetAccounts =
                await response.Content.ReadFromJsonAsync<
                    IReadOnlyList<BudgetAccount>>(
                        TestJsonSerializerOptions.Default,
                        TestContext.Current.CancellationToken);

            Assert.NotNull(budgetAccounts);

            BudgetAccount retrievedAccount =
                Assert.Single(
                    budgetAccounts,
                    account =>
                        account.BudgetAccountId ==
                        createdAccount.BudgetAccountId);

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
                BudgetAccountStatus.Open,
                retrievedAccount.Status);
        }
        finally
        {
            await BudgetApiTestHelper
                .DeleteBudgetAccountIfPresentAsync(
                    _client,
                    createdAccount.BudgetAccountId);
        }
    }
}
