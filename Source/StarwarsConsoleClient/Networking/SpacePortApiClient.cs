using Newtonsoft.Json;
using StarwarsConsoleClient.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StarwarsConsoleClient.Networking
{
    public partial class SpacePortApiClient
    {
        private HttpClient _client;
        private HttpFileLog _log;
        public SpacePortApiClient(string baseUrl, string logFile)
        {
            var client = new HttpClient();
            var log = new HttpFileLog(logFile);
            client.BaseAddress = new Uri(baseUrl);
            _client = client;
        }
        private string _token;

        #region Hard coded string endpoints
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
        #endregion

        private async Task<HttpResponseMessage> JsonPostRequest(string endpoint, object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_loginEndpoint, data).ConfigureAwait(continueOnCapturedContext: false);
            return response;
        }
        public async Task<bool> Register(string accountName, string password)
        {
            var response = await JsonPostRequest(_loginEndpoint, new
            {
                username = accountName,
                password = password
            });
            var responseData = await ClientResponseData.ToData(response);

            if (!response.IsSuccessStatusCode) return false;
            var token = responseData.GetResponseValueWithKey("token");
            _token = token;

            return token != null;
        }
        public async Task<bool> Login(string accountName, string password)
        {
            var response = await JsonPostRequest(_loginEndpoint, new
            {
                username = accountName,
                password = password
            });
            var responseData = await ClientResponseData.ToData(response);

            if (!response.IsSuccessStatusCode) return false;
           var token = responseData.GetResponseValueWithKey("token");
            _token = token;

            return token != null;
        }
    }

}

