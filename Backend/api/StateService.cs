using System.Threading.RateLimiting;
using Fleck;

namespace api;

public class WsWithMetaData(IWebSocketConnection connection)
{
    public IWebSocketConnection Connection { get; set; } = connection;

    public Dictionary<string, RateLimiter> RateLimitPerEvent { get; set; } = new();
}

public static class StateService
{
    public static Dictionary<Guid, IWebSocketConnection> Connections = new();

    public static bool AddConnection(IWebSocketConnection ws)
    {
        return Connections.TryAdd(ws.ConnectionInfo.Id, ws);
    }

    
    public static bool RemoveConnection(IWebSocketConnection ws)
    {
        return Connections.Remove(ws.ConnectionInfo.Id);
    }
    
}