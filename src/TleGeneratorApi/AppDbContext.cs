using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace TleGeneratorApi;

public class AppDbContext : DbContext, IAppDbContext
{
    public DbSet<TleEntry> TleEntries { get; set; }
    public DbSet<TleGroup> TleGroups { get; set; }

    public new DatabaseFacade Database => base.Database;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=./Database/app.db");
    }
}
