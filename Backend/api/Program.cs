using System.Net.Sockets;
using System.Reflection;
using System.Text.Json;
using api.Controller;
using Fleck;
using Infrastructure.Repositories;
using lib;
using Service;

namespace api;

public static class Startup
{
    public static void Main(string[] args)
    {
        Statup(args);
        Console.ReadLine();
    }

    public static void Statup(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddSingleton<TranslatorRepository>();
        builder.Services.AddSingleton<LanguageRepository>();
        builder.Services.AddSingleton<SpeechToTextRepository>();
        
        builder.Services.AddSingleton<SpeechToTextService>();
        builder.Services.AddSingleton<TranslatorService>();
        builder.Services.AddSingleton<LanguageService>();
        builder.Services.AddHttpClient();

        var clientEventHandlers = builder.FindAndInjectClientEventHandlers(Assembly.GetExecutingAssembly());

        var app = builder.Build();

        var server = new WebSocketServer("ws://0.0.0.0:8181");

        server.Start(ws =>
        {
            ws.OnClose = () => { StateService.RemoveConnection(ws); };

            ws.OnOpen = async () =>
            {
                try
                {
                    StateService.AddConnection(ws);
                    var result = await app.Services.GetService<LanguageService>().getLanguages();
                    var response = new ServerGivesLanguages()
                    {
                        language = result["language"],
                        code = result["code"]
                    };
                    ws.Send(JsonSerializer.Serialize(response));
                }
                catch (Exception e)
                {
                    GlobalExceptionHandler.Handle(e, ws, "fix this");
                }
            };

            ws.OnMessage = async message =>
            {
                try
                {
                    await app.InvokeClientEventHandler(clientEventHandlers, ws, message);
                }
                catch (Exception e)
                {
                    GlobalExceptionHandler.Handle(e, ws, message);
                }
            };
        });
    }
}