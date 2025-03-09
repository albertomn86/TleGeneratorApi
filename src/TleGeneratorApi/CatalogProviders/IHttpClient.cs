namespace TleGeneratorApi.CatalogProviders;

public interface IHttpClient
{
    Task<string> GetTleData(string groupName);
}