using Moq;
using TleGeneratorApi.CatalogProviders;

namespace TleGeneratorApi.Tests;

public class TleUpdaterTests
{
    [Fact (Skip = "TODO: SQLite in memory does not support transactions")]
    public async Task UpdateDatabase_ReturnsTrueWhenDatabaseIsUpdated()
    {
        var context = new Mock<IAppDbContext>();
        var httpClient = new Mock<IHttpClient>();
        var updater = new TleUpdater(context.Object, httpClient.Object);
        var groupsList = new List<string> { "group" };

        var result = await updater.UpdateDatabase();

        Assert.True(result);
    }
}