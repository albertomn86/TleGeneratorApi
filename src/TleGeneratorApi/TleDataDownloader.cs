using TleGeneratorApi.CatalogProviders;

namespace TleGeneratorApi;

public class TleDataDownloader
{
    private readonly IHttpClient _client;

    public TleDataDownloader(IHttpClient client)
    {
        _client = client;
    }

    public async Task<string> DownloadData(string groupName)
    {
        return await _client.GetTleData(groupName);
    }
}