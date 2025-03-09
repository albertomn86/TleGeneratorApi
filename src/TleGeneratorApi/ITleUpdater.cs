
namespace TleGeneratorApi;

public interface ITleUpdater
{
    Task<bool> UpdateDatabase();
}