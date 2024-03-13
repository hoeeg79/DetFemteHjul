using System.Text.Json;
using Fleck;
using lib;
using Service;

namespace api;

public class ClientWantsToTranslateDto : BaseDto
{
    public string? fromLanguage { get; set; }
    public string toLanguage { get; set; }
    public string messageToTranslate { get; set; }
}

public class ClientWantsToTranslate : BaseEventHandler<ClientWantsToTranslateDto>
{
    private readonly TranslatorService _translator;
    public ClientWantsToTranslate(TranslatorService translator)
    {
        _translator = translator;
    }
    public override async Task Handle(ClientWantsToTranslateDto dto, IWebSocketConnection socket)
    {
        var translate = await _translator.CreateMessageBody(dto.messageToTranslate);
        var actualObject = translate.First();
        var actualactualObject = actualObject.translations.First();
        
        var newTranslation = new ServerBroadcastTranslatedText()
        {
            translation = actualactualObject.text,
            language = actualactualObject.to
        };

        socket.Send(JsonSerializer.Serialize(newTranslation));
        
    }
}