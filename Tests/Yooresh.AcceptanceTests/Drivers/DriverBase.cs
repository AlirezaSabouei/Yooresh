using AutoFixture;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Yooresh.API;
using Yooresh.Application.Common;
using Yooresh.Domain.Entities;
using Yooresh.Infrastructure.Persistence;

namespace Yooresh.AcceptanceTests.Drivers;

public abstract class DriverBase<TEntity,TDto> 
    where TEntity : BaseEntity
    where TDto : BaseDto
{
    public WebApplicationFactory<Program> Factory { get; set; }

    public readonly HttpClient HttpClient;

    public Context? DbContext;

    public DriverBase(WebApplicationFactory<Program> factory)
    {
        Factory = factory;
        HttpClient = Factory.CreateClient();
        GetDbContext();
    }

    private void GetDbContext()
    {
        IServiceScopeFactory scopeFactory = Factory.Services.GetRequiredService<IServiceScopeFactory>();
        IServiceScope scope = scopeFactory.CreateScope();
        DbContext = scope.ServiceProvider.GetRequiredService<Context>();
        DbContext.Database.EnsureCreated();
    }

    public virtual async Task<TEntity> AddEntityToDatabaseAsync(TEntity baseEntity)
    {
        await DbContext!.Set<TEntity>().AddAsync(baseEntity);
        await DbContext!.SaveChangesAsync();
        return (await DbContext.Set<TEntity>().FindAsync(baseEntity.Id))!;
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
        DbContext!.Set<TEntity>().RemoveRange(DbContext!.Set<TEntity>());
        await DbContext.SaveChangesAsync();
    }

    public TEntity? GetEntityFromDatabase(Guid id)
    {
        return DbContext!.Set<TEntity>().FirstOrDefault(a => a.Id == id);
    }
}
