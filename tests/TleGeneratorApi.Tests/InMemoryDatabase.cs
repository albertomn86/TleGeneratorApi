using Microsoft.EntityFrameworkCore;

namespace TleGeneratorApi.Tests;

public class InMemoryDatabase
{
    public static AppDbContextInMemory GetDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContextInMemory>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        var context = new AppDbContextInMemory(options);

        if (!context.TleGroups.Any())
        {
            context.TleGroups.AddRange(
                new TleGroup { Name = "weather", LastUpdate = DateTime.UtcNow },
                new TleGroup { Name = "amateur", LastUpdate = DateTime.UtcNow }
            );
            context.SaveChanges();
        }

        var weatherGroup = context.TleGroups.Single(g => g.Name == "weather");
        var amateurGroup = context.TleGroups.Single(g => g.Name == "amateur");

        if (!context.TleEntries.Any())
        {
            context.TleEntries.AddRange(
                new TleEntry { 
                    ObjectName = "NOAA 18",
                    CatalogNumber = 28654,
                    Line1 = "1 28654U 05018A   25049.56466254  .00000375  00000+0  22228-3 0  9990",
                    Line2 = "2 28654  98.8505 130.2669 0015149  86.0406 274.2497 14.13518432 17985",
                    TleGroupName = "weather",
                    TleGroup = weatherGroup
                },
                new TleEntry { 
                    ObjectName = "NOAA 19",
                    CatalogNumber = 33591,
                    Line1 = "1 33591U 09005A   25049.55298031  .00000334  00000+0  20243-3 0  9991",
                    Line2 = "2 33591  99.0127 113.5722 0013010 320.1254  39.8961 14.13287913826162",
                    TleGroupName = "weather",
                    TleGroup = weatherGroup
                },
                new TleEntry { 
                    ObjectName = "ISS (ZARYA)",
                    CatalogNumber = 25544,
                    Line1 = "1 25544U 98067A   25049.54500833  .00019261  00000+0  34150-3 0  9992",
                    Line2 = "2 25544  51.6363 174.6431 0004349 332.4152  27.6605 15.50255355496766",
                    TleGroupName = "amateur",
                    TleGroup = amateurGroup
                }
            );
            context.SaveChanges();
        }

        return context;
    }
}
