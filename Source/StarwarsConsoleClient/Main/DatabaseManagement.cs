using System.IO;
using System.Linq;

namespace StarwarsConsoleClient.Main
{
    public partial class DatabaseManagement
    {
        public static double PriceMultiplier = 10;
        public static int ParkingSlots = 5;
        private static string ConnectionString { get; set; }

        public static void SetConnectionString(string filePath) { ConnectionString = File.ReadAllText(filePath); }
    }
}