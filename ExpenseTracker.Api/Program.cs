using System.Text.Json.Serialization;

using ExpenseTracker.Api.Features.Items;
using ExpenseTracker.Api.Features.Expenses;
using ExpenseTracker.Api.Infrastructure.Routing;
using ExpenseTracker.Api.Infrastructure.Database;
using ExpenseTracker.Api.Infrastructure.Errors;

namespace ExpenseTracker.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            
            builder.Services.AddControllers()
                            .AddJsonOptions(options =>
                            {
                                options.JsonSerializerOptions.UnmappedMemberHandling =
                                    JsonUnmappedMemberHandling.Disallow;
                            });
                            
            
            builder.Services.AddProblemDetails();
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

            builder.Services.AddOpenApi();

            builder.Services.AddDatabase(builder.Configuration);
            
            builder.Services.AddItems();
            builder.Services.AddExpenses();

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
}
