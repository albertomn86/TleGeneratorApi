using Microsoft.EntityFrameworkCore;

namespace TlegeneratorApi;

public interface IAppDbContext
{
    DbSet<TleEntry> TleEntries { get; }
}