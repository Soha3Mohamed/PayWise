using Microsoft.Extensions.DependencyInjection;
using PayWise.Core.Interfaces;
using PayWise.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayWise.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {

            //now this service extension is supposed to be used in program.cs but how to do so while every pacjage and so is in paywise.api??

            

            // Add repositories
            services.AddScoped<IUserRepository, UserRepository>();
            //services.AddScoped<IWalletRepository, WalletRepository>();
            //services.AddScoped<ITransactionRepository, TransactionRepository>();

            // Add other infrastructure services as needed
        }
    }
}
