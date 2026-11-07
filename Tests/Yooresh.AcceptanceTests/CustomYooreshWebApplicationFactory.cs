using Hangfire;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using MongoDB.Driver;

namespace Yooresh.AcceptanceTests;

public class CustomYooreshWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
    private readonly string _databaseName = $"YooreshAcceptance_{Guid.NewGuid():N}";

    public string DatabaseName => _databaseName;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            ReplaceDatabaseWithTestMongoDatabase(services);
            InjectFakeHangfireService(services);
            InjectFakeAuthenticationHandler(services);
        });

        builder.UseEnvironment("Test");
    }

    private void ReplaceDatabaseWithTestMongoDatabase(IServiceCollection services)
    {
        services.RemoveAll<IMongoDatabase>();

        services.AddScoped<IMongoDatabase>(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(_databaseName);
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
                .AddSingleton(Mock.Of<RouteCollection>());
    }

    private void InjectFakeAuthenticationHandler(IServiceCollection services)
    {
        services.RemoveAll<AuthenticationHandler<AuthenticationSchemeOptions>>();

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = FakeAuthenticationHandler.AuthenticationScheme;
                options.DefaultChallengeScheme = FakeAuthenticationHandler.AuthenticationScheme;
                options.DefaultScheme = FakeAuthenticationHandler.AuthenticationScheme;
            })
            .AddScheme<AuthenticationSchemeOptions, FakeAuthenticationHandler>(
                FakeAuthenticationHandler.AuthenticationScheme, _ => { });
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            try
            {
                Services.GetRequiredService<IMongoClient>().DropDatabase(_databaseName);
            }
            catch
            {
                // The test database is disposable; never fail test teardown because of cleanup.
            }
        }

        base.Dispose(disposing);
    }
}
