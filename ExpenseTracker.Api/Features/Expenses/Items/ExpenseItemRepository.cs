using Dapper;
using ExpenseTracker.Api.Infrastructure.Database;

namespace ExpenseTracker.Api.Features.Expenses.Items;

public sealed class ExpenseItemRepository
{
    private readonly DatabaseExecutor _databaseExecutor;
    public ExpenseItemRepository(DatabaseExecutor databaseExecutor)
    {
        _databaseExecutor = databaseExecutor;
    }

    public async Task<IReadOnlyList<ExpenseItem>> GetAllByExpenseEntryIdAsync(
        int expenseEntryId,
        CancellationToken cancellationToken = default)
    {
        const string sql = """
            SELECT
                ExpenseItemId,
                ExpenseEntryId,
                ItemId,
                ItemNameSnapshot,
                ItemCodeSnapshot,
                BrandSnapshot,
                Quantity,
                UnitPriceSnapshot,
                LineTotal
            FROM dbo.ExpenseItems
            WHERE ExpenseEntryId = @ExpenseEntryId
            ORDER BY ExpenseItemId ASC;
        """;
        
        return await _databaseExecutor.ExecuteAsync(
            async (connection, transaction) =>
            {
                var command = new CommandDefinition(
                    commandText: sql,
                    parameters: new { ExpenseEntryId = expenseEntryId },
                    transaction: transaction,
                    cancellationToken: cancellationToken);
                IEnumerable<ExpenseItem> expenseItems = await connection.QueryAsync<ExpenseItem>(command);
                return expenseItems.AsList();
            }
        );
    }

    public async Task<IReadOnlyList<ExpenseItem>> GetAllByExpenseEntryIdsAsync(
        IReadOnlyCollection<int> expenseEntryIds,
        CancellationToken cancellationToken = default)
    {
        const string sql = """
            SELECT
                ExpenseItemId,
                ExpenseEntryId,
                ItemId,
                ItemNameSnapshot,
                ItemCodeSnapshot,
                BrandSnapshot,
                Quantity,
                UnitPriceSnapshot,
                LineTotal
            FROM dbo.ExpenseItems
            WHERE ExpenseEntryId IN @ExpenseEntryIds
            ORDER BY ExpenseEntryId, ExpenseItemId;
        """;

        return await _databaseExecutor.ExecuteAsync(
            async (connection, transaction) =>
            {
                var command = new CommandDefinition(
                    commandText: sql,
                    parameters: new { ExpenseEntryIds = expenseEntryIds },
                    transaction: transaction,
                    cancellationToken: cancellationToken);
                IEnumerable<ExpenseItem> expenseItems = await connection.QueryAsync<ExpenseItem>(command);
                return expenseItems.AsList();
            }
        );
    }

    public async Task<ExpenseItem?> GetByIdAsync(
        int expenseItemId,
        CancellationToken cancellationToken = default)
    {
        const string sql = """
            SELECT
                ExpenseItemId,
                ExpenseEntryId,
                ItemId,
                ItemNameSnapshot,
                ItemCodeSnapshot,
                BrandSnapshot,
                Quantity,
                UnitPriceSnapshot,
                LineTotal
            FROM dbo.ExpenseItems
            WHERE ExpenseItemId = @ExpenseItemId;
        """;

        return await _databaseExecutor.ExecuteAsync(
            async (connection, transaction) =>
            {
                var command = new CommandDefinition(
                    commandText: sql,
                    parameters: new { ExpenseItemId = expenseItemId },
                    transaction: transaction,
                    cancellationToken: cancellationToken);
                return await connection.QuerySingleOrDefaultAsync<ExpenseItem>(command);
            }
        );
    }

    public async Task<ExpenseItem> CreateAsync(
        CreateExpenseItemInput input,
        CancellationToken cancellationToken = default)
    {
        const string sql = """
            INSERT INTO dbo.ExpenseItems (
                ExpenseEntryId,
                ItemId,
                ItemNameSnapshot,
                ItemCodeSnapshot,
                BrandSnapshot,
                Quantity,
                UnitPriceSnapshot,
                LineTotal
            )
            OUTPUT
                INSERTED.ExpenseItemId,
                INSERTED.ExpenseEntryId,
                INSERTED.ItemId,
                INSERTED.ItemNameSnapshot,
                INSERTED.ItemCodeSnapshot,
                INSERTED.BrandSnapshot,
                INSERTED.Quantity,
                INSERTED.UnitPriceSnapshot,
                INSERTED.LineTotal
            VALUES (
                @ExpenseEntryId,
                @ItemId,
                @ItemNameSnapshot,
                @ItemCodeSnapshot,
                @BrandSnapshot,
                @Quantity,
                @UnitPriceSnapshot,
                @LineTotal
            );
        """;

        return await _databaseExecutor.ExecuteAsync(
            async (connection, transaction) =>
            {
                var command = new CommandDefinition(
                    commandText: sql,
                    parameters: new
                    {
                        ExpenseEntryId = input.ExpenseEntryId,
                        ItemId = input.ItemId,
                        ItemNameSnapshot = input.ItemNameSnapshot,
                        ItemCodeSnapshot = input.ItemCodeSnapshot,
                        BrandSnapshot = input.BrandSnapshot,
                        Quantity = input.Quantity,
                        UnitPriceSnapshot = input.UnitPriceSnapshot,
                        LineTotal = input.LineTotal
                    },
                    transaction: transaction,
                    cancellationToken: cancellationToken);
                return await connection.QuerySingleAsync<ExpenseItem>(command);
            }
        );
    }
}
