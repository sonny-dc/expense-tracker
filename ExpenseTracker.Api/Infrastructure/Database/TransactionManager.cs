namespace ExpenseTracker.Api.Infrastructure.Database;

public sealed class TransactionManager
{
    private readonly UnitOfWork _unitOfWork;

    public TransactionManager(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Used for operations that do not return a value
    /// </summary>
    public async Task ExecuteAsync(
        Func<Task> operation,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(operation);

        await _unitOfWork.BeginAsync(cancellationToken);

        try
        {
            await operation();

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            if (_unitOfWork.HasActiveTransaction)
            {
                await _unitOfWork.RollbackAsync();
            }

            throw;
        }
    }

    /// <summary>
    /// Used for operations that return a value
    /// </summary>
    public async Task<TResult> ExecuteAsync<TResult>(
        Func<Task<TResult>> operation,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(operation);

        await _unitOfWork.BeginAsync(cancellationToken);

        try
        {
            TResult result = await operation();

            await _unitOfWork.CommitAsync();

            return result;
        }
        catch
        {
            if (_unitOfWork.HasActiveTransaction)
            {
                await _unitOfWork.RollbackAsync();
            }

            throw;
        }
    }
}
