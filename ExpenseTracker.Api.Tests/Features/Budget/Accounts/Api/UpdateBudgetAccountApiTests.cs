using System.Net;
using System.Net.Http.Json;

using ExpenseTracker.Api.Features.Budget.Accounts;
using ExpenseTracker.Api.Features.Budget.Accounts.Requests;
using ExpenseTracker.Api.Features.Budget.Logs;
using ExpenseTracker.Api.Tests.Features.Budget.Helpers;
using ExpenseTracker.Api.Tests.Infrastructure;

namespace ExpenseTracker.Api.Tests.Features.Budget.Accounts.Api;

public sealed class UpdateBudgetAccountApiTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public UpdateBudgetAccountApiTests(
        CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task UpdateAsync_WithBalanceChange_ShouldReturnUpdatedBudgetAccount()
    {
        BudgetAccount account =
            await BudgetApiTestHelper.CreateBudgetAccountAsync(
                _client,
                balance: 1000.00m);

        var request = new UpdateBudgetAccountRequest
        {
            Balance = 1500.00m
        };

        try
        {
            HttpResponseMessage response =
                await _client.PatchAsJsonAsync(
                    $"/api/v1/budget/accounts/" +
                    $"{account.BudgetAccountId}",
                    request,
                    TestContext.Current.CancellationToken);

            Assert.Equal(
                HttpStatusCode.OK,
                response.StatusCode);

            BudgetAccount? updatedAccount =
                await response.Content.ReadFromJsonAsync<
                    BudgetAccount>(
                        TestJsonSerializerOptions.Default,
                        TestContext.Current.CancellationToken);

            Assert.NotNull(updatedAccount);

            Assert.Equal(
                account.BudgetAccountId,
                updatedAccount.BudgetAccountId);

            Assert.Equal(
                account.Name,
                updatedAccount.Name);

            Assert.Equal(
                account.Description,
                updatedAccount.Description);

            Assert.Equal(
                request.Balance,
                updatedAccount.Balance);

            Assert.Equal(
                account.Status,
                updatedAccount.Status);
        }
        finally
        {
            await BudgetApiTestHelper
                .DeleteBudgetAccountIfPresentAsync(
                    _client,
                    account.BudgetAccountId);
        }
    }

    [Fact]
    public async Task UpdateAsync_WithBalanceChange_ShouldCreateAdjustmentLog()
    {
        BudgetAccount account =
            await BudgetApiTestHelper.CreateBudgetAccountAsync(
                _client,
                balance: 1000.00m);

        var request = new UpdateBudgetAccountRequest
        {
            Balance = 1500.00m
        };

        try
        {
            HttpResponseMessage response =
                await _client.PatchAsJsonAsync(
                    $"/api/v1/budget/accounts/" +
                    $"{account.BudgetAccountId}",
                    request,
                    TestJsonSerializerOptions.Default,
                    TestContext.Current.CancellationToken);

            Assert.Equal(
                HttpStatusCode.OK,
                response.StatusCode);

            IReadOnlyList<BudgetAccountLog> logs =
                await BudgetApiTestHelper
                    .GetLogsByBudgetAccountIdAsync(
                        _client,
                        account.BudgetAccountId);

            Assert.Equal(
                2,
                logs.Count);

            BudgetAccountLog adjustmentLog =
                Assert.Single(
                    logs,
                    log =>
                        log.LogType ==
                        BudgetAccountLogType.Adjustment);

            Assert.Equal(
                account.BudgetAccountId,
                adjustmentLog.BudgetAccountId);

            Assert.Null(
                adjustmentLog.ExpenseEntryId);

            Assert.Equal(
                500.00m,
                adjustmentLog.Amount);

            Assert.Equal(
                1000.00m,
                adjustmentLog.BalanceBefore);

            Assert.Equal(
                1500.00m,
                adjustmentLog.BalanceAfter);
        }
        finally
        {
            await BudgetApiTestHelper
                .DeleteBudgetAccountIfPresentAsync(
                    _client,
                    account.BudgetAccountId);
        }
    }

    [Fact]
    public async Task UpdateAsync_WithMetadataAndStatus_ShouldNotCreateAdjustmentLog()
    {
        BudgetAccount account =
            await BudgetApiTestHelper.CreateBudgetAccountAsync(
                _client,
                balance: 1000.00m);

        var request = new UpdateBudgetAccountRequest
        {
            Name = "Updated Budget Account",
            Description = "Updated budget account description",
            Status = BudgetAccountStatus.Archived
        };

        try
        {
            HttpResponseMessage response =
                await _client.PatchAsJsonAsync(
                    $"/api/v1/budget/accounts/" +
                    $"{account.BudgetAccountId}",
                    request,
                    TestJsonSerializerOptions.Default,
                    TestContext.Current.CancellationToken);

            Assert.Equal(
                HttpStatusCode.OK,
                response.StatusCode);

            BudgetAccount? updatedAccount =
                await response.Content.ReadFromJsonAsync<
                    BudgetAccount>(
                        TestJsonSerializerOptions.Default,
                        TestContext.Current.CancellationToken);

            Assert.NotNull(updatedAccount);

            Assert.Equal(
                request.Name,
                updatedAccount.Name);

            Assert.Equal(
                request.Description,
                updatedAccount.Description);

            Assert.Equal(
                account.Balance,
                updatedAccount.Balance);

            Assert.Equal(
                BudgetAccountStatus.Archived,
                updatedAccount.Status);

            IReadOnlyList<BudgetAccountLog> logs =
                await BudgetApiTestHelper
                    .GetLogsByBudgetAccountIdAsync(
                        _client,
                        account.BudgetAccountId);

            BudgetAccountLog log =
                Assert.Single(logs);

            Assert.Equal(
                BudgetAccountLogType.StartingBalance,
                log.LogType);
        }
        finally
        {
            await BudgetApiTestHelper
                .DeleteBudgetAccountIfPresentAsync(
                    _client,
                    account.BudgetAccountId);
        }
    }

    [Fact]
    public async Task UpdateAsync_WithArchivedBudgetAccount_ShouldReturnUpdatedBudgetAccount()
    {
        BudgetAccount account =
            await BudgetApiTestHelper.CreateBudgetAccountAsync(
                _client,
                balance: 1000.00m);

        try
        {
            var archiveRequest = new UpdateBudgetAccountRequest
            {
                Status = BudgetAccountStatus.Archived
            };

            HttpResponseMessage archiveResponse =
                await _client.PatchAsJsonAsync(
                    $"/api/v1/budget/accounts/" +
                    $"{account.BudgetAccountId}",
                    archiveRequest,
                    TestJsonSerializerOptions.Default,
                    TestContext.Current.CancellationToken);

            Assert.Equal(
                HttpStatusCode.OK,
                archiveResponse.StatusCode);

            var balanceRequest = new UpdateBudgetAccountRequest
            {
                Balance = 1250.00m
            };

            HttpResponseMessage response =
                await _client.PatchAsJsonAsync(
                    $"/api/v1/budget/accounts/" +
                    $"{account.BudgetAccountId}",
                    balanceRequest,
                    TestContext.Current.CancellationToken);

            Assert.Equal(
                HttpStatusCode.OK,
                response.StatusCode);

            BudgetAccount? updatedAccount =
                await response.Content.ReadFromJsonAsync<
                    BudgetAccount>(
                        TestJsonSerializerOptions.Default,
                        TestContext.Current.CancellationToken);

            Assert.NotNull(updatedAccount);

            Assert.Equal(
                1250.00m,
                updatedAccount.Balance);

            Assert.Equal(
                BudgetAccountStatus.Archived,
                updatedAccount.Status);
        }
        finally
        {
            await BudgetApiTestHelper
                .DeleteBudgetAccountIfPresentAsync(
                    _client,
                    account.BudgetAccountId);
        }
    }

    [Fact]
    public async Task UpdateAsync_WithSettledBudgetAccount_ShouldReturnConflict()
    {
        BudgetAccount account =
            await BudgetApiTestHelper.CreateBudgetAccountAsync(
                _client,
                balance: 1000.00m);

        try
        {
            var settleRequest = new UpdateBudgetAccountRequest
            {
                Status = BudgetAccountStatus.Settled
            };

            HttpResponseMessage settleResponse =
                await _client.PatchAsJsonAsync(
                    $"/api/v1/budget/accounts/" +
                    $"{account.BudgetAccountId}",
                    settleRequest,
                    TestJsonSerializerOptions.Default,
                    TestContext.Current.CancellationToken);

            Assert.Equal(
                HttpStatusCode.OK,
                settleResponse.StatusCode);

            var updateRequest = new UpdateBudgetAccountRequest
            {
                Balance = 2000.00m
            };

            HttpResponseMessage response =
                await _client.PatchAsJsonAsync(
                    $"/api/v1/budget/accounts/" +
                    $"{account.BudgetAccountId}",
                    updateRequest,
                    TestContext.Current.CancellationToken);

            Assert.Equal(
                HttpStatusCode.Conflict,
                response.StatusCode);

            BudgetAccount unchangedAccount =
                await BudgetApiTestHelper.GetBudgetAccountAsync(
                    _client,
                    account.BudgetAccountId);

            Assert.Equal(
                1000.00m,
                unchangedAccount.Balance);

            Assert.Equal(
                BudgetAccountStatus.Settled,
                unchangedAccount.Status);
        }
        finally
        {
            await BudgetApiTestHelper
                .DeleteBudgetAccountIfPresentAsync(
                    _client,
                    account.BudgetAccountId);
        }
    }

    [Fact]
    public async Task UpdateAsync_WithMissingBudgetAccount_ShouldReturnNotFound()
    {
        const int missingBudgetAccountId = int.MaxValue;

        var request = new UpdateBudgetAccountRequest
        {
            Balance = 1000.00m
        };

        HttpResponseMessage response =
            await _client.PatchAsJsonAsync(
                $"/api/v1/budget/accounts/" +
                $"{missingBudgetAccountId}",
                request,
                TestContext.Current.CancellationToken);

        Assert.Equal(
            HttpStatusCode.NotFound,
            response.StatusCode);
    }

    [Fact]
    public async Task UpdateAsync_WithInvalidStatus_ShouldReturnBadRequest()
    {
        BudgetAccount account =
            await BudgetApiTestHelper.CreateBudgetAccountAsync(
                _client);

        try
        {
            var request = new
            {
                status = "InvalidStatus"
            };

            HttpResponseMessage response =
                await _client.PatchAsJsonAsync(
                    $"/api/v1/budget/accounts/" +
                    $"{account.BudgetAccountId}",
                    request,
                    TestContext.Current.CancellationToken);

            Assert.Equal(
                HttpStatusCode.BadRequest,
                response.StatusCode);
        }
        finally
        {
            await BudgetApiTestHelper
                .DeleteBudgetAccountIfPresentAsync(
                    _client,
                    account.BudgetAccountId);
        }
    }

    [Fact]
    public async Task UpdateAsync_WithNumericStatus_ShouldReturnBadRequest()
    {
        BudgetAccount account =
            await BudgetApiTestHelper.CreateBudgetAccountAsync(
                _client);

        try
        {
            var request = new
            {
                status = 1
            };

            HttpResponseMessage response =
                await _client.PatchAsJsonAsync(
                    $"/api/v1/budget/accounts/" +
                    $"{account.BudgetAccountId}",
                    request,
                    TestContext.Current.CancellationToken);

            Assert.Equal(
                HttpStatusCode.BadRequest,
                response.StatusCode);
        }
        finally
        {
            await BudgetApiTestHelper
                .DeleteBudgetAccountIfPresentAsync(
                    _client,
                    account.BudgetAccountId);
        }
    }

    [Fact]
    public async Task UpdateAsync_WithUnknownProperty_ShouldReturnBadRequest()
    {
        BudgetAccount account =
            await BudgetApiTestHelper.CreateBudgetAccountAsync(
                _client);

        try
        {
            var request = new
            {
                balance = 1500.00m,
                unexpectedProperty = true
            };

            HttpResponseMessage response =
                await _client.PatchAsJsonAsync(
                    $"/api/v1/budget/accounts/" +
                    $"{account.BudgetAccountId}",
                    request,
                    TestContext.Current.CancellationToken);

            Assert.Equal(
                HttpStatusCode.BadRequest,
                response.StatusCode);
        }
        finally
        {
            await BudgetApiTestHelper
                .DeleteBudgetAccountIfPresentAsync(
                    _client,
                    account.BudgetAccountId);
        }
    }
}
