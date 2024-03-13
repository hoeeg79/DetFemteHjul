namespace Infrastructure.Models;

public class TranslatorDto
{
    public List<Translation> translations { get; set; }
}

public class Translation
{
    public string text { get; set; }
    public string to { get; set; }
}