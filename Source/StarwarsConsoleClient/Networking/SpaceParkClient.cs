using Newtonsoft.Json;
using StarwarsConsoleClient.Main;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StarwarsConsoleClient.Networking
{
    public class SpaceParkClient
    {
        private HttpClient _client;
        private readonly string _logfile = @"Networking/API-Client_LOG.txt";
        public SpaceParkClient(string baseUrl)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            _client = client;
        }

        private string _token;

        private readonly string _registerEndpoint = @"/api/Account/Register";
        private readonly string _loginEndpoint = @"/api/Account/Login";
        private readonly string _changeSpaceShipEndpoint = @"/api/Account/ChangeSpaceShip";
        private readonly string _getHomeworldEndpoint = @"​/api​/Account​/GetHomeworld";
        private readonly string _ShipsEndpoint = @"​/api​/Account​/Ships";

        private readonly string _AccountsEndpoint = @"​/api​/Admin/Accounts";
        private readonly string _getAccountEndpoint = @"​/api​/Admin/Accounts";
        private readonly string _getAccountsEndpoint = @"​/api​/Admin/GetAccount";
        private readonly string _addSpacePortEndpoint = @"​/api​/Admin/AddSpacePort";
        private readonly string _SpacePortEnabledEndpoint = @"​/api​/Admin/SpacePortEnabled";
        private readonly string _DeleteSpacePortEndpoint = @"​/api​/Admin/DeleteSpacePort";
        private readonly string _UpdateSpacePortPriceEndpoint = @"​/api​/Admin/UpdateSpacePortPrice";
        private readonly string _UpdateSpacePortNameEndpoint = @"​/api​/Admin/UpdateSpacePortName";
        private readonly string _PromoteAdminEndpoint = @"​/api​/Admin/PromoteAdmin";
        private readonly string _DemoteAdminEndpoint = @"​/api​/Admin/DemoteAdmin";
        public async Task Login(string accountName, string password)
        {
            var json = JsonConvert.SerializeObject(new
            {
                AccountName = accountName,
                Password = password
            });
            string token = null;
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(_loginEndpoint, data).ConfigureAwait(continueOnCapturedContext: false);

            var responseText = await response.Content.ReadAsStringAsync();
            var requestText = await response.RequestMessage.Content.ReadAsStringAsync();
            string jsonResponse = JsonConvert.SerializeObject(response, Formatting.Indented);
            Console.Write(jsonResponse);
            Log(response, responseText, requestText);
            if (!response.IsSuccessStatusCode) return;

            var loginResponseValuePairs = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseText);
            token = loginResponseValuePairs.GetValueOrDefault("token");
            _token = token;
        }

        private void Log(HttpResponseMessage response, string responseText = null, string requestText = null)
        {
            using (FileStream fs = new FileStream(@"C:\Users\axels\Google Drive\Code\VS Code\code-webbutveckling-backend\spaceparkv2-buddygroup6-renegades\Source\StarwarsConsoleClient\Networking\API-Client_LOG.txt"
                                     , FileMode.OpenOrCreate
                                     , FileAccess.ReadWrite))
            {
                StreamWriter writer = new StreamWriter(fs);
                Console.WriteLine("---------------------------------");
                Console.WriteLine(DateTime.Now.ToString("d"));
                Console.WriteLine("Method: " + response.RequestMessage.Method);
                Console.WriteLine("Request Uri: " + response.RequestMessage.RequestUri);
                Console.WriteLine("Response Code: " + response.StatusCode);
                Console.Write("RequestText: " + requestText);
                Console.WriteLine();
                Console.Write("Content: " + response.Content);
                Console.WriteLine();
                Console.WriteLine("HEADERS");
                foreach (var h in response.Headers)
                    Console.Write(h.Key + " : " + h.Value);
                Console.Write("Content: " + response.Headers);
                Console.WriteLine(responseText);
                Console.WriteLine("---------------------------------");
            }
        }
    }
}
