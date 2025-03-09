using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TleGeneratorApi.CatalogProviders;

namespace TleGeneratorApi.Tests;

public class ControllerTests
{
    private static AppController InitializeController()
    {
        var context = InMemoryDatabase.GetDbContext();
        var httpClient = new Mock<IHttpClient>();
        var tleUpdater = new TleUpdater(context, httpClient.Object);

        return new AppController(context, tleUpdater);
    }
    
    [Fact]
    public void GetObjecstByCatalogNumber_ShouldReturnTleEntryWhenCatalogNumberExists()
    {
        var controller = InitializeController();
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
        var controller = InitializeController();
        var catalogNumbers = new List<int>();

        var result = controller.GetObjecstByCatalogNumber(catalogNumbers);
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public void GetObjecstByGroupName_ShouldReturnListWhenGroupExists()
    {
        var controller = InitializeController();

        var result = controller.GetObjecstByGroupName("weather") as OkObjectResult;
        Assert.NotNull(result);

        var objectsList = Assert.IsType<List<ObjectDto>>(result.Value);
        Assert.Equal(2, objectsList.Count);
    }

    [Fact]
    public void GetObjecstByGroupName_ShouldReturnBadRequestWhenGroupIsNull()
    {
        var controller = InitializeController();

        var result = controller.GetObjecstByGroupName(null);
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public void GetObjecstByGroupName_ShouldReturnBadRequestWhenGroupIsEmpty()
    {
        var controller = InitializeController();

        var result = controller.GetObjecstByGroupName(string.Empty);
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public void GetObjecstByGroupName_ShouldReturnBadRequestWhenGroupDoesNotExist()
    {
        var controller = InitializeController();

        var result = controller.GetObjecstByGroupName("invalid");
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task UpdateCatalog_ShouldReturnOkWhenCatalogWasUpdated()
    {
        var context = InMemoryDatabase.GetDbContext();
        var httpClient = new Mock<IHttpClient>();
        var tleUpdater = new Mock<ITleUpdater>();
        var controller = new AppController(context, tleUpdater.Object);
        tleUpdater.Setup(x => x.UpdateDatabase()).ReturnsAsync(true);

        var result = await controller.UpdateCatalogDatabase();

        Assert.IsType<OkResult>(result);
    }
}
