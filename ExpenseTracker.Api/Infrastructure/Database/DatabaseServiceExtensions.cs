namespace ExpenseTracker.Api.Infrastructure.Database;

public static class DatabaseServiceExtensions
{
    public static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        string connectionString =
            configuration.GetConnectionString(
                "ExpenseTrackerDatabase")
            ?? throw new InvalidOperationException(
                "Connection string 'ExpenseTrackerDatabase' was not found.");

        services.AddSingleton(
            new SqlConnectionFactory(connectionString));

        services.AddScoped<UnitOfWork>();
        services.AddScoped<DatabaseExecutor>();
        services.AddScoped<TransactionManager>();

        return services;
    }
}
