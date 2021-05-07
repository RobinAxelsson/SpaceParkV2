using StarwarsConsoleClient.Networking;
using StarwarsConsoleClient.UI.Screens;
using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace StarwarsConsoleClient.Main
{
    public static class TestMain
    {

        static TestMain()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            Client = new SpacePortApiClient(@"https://localhost:5001", @"StarwarsConsoleClient/Networking/API-Client_LOG.txt");
        }
        public static readonly SpacePortApiClient Client;
        public static (string accountName, string password) _namepass { get; set; }

        public static async Task Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Tap to login again!");
                Console.ReadKey();
                Console.WriteLine("Logging in...");
                await LoginDarthMaul();
                Console.WriteLine("---End of message---");
                Console.ReadLine();
            }
        }

        private static async Task LoginDarthMaul()
        {
            await Client.Login("BigRed", "Maul@123");
        }
    }
}