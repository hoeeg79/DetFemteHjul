using lib;

namespace api;

public class ServerBroadcastTranslatedText : BaseDto
{
    public string translation { get; set; }
    public string language { get; set; }
}