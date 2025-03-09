using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using TleGeneratorApi;

public class AppDbContextInMemory : DbContext, IAppDbContext
{
    public DbSet<TleEntry> TleEntries { get; set; }
    public DbSet<TleGroup> TleGroups { get; set; }

    public new DatabaseFacade Database => base.Database;

    public AppDbContextInMemory(DbContextOptions<AppDbContextInMemory> options) : base(options) { }
}
