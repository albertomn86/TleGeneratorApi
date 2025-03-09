using Microsoft.EntityFrameworkCore;
using TleGeneratorApi.CatalogProviders;

namespace TleGeneratorApi;

public class TleUpdater : ITleUpdater
{
    private readonly IAppDbContext _context;
    private readonly IHttpClient _httpClient;

    public TleUpdater(IAppDbContext context, IHttpClient httpClient)
    {
        _context = context;
        _httpClient = httpClient;
    }

    public async Task<bool> UpdateDatabase()
    {
        var groups = await _context.TleGroups.AsNoTracking().ToListAsync();
        if (groups == null || groups.Count == 0) return false;

        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            foreach (var group in groups)
            {
                var data = await _httpClient.GetTleData(group.Name);
                if (string.IsNullOrEmpty(data)) return false;

                var tleEntries = TleStreamParser.GetTleEntries(group, data);
                foreach (var tleEntry in tleEntries)
                {
                    var existingEntry = await _context.TleEntries
                        .FirstOrDefaultAsync(t => t.CatalogNumber == tleEntry.CatalogNumber);

                    if (existingEntry != null)
                    {
                        existingEntry.Line1 = tleEntry.Line1;
                        existingEntry.Line2 = tleEntry.Line2;
                    }
                    else
                    {
                        await _context.TleEntries.AddAsync(tleEntry);
                    }
                }

                group.LastUpdated = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return true;
        }
        catch
        {
            await transaction.RollbackAsync();

            return false;
        }
    }
}
