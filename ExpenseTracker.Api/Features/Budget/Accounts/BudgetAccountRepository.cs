using Dapper;
using ExpenseTracker.Api.Infrastructure.Database;
using ExpenseTracker.Api.Features.Budget.Accounts.Requests;

namespace ExpenseTracker.Api.Features.Budget.Accounts;

public sealed class BudgetAccountRepository
{
    private readonly DatabaseExecutor _databaseExecutor;
    public BudgetAccountRepository(DatabaseExecutor databaseExecutor)
    {
        _databaseExecutor = databaseExecutor;
    }

    public async Task<IReadOnlyList<BudgetAccount>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        const string sql = """
            SELECT
                BudgetAccountId,
                Name,
                Description,
                Balance,
                Status,
                CreatedDateTime,
                LastUpdatedDateTime
            FROM BudgetAccounts
            ORDER BY Name ASC;
        """;

        return await _databaseExecutor.ExecuteAsync(
            async (connection, transaction) =>
            {
                var command = new CommandDefinition(
                    commandText: sql,
                    transaction: transaction,
                    cancellationToken: cancellationToken);
                IEnumerable<BudgetAccount> budgetAccounts = await connection.QueryAsync<BudgetAccount>(command);
                return budgetAccounts.AsList();
            }
        );
    }

    public async Task<BudgetAccount?> GetByIdAsync(
        int budgetAccountId,
        CancellationToken cancellationToken = default)
    {
        const string sql = """
            SELECT
                BudgetAccountId,
                Name,
                Description,
                Balance,
                Status,
                CreatedDateTime,
                LastUpdatedDateTime
            FROM BudgetAccounts
            WHERE BudgetAccountId = @BudgetAccountId;
        """;

        return await _databaseExecutor.ExecuteAsync(
            async (connection, transaction) =>
            {
                var command = new CommandDefinition(
                    commandText: sql,
                    parameters: new { BudgetAccountId = budgetAccountId },
                    transaction: transaction,
                    cancellationToken: cancellationToken);
                return await connection.QuerySingleOrDefaultAsync<BudgetAccount>(command);
            }
        );
    }

    public async Task<BudgetAccount> CreateAsync(
        CreateBudgetAccountRequest request,
        CancellationToken cancellationToken = default)
    {
        const string sql = """
            INSERT INTO BudgetAccounts
            (
                Name, 
                Description,
                Balance, 
                Status
            )
            OUTPUT
                INSERTED.BudgetAccountId,
                INSERTED.Name,
                INSERTED.Description,
                INSERTED.Balance,
                INSERTED.Status,
                INSERTED.CreatedDateTime,
                INSERTED.LastUpdatedDateTime
            VALUES
            (
                @Name,
                @Description,
                @Balance,
                @Status
            );
        """;

        return await _databaseExecutor.ExecuteAsync(
            async (connection, transaction) =>
            {
                var command = new CommandDefinition(
                    commandText: sql,
                    parameters: new
                    {
                        Name = request.Name,
                        Description = request.Description,
                        Balance = request.Balance,
                        Status = BudgetAccountStatus.Open.ToString()
                    },
                    transaction: transaction,
                    cancellationToken: cancellationToken);
                return await connection.QuerySingleAsync<BudgetAccount>(command);
            }
        );
    }

    public async Task<bool> UpdateAsync(
        int budgetAccountId,
        UpdateBudgetAccountRequest request,
        CancellationToken cancellationToken = default)
    {
        const string sql = """
            UPDATE BudgetAccounts
            SET
                Name = COALESCE(@Name, Name),
                Description = COALESCE(@Description, Description),
                Balance = COALESCE(@Balance, Balance),
                Status = COALESCE(@Status, Status),
                LastUpdatedDateTime = SYSUTCDATETIME()
            WHERE BudgetAccountId = @BudgetAccountId;
        """;

        return await _databaseExecutor.ExecuteAsync(
            async (connection, transaction) =>
            {
                var command = new CommandDefinition(
                    commandText: sql,
                    parameters: new
                    {
                        BudgetAccountId = budgetAccountId,
                        Name = request.Name,
                        Description = request.Description,
                        Balance = request.Balance,
                        Status = request.Status?.ToString()
                    },
                    transaction: transaction,
                    cancellationToken: cancellationToken);
                int affectedRows =
                    await connection.ExecuteAsync(command);

                return affectedRows > 0;
            }
        );
    }

    public async Task<bool> DeleteAsync(
        int budgetAccountId,
        CancellationToken cancellationToken = default)
    {
        const string sql = """
            DELETE FROM BudgetAccounts
            WHERE BudgetAccountId = @BudgetAccountId;
        """;

        return await _databaseExecutor.ExecuteAsync(
            async (connection, transaction) =>
            {
                var command = new CommandDefinition(
                    commandText: sql,
                    parameters: new { BudgetAccountId = budgetAccountId },
                    transaction: transaction,
                    cancellationToken: cancellationToken);
                int affectedRows =
                    await connection.ExecuteAsync(command);

                return affectedRows > 0;
            }
        );
    }
}
