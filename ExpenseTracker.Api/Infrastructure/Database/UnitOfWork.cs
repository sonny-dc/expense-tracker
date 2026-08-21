using Microsoft.Data.SqlClient;

namespace ExpenseTracker.Api.Infrastructure.Database;

public sealed class UnitOfWork : IAsyncDisposable
{
    private readonly SqlConnectionFactory _connectionFactory;

    private SqlConnection? _connection;
    private SqlTransaction? _transaction;

    public UnitOfWork(SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public SqlConnection Connection =>
        _connection
        ?? throw new InvalidOperationException(
            "No active database connection exists."
        );

    public SqlTransaction Transaction =>
        _transaction
        ?? throw new InvalidOperationException(
            "No active database transaction exists."
        );

    public bool HasActiveTransaction => _transaction is not null;

    public async Task BeginAsync(
        CancellationToken cancellationToken = default)
    {
        if (_connection is not null || _transaction is not null)
        {
            throw new InvalidOperationException(
                "A database transaction is already in progress."
            );
        }
        _connection = _connectionFactory.CreateConnection();

        try
        {
            await _connection.OpenAsync(cancellationToken);

            _transaction = (SqlTransaction)
                await _connection.BeginTransactionAsync(cancellationToken);
        }
        catch
        {
            await DisposeConnectionAsync();
            throw;
        }

    }

    public async Task CommitAsync(
        CancellationToken cancellationToken = default)
    {
        SqlTransaction transaction = Transaction;
        try
        {
            await transaction.CommitAsync(cancellationToken);
        }
        finally
        {
            await ClearTransactionAsync();
        }
    }

    public async Task RollbackAsync(
        CancellationToken cancellationToken = default)
    {
        SqlTransaction transaction = Transaction;
        try
        {
            await transaction.RollbackAsync(cancellationToken);
        }
        finally
        {
            await ClearTransactionAsync();
        }
    }

    public async ValueTask DisposeAsync()
    {
        SqlTransaction? transaction = _transaction;
        if (transaction is not null)
        {
            try
            {
                await transaction.RollbackAsync();
            }
            finally
            {
                await ClearTransactionAsync();
            }

            return;
        }
        await DisposeConnectionAsync();
    }

    private async Task ClearTransactionAsync()
    {
        SqlTransaction? transaction = _transaction;
        _transaction = null;
        try
        {
            if (transaction is not null)
            {
                await transaction.DisposeAsync();
            }
        }
        finally
        {
            await DisposeConnectionAsync();
        }
    }

    private async Task DisposeConnectionAsync()
    {
        SqlConnection? connection = _connection;
        _connection = null;
        if (connection is not null)
        {
            await connection.DisposeAsync();
        }
    }
}
