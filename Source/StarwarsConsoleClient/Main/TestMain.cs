using StarwarsConsoleClient.Networking;
using StarwarsConsoleClient.UI.Screens;
using System;
using System.Globalization;
using System.IO;
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
            Client = new SpacePortApiClient(@"https://localhost:5001", Path.GetTempPath() + "API-Client_LOG.txt");
        }
        public static readonly SpacePortApiClient Client;
        public static (string accountName, string password) _namepass { get; set; }

        public static async Task Main(string[] args)
        {
            Console.WriteLine("Tap to login again!");
            Console.ReadKey();
            Console.WriteLine("Logging in...");
            await LoginDarthMaul();
            Console.WriteLine("---End of message---");
            Client.OpenLogFile();
            Console.ReadLine();

        }

        private static async Task LoginDarthMaul()
        {
            await Client.Login("BigRed", "Maul@123");
        }
    }
}