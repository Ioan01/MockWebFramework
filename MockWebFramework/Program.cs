using MockWebFramework;
using MockWebFramework.Logging;
using MockWebFramework.Networking;

class Program
{
    static async Task Main(string[] args)
    {
        var web = new WebServer();
        web.Services.RegisterServices();
        

        web.Controllers.RegisterControllers();

        await web.Start();

        Console.ResetColor();

    }
}