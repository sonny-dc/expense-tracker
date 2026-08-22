using Dapper;
using ExpenseTracker.Api.Infrastructure.Database;

namespace ExpenseTracker.Api.Features.Expenses.Entries;

public sealed class ExpenseEntryRepository
{
    private readonly DatabaseExecutor _databaseExecutor;
    public ExpenseEntryRepository(DatabaseExecutor databaseExecutor)
    {
        _databaseExecutor = databaseExecutor;
    }

    public async Task<IReadOnlyList<ExpenseEntry>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        const string sql = """
            SELECT
                ExpenseEntryId,
                ExpenseDateTime,
                TotalCost,
                Notes
            FROM dbo.ExpenseEntries
            ORDER BY ExpenseDateTime DESC, ExpenseEntryId DESC;
        """;
        return await _databaseExecutor.ExecuteAsync(
            async (connection, transaction) =>
            {
                var command = new CommandDefinition(
                    commandText: sql,
                    transaction: transaction,
                    cancellationToken: cancellationToken);
                IEnumerable<ExpenseEntry> expenseEntries = await connection.QueryAsync<ExpenseEntry>(command);
                return expenseEntries.AsList();
            }
        );
    }

    public async Task<ExpenseEntry?> GetByIdAsync(
        int expenseEntryId,
        CancellationToken cancellationToken = default)
    {
        const string sql = """
            SELECT
                ExpenseEntryId,
                ExpenseDateTime,
                TotalCost,
                Notes
            FROM dbo.ExpenseEntries
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
                return await connection.QuerySingleOrDefaultAsync<ExpenseEntry>(command);
            }
        );
    }

    public async Task<ExpenseEntry> CreateAsync(
        CreateExpenseEntryInput input,
        CancellationToken cancellationToken = default)
    {
        const string sql = """
            INSERT INTO dbo.ExpenseEntries
            (
                TotalCost,
                Notes
            )
            OUTPUT
                INSERTED.ExpenseEntryId,
                INSERTED.ExpenseDateTime,
                INSERTED.TotalCost,
                INSERTED.Notes
            VALUES
            (
                @TotalCost,
                @Notes
            );
        """;
        return await _databaseExecutor.ExecuteAsync(
            async (connection, transaction) =>
            {
                var command = new CommandDefinition(
                    commandText: sql,
                    parameters: new
                    {
                        TotalCost = input.TotalCost,
                        Notes = input.Notes
                    },
                    transaction: transaction,
                    cancellationToken: cancellationToken);
                return await connection.QuerySingleAsync<ExpenseEntry>(command);
            }
        );
    }
}
