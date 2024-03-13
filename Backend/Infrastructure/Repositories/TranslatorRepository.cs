using System.Text.Json;
using System.Text.Json.Serialization;
using Infrastructure.Models;

namespace Infrastructure.Repositories;

public class TranslatorRepository
{
    private readonly HttpClient _http;

    public TranslatorRepository(HttpClient http)
    {
        _http = http;
    }
    
    public async Task<List<TranslatorDto>?> getTranslation(HttpRequestMessage request)
    {
        var json = _http.SendAsync(request).Result;
        Console.WriteLine("HER ER FEJLEN måske" + json.Content);
        var str = await json.Content.ReadAsStringAsync();
        var o = JsonSerializer.Deserialize<List<TranslatorDto>>(str);
        return o;
    }
}
