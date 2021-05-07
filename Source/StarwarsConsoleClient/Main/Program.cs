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
    public static class Program
    {
        public const ConsoleColor ForegroundColor = ConsoleColor.Green;
        private static readonly IntPtr ThisConsole = GetConsoleWindow();

        static Program()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            Client = new SpacePortApiClient(@"https://localhost:5001/", Path.GetTempPath() + "API-Client_LOG.txt");
        }
        public static readonly SpacePortApiClient Client;

        private static async Task Main(string[] args)
        {
            ShowWindow(ThisConsole, 3);
            Console.CursorVisible = false;

            var option = Option.Welcome;

            while (true)
                switch (option)
                {
                    case Option.Welcome:
                        option = Screen.Welcome();
                        Console.ReadKey(true);
                        break;
                    case Option.Start:
                        option = Screen.Start();
                        break;
                    case Option.Login:
                        option = await Screen.Login();
                        Client.OpenLogFile();
                        break;
                    case Option.Registration:
                        option = Screen.Registration();
                        break;
                    case Option.RegisterShip:
                        option = await Screen.RegisterShip();
                        break;
                    case Option.Account:
                        option = await Screen.Account();
                        break;
                    case Option.Parking:
                        option = Screen.Parking();
                        break;
                    case Option.Homeplanet:
                        option = await Screen.HomePlanet();
                        break;
                    case Option.Receipts:
                        option = Screen.Receipts();
                        break;
                    case Option.Exit:
                        Screen.Exit();
                        Thread.Sleep(2000);
                        Client.OpenLogFile();
                        Environment.Exit(0);
                        break;
                    case Option.ReRegisterShip:
                        option = await Screen.RegisterShip(true);
                        break;
                    case Option.Logout:
                        UserData.ClearData();
                        option = Screen.Start();
                        break;
                    default:
                        throw new Exception("Something wrong with the options");
                }
        }

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    }
}