using AutoFixture;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Yooresh.API;
using Yooresh.Application.Common;
using Yooresh.Domain.Entities;

namespace Yooresh.AcceptanceTests.Drivers;

public abstract class DriverBase<TEntity,TDto> : IDisposable
    where TEntity : BaseEntity
    where TDto : BaseDto 
{
    private readonly IServiceScope _scope;

    public WebApplicationFactory<Program> Factory { get; set; }

    public readonly HttpClient HttpClient;

    public IMongoDatabase Database { get; }

    public IMongoCollection<TEntity> Collection { get; }

    public DriverBase(WebApplicationFactory<Program> factory)
    {
        Factory = factory;
        HttpClient = Factory.CreateClient();

        _scope = Factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        Database = _scope.ServiceProvider.GetRequiredService<IMongoDatabase>();
        Collection = Database.GetCollection<TEntity>(typeof(TEntity).Name);
    }

    public virtual async Task<TEntity> AddEntityToDatabaseAsync(TEntity baseEntity)
    {
        await Collection.InsertOneAsync(baseEntity);
        return await Collection.Find(a => a.Id == baseEntity.Id).FirstAsync();
    }

    public virtual TEntity CreateEntityInstance()
    {
        Fixture fixture = new();
        fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));
                    fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        return fixture.Build<TEntity>().Create();
    }

    public virtual TDto CreateDtoInstance()
    {
        Fixture fixture = new();
        return fixture.Build<TDto>().Create();
    }

    public async Task ClearDatabaseAsync()
    {
        await Collection.DeleteManyAsync(Builders<TEntity>.Filter.Empty);
    }

    public async Task<TEntity?> GetEntityFromDatabaseAsync(Guid id)
    {
        return await Collection.Find(a => a.Id == id).FirstOrDefaultAsync();
    }

    public virtual void Dispose()
    {
        try
        {
            Database.Client.DropDatabase(Database.DatabaseNamespace.DatabaseName);
        }
        catch
        {
            // The test database is disposable; never fail test teardown because of cleanup.
        }

        _scope.Dispose();
        HttpClient.Dispose();
        Factory.Dispose();
    }
}
