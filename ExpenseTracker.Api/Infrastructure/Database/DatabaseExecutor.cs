using Microsoft.Data.SqlClient;

namespace ExpenseTracker.Api.Infrastructure.Database;

public sealed class DatabaseExecutor
{
    private readonly SqlConnectionFactory _connectionFactory;
    private readonly UnitOfWork _unitOfWork;

    public DatabaseExecutor(
        SqlConnectionFactory connectionFactory,
        UnitOfWork unitOfWork)
    {
        _connectionFactory = connectionFactory;
        _unitOfWork = unitOfWork;
    }

    public async Task<TResult> ExecuteAsync<TResult>(
        Func<SqlConnection, SqlTransaction?, Task<TResult>> operation)
    {
        ArgumentNullException.ThrowIfNull(operation);

        if (_unitOfWork.HasActiveTransaction)
        {
            return await operation(
                _unitOfWork.Connection,
                _unitOfWork.Transaction);
        }

        await using SqlConnection connection =
            _connectionFactory.CreateConnection();

        return await operation(
            connection,
            null);
    }

    public async Task ExecuteAsync(
        Func<SqlConnection, SqlTransaction?, Task> operation)
    {
        ArgumentNullException.ThrowIfNull(operation);

        if (_unitOfWork.HasActiveTransaction)
        {
            await operation(
                _unitOfWork.Connection,
                _unitOfWork.Transaction);

            return;
        }

        await using SqlConnection connection =
            _connectionFactory.CreateConnection();

        await operation(
            connection,
            null);
    }
}