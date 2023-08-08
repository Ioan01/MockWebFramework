using MockWebFramework.Networking;

class Program
{
    static async Task Main(string[] args)
    {
        TcpHost host = new TcpHost();

        await host.Listen();
    }
}