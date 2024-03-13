namespace Infrastructure.Repositories;

public class TranslatorRepository
{
    private readonly HttpClient _http;

    public TranslatorRepository(HttpClient http)
    {
        _http = http;
    }
    
    public async Task<String> getTranslation(HttpRequestMessage request)
    {
        var json = await _http.SendAsync(request).ConfigureAwait(false);
        Console.WriteLine(json.Content.ReadAsStringAsync());
        return await json.Content.ReadAsStringAsync();
        
    }
}