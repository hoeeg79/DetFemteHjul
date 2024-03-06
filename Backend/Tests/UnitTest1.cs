using System.Drawing;
using System.Text.Json;
using api;

namespace Tests;

public class Tests
{
    
    [SetUp]
    public void Setup()
    {
       
    }

    [Test]
    public async Task Test1()
    {
        string obj = await Translator.TranslateText("How do you do?");
        StringAssert.Contains("Comment vas-tu?", obj);
    }

}