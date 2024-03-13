using LanguageDto;
using Newtonsoft.Json;

namespace Infrastructure.Repositories;

public class LanguageRepository
{
    private readonly HttpClient _http;
    
    public LanguageRepository(HttpClient http)
    {
        _http = http;
    }
    
    public async Task<Welcome> getLanguages()
    {
        var url = "https://api.cognitive.microsofttranslator.com/languages?api-version=3.0";
        var json = _http.GetAsync(url);
        //Console.WriteLine("HER ER FEJLEN måske" + json.);
        var str = await json.Result.Content.ReadAsStringAsync();
        Console.WriteLine(str);
        var o = JsonConvert.DeserializeObject<LanguageDto.Welcome>(str) ??
                throw new InvalidOperationException("cant turn into the c# obj");
        return o;
    }
}