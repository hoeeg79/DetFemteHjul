using Fleck;
using lib;

namespace api;

public class ClientWantsToTranslateDto : BaseDto
{
    public string fromLanguage { get; set; }
    public string toLanguage { get; set; }
    public string messageToTranslate { get; set; }
}

public class ClientWantsToTranslate : BaseEventHandler<ClientWantsToTranslateDto>
{
    public override Task Handle(ClientWantsToTranslateDto dto, IWebSocketConnection socket)
    {
        
        
        
        return Task.CompletedTask;
    }
}