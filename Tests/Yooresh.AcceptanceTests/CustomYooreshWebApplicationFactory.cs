using Hangfire;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using System.Data.Common;
using Yooresh.Infrastructure.Persistence;

namespace Yooresh.AcceptanceTests;

public class CustomYooreshWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            ReplaceDatabaseWithSqLiteInMemory(services);
            InjectFakeHangfireService(services);
            InjectFakeAuthenticationHandelr(services);
        });

        builder.UseEnvironment("Test");
    }

    private void ReplaceDatabaseWithSqLiteInMemory(IServiceCollection services)
    {
        var dbContextDescriptor = services.SingleOrDefault(
            d => d.ServiceType ==
                typeof(IDbContextOptionsConfiguration<Context>));
        services.Remove(dbContextDescriptor!);

        var dbConnectionDescriptor = services.SingleOrDefault(
            d => d.ServiceType ==
                typeof(DbConnection));
        services.Remove(dbConnectionDescriptor!);

        // Create open SqliteConnection so EF won't automatically close it.
        services.AddSingleton<DbConnection>(container =>
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            return connection;
        });

        services.AddDbContext<Context>((container, options) =>
        {
            var connection = container.GetRequiredService<DbConnection>();
            options.UseSqlite(connection);
        });
    }

    private void InjectFakeHangfireService(IServiceCollection services)
    {
        // Find and remove Hangfire-related services
        var descriptorsToRemove = services
            .Where(d => d.ServiceType.Namespace != null &&
                        d.ServiceType.Namespace.Contains("Hangfire"))
            .ToList();

        foreach (var descriptor in descriptorsToRemove)
        {
            services.Remove(descriptor);
        }

        services.AddSingleton(Mock.Of<IGlobalConfiguration>())
                .AddSingleton(Mock.Of<JobStorage>())
                //.AddSingleton(Mock.Of<IUpdateVendorDataJobsScheduler>())
                .AddSingleton(Mock.Of<RouteCollection>());
    }

    private void InjectFakeAuthenticationHandelr(IServiceCollection services)
    {
        // Remove existing authentication handlers
        var descriptor = services.SingleOrDefault(
            d => d.ServiceType == typeof(AuthenticationSchemeOptions));

        // Optionally remove real BasicAuth handler registrations
        services.RemoveAll<AuthenticationHandler<AuthenticationSchemeOptions>>();

        // Add fake handler
        services.AddAuthentication(FakeAuthenticationHandler.AuthenticationScheme)
            .AddScheme<AuthenticationSchemeOptions, FakeAuthenticationHandler>(
                FakeAuthenticationHandler.AuthenticationScheme, options => { });
    }
}