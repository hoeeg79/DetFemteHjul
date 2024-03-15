using Fleck;
using lib;
using Service;

namespace api;

public class ClientWantsToSaveBlobDto : BaseDto
{
    public string? fromSpeech { get; set; }
    public string toLanguage { get; set; }
}

public class ClientWantsToSaveBlob : BaseEventHandler<ClientWantsToSaveBlobDto>
{
    private readonly BlobService _blob;

    public ClientWantsToSaveBlob(BlobService blob)
    {
        _blob = blob;
    }
    
    public override Task Handle(ClientWantsToSaveBlobDto dto, IWebSocketConnection socket)
    {
        throw new NotImplementedException();
    }
}
