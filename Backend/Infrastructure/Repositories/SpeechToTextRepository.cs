using System.Text.Json;
using System.Text.Json.Serialization;
using Infrastructure.Models;

namespace Infrastructure.Repositories;

public class SpeechToTextRepository
{
    private readonly HttpClient _http;

    public SpeechToTextRepository(HttpClient http)
    {
        _http = http;
    }
    
    public async Task<SpeechToTextDto> GetSpeechToText(HttpRequestMessage request)
    {
        var json = _http.SendAsync(request).Result;
        var str = await json.Content.ReadAsStringAsync();
        var o = JsonSerializer.Deserialize<SpeechToTextDto>(str);
        return o;
    }
}