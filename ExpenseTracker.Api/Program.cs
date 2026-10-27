using System.Text.Json.Serialization;

using ExpenseTracker.Api.Features.Items;
using ExpenseTracker.Api.Features.Expenses;
using ExpenseTracker.Api.Infrastructure.Routing;
using ExpenseTracker.Api.Infrastructure.Database;
using ExpenseTracker.Api.Infrastructure.Errors;
using ExpenseTracker.Api.Features.Budget;
using FluentValidation;

namespace ExpenseTracker.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers()
                        .AddJsonOptions(options =>
                        {
                            // Rejects requests with unmapped members in the JSON payload
                            // (e.g., extra properties that don't match the model, typos in property names, etc.).
                            options.JsonSerializerOptions.UnmappedMemberHandling =
                                JsonUnmappedMemberHandling.Disallow;
                            // Prevents enum values from being serialized as integers for both
                            // Client->Server and Server->Client communication.
                            // This is a global setting that applies to all enums in the API.
                            options.JsonSerializerOptions.Converters.Add(
                                new JsonStringEnumConverter(allowIntegerValues: false));
                        });

        builder.Services.AddProblemDetails();
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

        builder.Services.AddOpenApi();

        builder.Services.AddDatabase(builder.Configuration);

        builder.Services.AddItems();
        builder.Services.AddExpenses();
        builder.Services.AddBudgets();

        // Register all validators contained in the API assembly.
        builder.Services.AddValidatorsFromAssemblyContaining<Program>();

        var app = builder.Build();

        app.UseExceptionHandler();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        RouteGroupBuilder api = app.MapGroup(AppRoutes.Api);
        api.MapControllers();

        app.Run();
    }
}
