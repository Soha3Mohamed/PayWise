
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PayWise.Infrastructure.Contexts;
using Serilog;
using PayWise.Infrastructure.Extensions;
using PayWise.Application;
namespace PayWise.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //adding Serilog configuration
            Log.Logger = new LoggerConfiguration()
                         .MinimumLevel.Information()
                         .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
                         .Enrich.FromLogContext()
                         .WriteTo.Console()
                         .WriteTo.File(new Serilog.Formatting.Json.JsonFormatter(), "logs/log.json", rollingInterval: RollingInterval.Day)
                         .WriteTo.Seq("http://localhost:5341") // Example Seq server URL
                         .CreateLogger();

            builder.Host.UseSerilog(); //instead of the default logger

            // Add services to the container.

            builder.Services.AddControllers();
        
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddInfrastructureServices(); 
            builder.Services.AddApplicationServices();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();

            app.UseSerilogRequestLogging(); // logs HTTP requests

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
