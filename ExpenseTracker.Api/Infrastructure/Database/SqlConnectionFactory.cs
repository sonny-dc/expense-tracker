using Microsoft.Data.SqlClient;

namespace ExpenseTracker.Api.Infrastructure.Database;

public sealed class SqlConnectionFactory
{
    private readonly string _connectionString;

    public SqlConnectionFactory(string connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new ArgumentException(
                "The database connection string cannot be null or empty. Please provide a valid connection string in the configuration file.",
                nameof(connectionString)
            );
        }
        _connectionString = connectionString;
    }

    public SqlConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }

}
