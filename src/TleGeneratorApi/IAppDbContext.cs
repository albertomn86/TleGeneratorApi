using Microsoft.EntityFrameworkCore;

namespace TleGeneratorApi;

public interface IAppDbContext
{
    DbSet<TleEntry> TleEntries { get; }
    DbSet<TleGroup> TleGroups { get; }
}
