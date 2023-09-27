using Yooresh.Domain.Entities.Players;
using Yooresh.Domain.Entities.Villages;
using Microsoft.EntityFrameworkCore;

namespace Yooresh.Application.Common.Interfaces;

public interface IContext
{
    public DbSet<Player> Players { get; set; }
    public DbSet<Village> Villages { get; set; }
    public Task SaveChangesAsync(CancellationToken cancellationToken = default);
}