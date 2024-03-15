using System.Reflection.Metadata;
using Fleck;
using lib;
using Service;

namespace api;

public class ClientWantsToTranslateAudio : BaseEventHandler<ClientWantsToTranslateAudioDto>
{
    private readonly SpeechToTextService _speechToTextService;
    public ClientWantsToTranslateAudio(SpeechToTextService speechToTextService)
    {
        _speechToTextService = speechToTextService;
    }
    public override async Task Handle(ClientWantsToTranslateAudioDto dto, IWebSocketConnection socket)
    {
        Console.WriteLine(dto.audioToTranslate);
        var text = await _speechToTextService.CreateMessageBody(dto.fromLanguage, dto.audioToTranslate);
        Console.WriteLine(text);
    }
}

public class ClientWantsToTranslateAudioDto : BaseDto
{
    public string? fromLanguage { get; set; }
    public string toLanguage { get; set; }
    public string audioToTranslate { get; set; }
}