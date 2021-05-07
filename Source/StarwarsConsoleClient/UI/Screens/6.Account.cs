using StarwarsConsoleClient.Main;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static StarwarsConsoleClient.Main.Program;

namespace StarwarsConsoleClient.UI.Screens
{
    public static partial class Screen
    {
        public static async Task<Option> Account()
        {
            var myDataPromise = Client.MyDataAsync();
            var myShipPromise = Client.MySpaceShipAsync();
            var myHomeworldPromise = Client.GetHomeworldAsync();

            ConsoleWriter.ClearScreen();
            var lines = File.ReadAllLines(@"UI/maps/6.Account.txt");
            var drawables = TextEditor.Add.DrawablesAt(lines, 0);
            TextEditor.Center.ToScreen(drawables, Console.WindowWidth, Console.WindowHeight);
            var parameterCoords = drawables.FindAll(x => x.Chars == "¤").ToList()
                .Select(x => (x.CoordinateX, x.CoordinateY)).ToList();

            var nameCoord = parameterCoords[0];
            var shipCoord = parameterCoords[1];

            var selectionList = new SelectionList<Option>(ForegroundColor, '$');
            selectionList.GetCharPositions(drawables);
            selectionList.AddSelections(new[]
            {
                Option.Parking,
                Option.Receipts,
                Option.ReRegisterShip,
                Option.Homeplanet,
                Option.Logout,
                Option.Exit
            });
            ConsoleWriter.TryAppend(drawables);
            ConsoleWriter.Update();

            UserData.Person = await myDataPromise;
            UserData.SpaceShip = await myShipPromise;
            UserData.Person.Homeplanet = await myHomeworldPromise;

            Console.ForegroundColor = ConsoleColor.Green;
            LineTools.SetCursor(nameCoord);
            Console.Write(UserData.Person.Name);
            LineTools.SetCursor(shipCoord);
            Console.Write(UserData.SpaceShip.Model);
            return selectionList.GetSelection();
        }
    }
}