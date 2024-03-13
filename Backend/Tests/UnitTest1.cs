using System.Drawing;
using System.Text.Json;
using api;
using api.Controller;

namespace Tests;

public class Tests
{
    private readonly TranslatorController _translatorController;
    
    [SetUp]
    public void Setup()
    {
       
    }

    [Test]
    public async Task Test1()
    {
        var obj = _translatorController.GetTranslation("How do you do?");
        Console.WriteLine(obj);
        StringAssert.Contains("Comment vas-tu?", obj);
    }

}