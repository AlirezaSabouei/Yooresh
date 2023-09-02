using Eloy.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Eloy.Infrastructure.Persistence;

public class Context : DbContext, IContext
{
    public DbSet<Domain.Entities.Player> Players { get; set; }
    public DbSet<Domain.Entities.ResourceBuildings.ResourceBuilding> ResourceBuildings { get; set; }

    public Context(DbContextOptions options) : base(options) { }
    public new async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        var count = await base.SaveChangesAsync(cancellationToken);
        if (count == 0)
        {
            throw new Exception("Save Failed");
        }
    }
}
