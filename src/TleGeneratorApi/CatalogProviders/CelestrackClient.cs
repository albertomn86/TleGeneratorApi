namespace TleGeneratorApi.CatalogProviders;

public class CelestrackClient : IHttpClient
{
    private readonly HttpClient _httpClient;

    public CelestrackClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://celestrak.com/NORAD/elements/gp.php");
    }

    public async Task<string> GetTleData(string groupName)
    {
        var response = await _httpClient.GetAsync($"?GROUP={groupName}&FORMAT=TLE");

        response.EnsureSuccessStatusCode();           
        
        return await response.Content.ReadAsStringAsync(); 
    }
}