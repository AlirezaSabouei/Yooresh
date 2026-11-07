using System.Linq.Expressions;
using Yooresh.Domain.Entities;

namespace Yooresh.Application.Common.Data;

/// <summary>
/// Persistence abstraction for aggregate/document roots. Mirrors the document-store pattern
/// used in TreePomodoro: handlers never see the MongoDB driver directly.
/// </summary>
public interface IDocumentStore<TEntity> where TEntity : BaseEntity
{
    Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter, CancellationToken token = default);

    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken token = default);

    Task InsertAsync(TEntity entity, CancellationToken token = default);

    Task UpdateAsync(TEntity entity, CancellationToken token = default);

    Task DeleteAsync(Guid id, CancellationToken token = default);
}
