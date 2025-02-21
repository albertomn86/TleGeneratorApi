using Microsoft.AspNetCore.Mvc;
using TlegeneratorApi;

namespace TleGeneratorApi.Tests;

public class ControllerTests
{
    [Fact]
    public void GetObjectByCatalogNumber_ShouldReturnTleEntryWhenCatalogNumberExists()
    {
        var context = InMemoryDatabase.GetDbContext();
        var controller = new AppController(context);

        var result = controller.GetObjectByCatalogNumber(33591) as OkObjectResult;
        Assert.NotNull(result);

        var tleEntry = Assert.IsType<TleEntry>(result.Value);
        Assert.Equal("NOAA 19", tleEntry.ObjectName);
    }
}