using Microsoft.AspNetCore.Mvc;

namespace TleGeneratorApi.Tests;

public class ControllerTests
{
    [Fact]
    public void GetObjecstByCatalogNumber_ShouldReturnTleEntryWhenCatalogNumberExists()
    {
        var context = InMemoryDatabase.GetDbContext();
        var controller = new AppController(context);
        var catalogNumbers = new List<int>{ 33591 };

        var result = controller.GetObjecstByCatalogNumber(catalogNumbers) as OkObjectResult;
        Assert.NotNull(result);

        var tleEntries = Assert.IsType<List<TleEntry>>(result.Value);
        var tleEntry = Assert.Single(tleEntries);
        Assert.Equal("NOAA 19", tleEntry.ObjectName);
    }

    [Fact]
    public void GetObjecstByCatalogNumber_ShouldReturnBadRequestWhenCatalogNumberListIsEmpty()
    {
        var context = InMemoryDatabase.GetDbContext();
        var controller = new AppController(context);
        var catalogNumbers = new List<int>();

        var result = controller.GetObjecstByCatalogNumber(catalogNumbers);
        Assert.IsType<BadRequestResult>(result);
    }
}
