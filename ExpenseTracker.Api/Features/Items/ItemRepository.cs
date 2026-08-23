using Dapper;
using ExpenseTracker.Api.Infrastructure.Database;

namespace ExpenseTracker.Api.Features.Items;

public sealed class ItemRepository
{
    private readonly DatabaseExecutor _databaseExecutor;
    public ItemRepository(DatabaseExecutor databaseExecutor)
    {
        _databaseExecutor = databaseExecutor;
    }

    public async Task<IReadOnlyList<Item>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        const string sql = """
            SELECT 
                ItemId,
                Name,
                Code,
                Brand,
                UnitPrice
            FROM dbo.Items
            ORDER BY Name, ItemId;
        """;
        
        return await _databaseExecutor.ExecuteAsync(
            async (connection, transaction) =>
            {
                var command = new CommandDefinition(
                    commandText: sql,
                    transaction: transaction,
                    cancellationToken: cancellationToken);
                IEnumerable<Item> items = await connection.QueryAsync<Item>(command);
                return items.AsList();
            }
        );
    }

    public async Task<Item?> GetByIdAsync(
        int itemId,
        CancellationToken cancellationToken = default)
    {
        const string sql = """
            SELECT 
                ItemId,
                Name,
                Code,
                Brand,
                UnitPrice
            FROM dbo.Items
            WHERE ItemId = @ItemId;
        """;

        return await _databaseExecutor.ExecuteAsync(
            async (connection, transaction) =>
            {
                var command = new CommandDefinition(
                    commandText: sql,
                    parameters: new { ItemId = itemId },
                    transaction: transaction,
                    cancellationToken: cancellationToken);
                return await connection.QuerySingleOrDefaultAsync<Item>(command);
            }
        );
    }

    public async Task<IReadOnlyList<Item>> GetByIdsAsync(
        IReadOnlyCollection<int> itemIds,
        CancellationToken cancellationToken = default)
    {
        const string sql = """
            SELECT
                ItemId,
                Name,
                Code,
                Brand,
                UnitPrice
            FROM dbo.Items
            WHERE ItemId IN @ItemIds;
        """;

        return await _databaseExecutor.ExecuteAsync(
            async (connection, transaction) =>
            {
                var command = new CommandDefinition(
                    commandText: sql,
                    parameters: new { ItemIds = itemIds },
                    transaction: transaction,
                    cancellationToken: cancellationToken);
                IEnumerable<Item> items = await connection.QueryAsync<Item>(command);
                return items.AsList();
            }
        );
    }

    public async Task<Item> CreateAsync(
        CreateItemRequest request,
        CancellationToken cancellationToken = default)
    {
        const string sql = """
            INSERT INTO dbo.Items
            (
                Name,
                Code,
                Brand,
                UnitPrice
            )
            OUTPUT
                INSERTED.ItemId,
                INSERTED.Name,
                INSERTED.Code,
                INSERTED.Brand,
                INSERTED.UnitPrice
            VALUES
            (
                @Name,
                @Code,
                @Brand,
                @UnitPrice
            );
        """;

        return await _databaseExecutor.ExecuteAsync(
            async (connection, transaction) =>
            {
                var command = new CommandDefinition(
                    commandText: sql,
                    parameters: new
                    {
                        request.Name,
                        request.Code,
                        request.Brand,
                        request.UnitPrice
                    },
                    transaction: transaction,
                    cancellationToken: cancellationToken
                );
                return await connection.QuerySingleAsync<Item>(command);
            }
        );
    }

    public async Task<bool> AlreadyExistsAsync(
        string name,
        string code,
        string brand,
        int? excludedItemId = null,
        CancellationToken cancellationToken = default)
    {
        const string sql = """
            SELECT CAST(
                CASE
                    WHEN EXISTS
                    (
                        SELECT 1
                        FROM dbo.Items
                        WHERE Name = @Name
                            AND Code = @Code
                            AND Brand = @Brand
                            AND (
                                @ExcludedItemId IS NULL
                                OR ItemId <> @ExcludedItemId
                            )
                    )
                    THEN 1
                    ELSE 0
                END
                AS BIT
            );
        """;

        return await _databaseExecutor.ExecuteAsync(
            async (connection, transaction) =>
            {
                var command = new CommandDefinition(
                    commandText: sql,
                    parameters: new
                    {
                        Name = name,
                        Code = code,
                        Brand = brand,
                        ExcludedItemId = excludedItemId
                    },
                    transaction: transaction,
                    cancellationToken: cancellationToken
                );

                return await connection.ExecuteScalarAsync<bool>(
                    command);
            }
        );
    }
    public async Task<bool> UpdateAsync(
        int itemId,
        UpdateItemRequest request,
        CancellationToken cancellationToken = default)
    {
        const string sql = """
            UPDATE dbo.Items
            SET
                Name = COALESCE(@Name, Name),
                Code = COALESCE(@Code, Code),
                Brand = COALESCE(@Brand, Brand),
                UnitPrice = COALESCE(@UnitPrice, UnitPrice)
            WHERE ItemId = @ItemId;
        """;

        return await _databaseExecutor.ExecuteAsync(
            async (connection, transaction) =>
            {
                var command = new CommandDefinition(
                    commandText: sql,
                    parameters: new
                    {
                        ItemId = itemId,
                        request.Name,
                        request.Code,
                        request.Brand,
                        request.UnitPrice
                    },
                    transaction: transaction,
                    cancellationToken: cancellationToken
                );
                int affectedRows =
                    await connection.ExecuteAsync(command);

                return affectedRows > 0;
            }
        );
    }

    public async Task<bool> DeleteAsync(
        int itemId,
        CancellationToken cancellationToken = default)
    {
        const string sql = """
            DELETE FROM dbo.Items
            WHERE ItemId = @ItemId;
        """;

        return await _databaseExecutor.ExecuteAsync(
            async (connection, transaction) =>
            {
                var command = new CommandDefinition(
                    commandText: sql,
                    parameters: new { ItemId = itemId },
                    transaction: transaction,
                    cancellationToken: cancellationToken
                );
                int affectedRows =
                    await connection.ExecuteAsync(command);

                return affectedRows > 0;
            }
        );
    }
}
