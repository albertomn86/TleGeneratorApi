using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace TleGeneratorApi;

public interface IAppDbContext
{
    DbSet<TleEntry> TleEntries { get; }
    DbSet<TleGroup> TleGroups { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    DatabaseFacade Database { get; }
}
