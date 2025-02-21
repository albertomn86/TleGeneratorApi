using Microsoft.EntityFrameworkCore;

namespace TleGeneratorApi;

public class AppDbContext : DbContext, IAppDbContext
{
    public DbSet<TleEntry> TleEntries { get; set; }
    public DbSet<TleGroup> TleGroups { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=./Database/app.db");
    }
}
