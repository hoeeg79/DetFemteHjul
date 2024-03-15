using System.IO;
using System.Text;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Newtonsoft.Json;

namespace Service;

public class SpeechToTextService
{
    private readonly SpeechToTextRepository _speechToTextRepository;
    private static readonly string key = Environment.GetEnvironmentVariable("SPEECH_KEY")!;
    private static readonly string endpoint = "https://northeurope.stt.speech.microsoft.com/";
    Dictionary<string, string> languageToCountry = new Dictionary<string, string>
        {
            { "af", "ZA" },
            { "am", "ET" },
            { "ar", "AE" },
            { "az", "AZ" },
            { "bg", "BG" },
            { "bn", "IN" },
            { "bs", "BA" },
            { "ca", "ES" },
            { "cs", "CZ" },
            { "cy", "GB" },
            { "da", "DK" },
            { "de", "DE" },
            { "el", "GR" },
            { "en", "AU" },
            { "es", "ES" },
            { "et", "EE" },
            { "eu", "ES" },
            { "fa", "IR" },
            { "fi", "FI" },
            { "fil", "PH" },
            { "fr", "FR" },
            { "ga", "IE" },
            { "gl", "ES" },
            { "gu", "IN" },
            { "he", "IL" },
            { "hi", "IN" },
            { "hr", "HR" },
            { "hu", "HU" },
            { "hy", "AM" },
            { "id", "ID" },
            { "is", "IS" },
            { "it", "IT" },
            { "ja", "JP" },
            { "jv", "ID" },
            { "ka", "GE" },
            { "kk", "KZ" },
            { "km", "KH" },
            { "kn", "IN" },
            { "ko", "KR" },
            { "lo", "LA" },
            { "lt", "LT" },
            { "lv", "LV" },
            { "mk", "MK" },
            { "ml", "IN" },
            { "mn", "MN" },
            { "mr", "IN" },
            { "ms", "MY" },
            { "mt", "MT" },
            { "my", "MM" },
            { "nb", "NO" },
            { "ne", "NP" },
            { "nl", "NL" },
            { "pa", "IN" },
            { "pl", "PL" },
            { "ps", "AF" },
            { "pt", "BR" },
            { "pt", "PT" },
            { "ro", "RO" },
            { "ru", "RU" },
            { "si", "LK" },
            { "sk", "SK" },
            { "sl", "SI" },
            { "so", "SO" },
            { "sq", "AL" },
            { "sr", "RS" },
            { "sv", "SE" },
            { "sw", "KE" },
            { "sw", "TZ" },
            { "ta", "IN" },
            { "te", "IN" },
            { "th", "TH" },
            { "tr", "TR" },
            { "uk", "UA" },
            { "ur", "IN" },
            { "uz", "UZ" },
            { "vi", "VN" },
            { "wuu", "CN" },
            { "yue", "CN" },
            { "zh", "CN" },
            { "zu", "ZA" }
        };
    
    // Your audio file path
    string audioFilePath = "path/to/your/audio/file.wav";

    // Read the audio file into a byte array
    byte[] audioBytes = File.ReadAllBytes(audioFilePath);

    // Convert the byte array to Base64 string (if required by the API)
    string base64Audio = Convert.ToBase64String(audioBytes);
    
    
    public SpeechToTextService(SpeechToTextRepository speechToTextRepository)
    {
        _speechToTextRepository = speechToTextRepository;
    }

    public async Task<SpeechToTextDto> CreateMessageBody(string fromLanguage)
    {
        
        string route = "";
        if (fromLanguage != null)
        {
            string route = "speech/recognition/conversation/cognitiveservices/v1?language=" + fromLanguage + "-" + languageToCountry[fromLanguage] + "&format=detailed";
        }
        else
        {
            string route = "speech/recognition/conversation/cognitiveservices/v1?language=en-AU&format=detailed";
        }
        
        object[] body = new object[]
        {
            new { Audio = base64Audio }
        };
        var requestBody = JsonConvert.SerializeObject(body);
        var request = new HttpRequestMessage();
        // Build the request.
        request.Method = HttpMethod.Post;
        request.RequestUri = new Uri(endpoint + route);
        request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
        request.Headers.Add("Ocp-Apim-Subscription-Key", key);
        // location required if you're using a multi-service or regional (not global) resource.
        request.Headers.Add("Ocp-Apim-Subscription-Region", location);
        
        return await _speechToTextRepository.GetSpeechToText(request);
    }
}