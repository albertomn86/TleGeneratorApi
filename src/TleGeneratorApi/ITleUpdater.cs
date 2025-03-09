
namespace TleGeneratorApi;

public interface ITleUpdater
{
    bool UpdateDatabase(List<string> groupsList);
}