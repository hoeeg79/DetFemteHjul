namespace Infrastructure.Models;
public class NBest
{
    public double Confidence { get; set; }
    public string Lexical { get; set; }
    public string ITN { get; set; }
    public string MaskedITN { get; set; }
    public string Display { get; set; }
}

public class SpeechToTextDto
{
    public string RecognitionStatus { get; set; }
    public int Offset { get; set; }
    public int Duration { get; set; }
    public List<NBest> NBest { get; set; }
    public string DisplayText { get; set; }
}