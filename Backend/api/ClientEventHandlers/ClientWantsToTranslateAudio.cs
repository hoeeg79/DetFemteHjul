using System.Reflection.Metadata;
using Fleck;
using lib;

namespace api;

public class ClientWantsToTranslateAudio : BaseEventHandler<ClientWantsToTranslateAudioDto>
{
    public override async Task Handle(ClientWantsToTranslateAudioDto dto, IWebSocketConnection socket)
    {
        Console.WriteLine(dto.audioToTranslate.GetBytes());
    }
}

public class ClientWantsToTranslateAudioDto : BaseDto
{
    public string? fromLanguage { get; set; }
    public string toLanguage { get; set; }
    public Blob audioToTranslate { get; set; }
}