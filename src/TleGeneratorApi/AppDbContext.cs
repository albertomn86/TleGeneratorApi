using Microsoft.EntityFrameworkCore;

namespace TlegeneratorApi;

public class AppDbContext : DbContext, IAppDbContext
{
    public DbSet<TleEntry> TleEntries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=./Database/app.db");
    }
}