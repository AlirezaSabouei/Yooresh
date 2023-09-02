using Eloy.Domain.Entities;
using Eloy.Domain.Entities.ResourceBuildings;
using Microsoft.EntityFrameworkCore;

namespace Eloy.Application.Common.Interfaces;

public interface IContext
{
    public DbSet<Player> Players { get; set; }
    public DbSet<ResourceBuilding> ResourceBuildings { get; set; }
    public Task SaveChangesAsync(CancellationToken cancellationToken = default);
}