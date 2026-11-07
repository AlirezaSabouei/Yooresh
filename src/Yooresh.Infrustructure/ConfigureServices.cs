using Hangfire;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Yooresh.Application.Common.Data;
using Yooresh.Application.Common.Interfaces;
using Yooresh.Application.Common.Tools;
using Yooresh.Domain.Entities;
using Yooresh.Domain.Interfaces;
using Yooresh.Infrastructure.Common.Data;
using Yooresh.Infrastructure.EmailTools;
using Yooresh.Infrastructure.JobTools;
using Yooresh.Infrastructure.PasswordTools;
using Yooresh.Infrastructure.Persistence;

namespace Yooresh.Infrastructure;

public static class ConfigureServices
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // MongoDB
        var mongoConnection = configuration.GetConnectionString("MongoConnection")
            ?? throw new InvalidOperationException(
                "ConnectionStrings:MongoConnection must be configured to use MongoDB.");

        var databaseName = MongoUrl.Create(mongoConnection).DatabaseName ?? "Yooresh";

        services.AddSingleton<IMongoClient>(_ => new MongoClient(mongoConnection));
        services.AddScoped<IMongoDatabase>(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(databaseName);
        });

        services.AddScoped(typeof(IDocumentStore<>), typeof(DocumentStore<>));

        // Email Configuration
        var senderEmail = configuration.GetSection("Email")["Address"]!;
        var senderPassword = configuration.GetSection("Email")["Password"]!;

        services.AddScoped<IEmail>(s => new Email(senderEmail, senderPassword));

        // Hangfire Configuration (MongoDB storage)
        var mongoStorageOptions = new MongoStorageOptions
        {
            QueuePollInterval = TimeSpan.FromSeconds(1),
            MigrationOptions = new MongoMigrationOptions
            {
                MigrationStrategy = new MigrateMongoMigrationStrategy()
            },
            CheckQueuedJobsStrategy = CheckQueuedJobsStrategy.TailNotificationsCollection
        };

        services.AddHangfire((serviceProvider, config) =>
        {
            config
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseMongoStorage(mongoConnection, databaseName, mongoStorageOptions)
                .UseActivator(new Hangfire.AspNetCore.AspNetCoreJobActivator(
                    serviceProvider.GetRequiredService<IServiceScopeFactory>()));
        });

        services.AddHangfireServer();

        services.AddScoped<IJob, Job>();
        services.AddScoped<IPasswordEncryption<BaseEntity>, PasswordEncryption<BaseEntity>>();
        services.AddScoped<IPasswordHasher<BaseEntity>, PasswordHasher<BaseEntity>>();
    }

    /// <summary>
    /// Creates the Mongo indexes and the reference data (factions and upgrade catalogues).
    /// Call once at application startup.
    /// </summary>
    public static async Task InitializeMongoDatabaseAsync(
        this IServiceProvider services, CancellationToken cancellationToken = default)
    {
        using var scope = services.CreateScope();
        var database = scope.ServiceProvider.GetRequiredService<IMongoDatabase>();
        await MongoDbInitializer.InitializeAsync(database, cancellationToken);
    }
}
