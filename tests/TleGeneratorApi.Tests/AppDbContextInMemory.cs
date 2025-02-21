using Microsoft.EntityFrameworkCore;

using TleGeneratorApi;

public class AppDbContextInMemory : DbContext, IAppDbContext
{
    public DbSet<TleEntry> TleEntries { get; set; }
    public DbSet<TleGroup> TleGroups { get; set; }

    public AppDbContextInMemory(DbContextOptions<AppDbContextInMemory> options) : base(options) { }
}
