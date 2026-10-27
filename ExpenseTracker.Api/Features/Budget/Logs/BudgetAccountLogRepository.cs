using Dapper;

using ExpenseTracker.Api.Infrastructure.Database;

namespace ExpenseTracker.Api.Features.Budget.Logs;

public sealed class BudgetAccountLogRepository
{
    private readonly DatabaseExecutor _databaseExecutor;

    public BudgetAccountLogRepository(DatabaseExecutor databaseExecutor)
    {
        _databaseExecutor = databaseExecutor;
    }

    public async Task<IReadOnlyList<BudgetAccountLog>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        const string sql = """
            SELECT
                BudgetAccountLogId,
                BudgetAccountId,
                ExpenseEntryId,
                Amount,
                BalanceBefore,
                BalanceAfter,
                LogType,
                LogDateTime
            FROM BudgetAccountLogs
            ORDER BY LogDateTime DESC;
        """;

        return await _databaseExecutor.ExecuteAsync(
            async (connection, transaction) =>
            {
                var command = new CommandDefinition(
                    commandText: sql,
                    transaction: transaction,
                    cancellationToken: cancellationToken);
                IEnumerable<BudgetAccountLog> budgetAccountLogs = await connection.QueryAsync<BudgetAccountLog>(command);
                return budgetAccountLogs.AsList();
            }
        );
    }

    public async Task<IReadOnlyList<BudgetAccountLog>> GetAllByBudgetAccountIdAsync(
        int budgetAccountId,
        CancellationToken cancellationToken)
    {
        const string sql = """
            SELECT
                BudgetAccountLogId,
                BudgetAccountId,
                ExpenseEntryId,
                Amount,
                BalanceBefore,
                BalanceAfter,
                LogType,
                LogDateTime
            FROM BudgetAccountLogs
            WHERE BudgetAccountId = @BudgetAccountId
            ORDER BY LogDateTime DESC;
        """;

        return await _databaseExecutor.ExecuteAsync(
            async (connection, transaction) =>
            {
                var command = new CommandDefinition(
                    commandText: sql,
                    parameters: new { BudgetAccountId = budgetAccountId },
                    transaction: transaction,
                    cancellationToken: cancellationToken);
                IEnumerable<BudgetAccountLog> budgetAccountLogs = await connection.QueryAsync<BudgetAccountLog>(command);
                return budgetAccountLogs.AsList();
            }
        );
    }

    public async Task<BudgetAccountLog?> GetByExpenseEntryIdAsync(
        int expenseEntryId,
        CancellationToken cancellationToken = default)
    {
        const string sql = """
            SELECT
                BudgetAccountLogId,
                BudgetAccountId,
                ExpenseEntryId,
                Amount,
                BalanceBefore,
                BalanceAfter,
                LogType,
                LogDateTime
            FROM BudgetAccountLogs
            WHERE ExpenseEntryId = @ExpenseEntryId;
        """;

        return await _databaseExecutor.ExecuteAsync(
            async (connection, transaction) =>
            {
                var command = new CommandDefinition(
                    commandText: sql,
                    parameters: new { ExpenseEntryId = expenseEntryId },
                    transaction: transaction,
                    cancellationToken: cancellationToken);
                return await connection.QuerySingleOrDefaultAsync<BudgetAccountLog>(command);
            }
        );
    }

    public async Task<BudgetAccountLog> CreateAsync(
        CreateBudgetAccountLogInput input,
        CancellationToken cancellationToken = default)
    {
        const string sql = """
            INSERT INTO BudgetAccountLogs
            (
                BudgetAccountId,
                ExpenseEntryId,
                Amount,
                BalanceBefore,
                BalanceAfter,
                LogType
            )
            OUTPUT
                INSERTED.BudgetAccountLogId,
                INSERTED.BudgetAccountId,
                INSERTED.ExpenseEntryId,
                INSERTED.Amount,
                INSERTED.BalanceBefore,
                INSERTED.BalanceAfter,
                INSERTED.LogType,
                INSERTED.LogDateTime
            VALUES
            (
                @BudgetAccountId,
                @ExpenseEntryId,
                @Amount,
                @BalanceBefore,
                @BalanceAfter,
                @LogType
            );
        """;

        return await _databaseExecutor.ExecuteAsync(
            async (connection, transaction) =>
            {
                var command = new CommandDefinition(
                    commandText: sql,
                    parameters: new
                    {
                        BudgetAccountId = input.BudgetAccountId,
                        ExpenseEntryId = input.ExpenseEntryId,
                        Amount = input.Amount,
                        BalanceBefore = input.BalanceBefore,
                        BalanceAfter = input.BalanceAfter,
                        LogType = input.LogType.ToString()
                    },
                    transaction: transaction,
                    cancellationToken: cancellationToken);
                return await connection.QuerySingleAsync<BudgetAccountLog>(command);
            }
        );
    }
}
