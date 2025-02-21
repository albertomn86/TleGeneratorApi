using Microsoft.EntityFrameworkCore;

using TlegeneratorApi;

public class AppDbContextInMemory : DbContext, IAppDbContext
{
    public DbSet<TleEntry> TleEntries { get; set; }

    public AppDbContextInMemory(DbContextOptions<AppDbContextInMemory> options) : base(options) { }
}