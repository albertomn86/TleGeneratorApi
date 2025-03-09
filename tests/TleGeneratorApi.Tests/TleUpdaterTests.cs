using Moq;

namespace TleGeneratorApi.Tests;

public class TleUpdaterTests
{
    [Fact]
    public void UpdateDatabase_ReturnsTrueWhenDatabaseIsUpdated()
    {
        var context = new Mock<IAppDbContext>();
        var updater = new TleUpdater(context.Object);
        var groupsList = new List<string> { "group" };

        var result = updater.UpdateDatabase(groupsList);

        Assert.True(result);
    }
}