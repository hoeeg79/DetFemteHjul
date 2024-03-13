using Microsoft.AspNetCore.Mvc;
using Service;

namespace api.Controller;

public class TranslatorController : ControllerBase
{
    private readonly TranslatorService _translatorService;

    public TranslatorController(TranslatorService translatorService)
    {
        _translatorService = translatorService;
    }

    [HttpGet]
    [Route("/translator")]
    public string GetTranslation(string text)
    {
        return _translatorService.CreateMessageBody(text);
    }
}