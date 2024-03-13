using lib;

namespace api;

public class ServerGivesLanguages : BaseDto
{
    public List<string> language { get; set; }
    public List<string> code { get; set; }
}