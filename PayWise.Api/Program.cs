
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PayWise.Infrastructure.Contexts;
using Serilog;
using PayWise.Infrastructure.Extensions;
namespace PayWise.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //adding serilog configuration
            Log.Logger = new LoggerConfiguration()
                         .MinimumLevel.Information()
                         .Enrich.FromLogContext()
                         .WriteTo.Console()
                         .WriteTo.File("logs/app-.log", rollingInterval: RollingInterval.Day)
                         .CreateLogger();

            builder.Host.UseSerilog(); 
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            builder.Services.AddInfrastructureServices(); // Extension method to add infrastructure services

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
