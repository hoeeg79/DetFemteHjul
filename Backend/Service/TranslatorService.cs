using System.Text;
using Infrastructure.Repositories;
using Newtonsoft.Json;

namespace Service;

public class TranslatorService
{
    private readonly TranslatorRepository _translatorRepository;
    private static readonly string key = Environment.GetEnvironmentVariable("API_KEY_TRANSLATOR")!;
    private static readonly string location = "northeurope";
    private static readonly string endpoint = "https://api.cognitive.microsofttranslator.com/";

    public TranslatorService(TranslatorRepository translatorRepository)
    {
        _translatorRepository = translatorRepository;
    }

    public string CreateMessageBody(string text)
    {
        string route = "/translate?api-version=3.0&from=en&to=fr&to=zu";
        object[] body = new object[]
        {
            new { Text = text }
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
        var response = _translatorRepository.getTranslation(request).Result;
        return response;
    }
}