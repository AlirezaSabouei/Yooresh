using Yooresh.Application.Common.Interfaces;
using Yooresh.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Yooresh.AcceptanceTests;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Acceptance");
        
        builder.ConfigureServices(services =>
        {
            var provider = services.BuildServiceProvider();
            var configuration = provider.GetService<IConfiguration>();

            // Remove the existing database context registration
            var dbContextDescriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<Context>));

            if (dbContextDescriptor != null)
            {
                services.Remove(dbContextDescriptor);
            }

            var useInMemoryDb = configuration!.GetValue<bool>("UseInMemoryDb");
            
            // Add the in-memory database context
            services.AddDbContext<Context>(options =>
            {
                if (useInMemoryDb)
                {
                    options.UseInMemoryDatabase(configuration!.GetConnectionString("InMemoryConnection")!);
                }
                else
                {
                    options.UseSqlServer(configuration!.GetConnectionString("SqlServerConnection"));
                }
            });

            // Other services configuration if needed
            services.AddScoped<IContext, Context>();
            
        });
    }
}