namespace Infrastructure.Repositories;

public class BlobRepository
{
    private readonly HttpClient _http;

    public BlobRepository(HttpClient http)
    {
        _http = http;
    }
}