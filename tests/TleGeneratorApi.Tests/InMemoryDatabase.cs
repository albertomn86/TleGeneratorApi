using Microsoft.EntityFrameworkCore;
using TlegeneratorApi;

namespace TleGeneratorApi.Tests;

public class InMemoryDatabase
{
    public static AppDbContextInMemory GetDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContextInMemory>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        var context = new AppDbContextInMemory(options);

        if (!context.TleEntries.Any())
        {
            context.TleEntries.Add(new TleEntry { 
                ObjectName = "NOAA 19",
                CatalogNumber = 33591,
                GroupName = "weather", 
                LastUpdate = DateTime.UtcNow,
                Line1 = "1 33591U 09005A   25049.55298031  .00000334  00000+0  20243-3 0  9991",
                Line2 = "2 33591  99.0127 113.5722 0013010 320.1254  39.8961 14.13287913826162"
            });
            context.SaveChanges();
        }

        return context;
    }

}