using System.Text.Json;
using Infrastructure.Models;


namespace Infrastructure.Repositories;

public class LanguageRepository
{
    private readonly HttpClient _http;
    
    public LanguageRepository(HttpClient http)
    {
        _http = http;
    }
    
    public async Task<List<LanguageDto>?> getLanguages()
    {
        var url = "https://api.cognitive.microsofttranslator.com/languages?api-version=3.0";
        var json = _http.GetAsync(url);
        //Console.WriteLine("HER ER FEJLEN måske" + json.);
        var str = await json.Content.ReadAsStringAsync();
        var o = JsonSerializer.Deserialize<List<LanguageDto>>(str);
        return o;
    }
}