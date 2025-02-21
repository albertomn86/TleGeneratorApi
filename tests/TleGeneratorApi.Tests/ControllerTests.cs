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

    [Fact]
    public void GetObjecstByGroupName_ShouldReturnListWhenGroupExists()
    {
        var context = InMemoryDatabase.GetDbContext();
        var controller = new AppController(context);

        var result = controller.GetObjecstByGroupName("weather") as OkObjectResult;
        Assert.NotNull(result);

        var objectsList = Assert.IsType<List<ObjectDto>>(result.Value);
        Assert.Equal(2, objectsList.Count);
    }

    [Fact]
    public void GetObjecstByGroupName_ShouldReturnEmptyListtWhenGroupDoesNotExist()
    {
        var context = InMemoryDatabase.GetDbContext();
        var controller = new AppController(context);

        var result = controller.GetObjecstByGroupName("invalid") as OkObjectResult;
        Assert.NotNull(result);

        var objectsList = Assert.IsType<List<ObjectDto>>(result.Value);
        Assert.Empty(objectsList);
    }

    [Fact]
    public void GetObjecstByGroupName_ShouldReturnBadRequestWhenGroupIsNull()
    {
        var context = InMemoryDatabase.GetDbContext();
        var controller = new AppController(context);

        var result = controller.GetObjecstByGroupName(null);
        Assert.IsType<BadRequestResult>(result);
    }
}
