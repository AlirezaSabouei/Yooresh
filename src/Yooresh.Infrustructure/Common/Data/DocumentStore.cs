using System.Linq.Expressions;
using MediatR;
using MongoDB.Driver;
using Yooresh.Application.Common.Data;
using Yooresh.Domain.Entities;

namespace Yooresh.Infrastructure.Common.Data;

/// <summary>
/// MongoDB implementation of <see cref="IDocumentStore{TEntity}"/>.
/// Each aggregate/document root gets its own collection named after the entity type,
/// and pending domain events are published after every insert or update.
/// </summary>
public class DocumentStore<TEntity>(IMongoDatabase database, IMediator mediator)
    : IDocumentStore<TEntity> where TEntity : BaseEntity
{
    private readonly IMongoCollection<TEntity> _collection =
        database.GetCollection<TEntity>(typeof(TEntity).Name);

    public async Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter,
        CancellationToken token = default)
    {
        return await _collection.Find(filter).ToListAsync(token);
    }

    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        return await _collection.Find(entity => entity.Id == id).FirstOrDefaultAsync(token);
    }

    public async Task InsertAsync(TEntity entity, CancellationToken token = default)
    {
        var now = DateTimeOffset.UtcNow;
        entity.CreateDate = now;
        entity.UpdateDate = now;

        await _collection.InsertOneAsync(entity, cancellationToken: token);
        await DispatchEvents(entity);
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken token = default)
    {
        entity.UpdateDate = DateTimeOffset.UtcNow;

        await _collection.ReplaceOneAsync(
            stored => stored.Id == entity.Id, entity, cancellationToken: token);
        await DispatchEvents(entity);
    }

    public async Task DeleteAsync(Guid id, CancellationToken token = default)
    {
        await _collection.DeleteOneAsync(entity => entity.Id == id, token);
    }

    private async Task DispatchEvents(TEntity entity)
    {
        var events = entity.DomainEvents;

        foreach (var domainEvent in events)
        {
            await mediator.Publish(domainEvent);
        }

        entity.ClearDomainEvents();
    }
}
