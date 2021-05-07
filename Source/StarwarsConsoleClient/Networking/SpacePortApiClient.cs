using Newtonsoft.Json;
using StarwarsConsoleClient.Main;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StarwarsConsoleClient.Networking
{
    public partial class SpacePortApiClient
    {
        private HttpClient _client;
        private RequestLogger _log;
        public SpacePortApiClient(string baseUrl, string logFile)
        {
            var client = new HttpClient();
            var log = new RequestLogger(logFile);
            client.BaseAddress = new Uri(baseUrl);
            _log = log;
            _client = client;
        }

        public void OpenLogFile() => Process.Start("notepad.exe", _log.FilePath);

        private static class EndPoints
        {
            public static class Account
            {
                public static readonly string register =            @"/api/Account/Register";
                public static readonly string login =               @"/api/Account/Login";
                public static readonly string changeSpaceShip =     @"/api/Account/ChangeSpaceShip";
                public static readonly string getHomeworld =        @"​/api​/Account​/GetHomeworld";
                public static readonly string Ships =               @"​/api​/Account​/Ships";
                public static readonly string MyShip =              @"api/Account/MySpaceShip";
                public static readonly string MyData =              @"api/Account/MyData";
            }
            public static class Parking
            {
                public static readonly string park =                @"​/api​/Parking/Park";
                public static readonly string getSpacePorts =       @"​/api​/Parking​/GetSpacePorts";
                public static readonly string parkingPrice =        @"​/api​/Parking​/Price";

            }
            public static class Admin
            {
                public static readonly string Accounts =            @"​/api​/Admin/Accounts";
                public static readonly string getAccount =          @"​/api​/Admin/Accounts";
                public static readonly string getAccounts =         @"​/api​/Admin/GetAccount";
                public static readonly string addSpacePort =        @"​/api​/Admin/AddSpacePort";
                public static readonly string SpacePortEnabled =    @"​/api​/Admin/SpacePortEnabled";
                public static readonly string DeleteSpacePort =     @"​/api​/Admin/DeleteSpacePort";
                public static readonly string UpdateSpacePortPrice =@"​/api​/Admin/UpdateSpacePortPrice";
                public static readonly string UpdateSpacePortName = @"​/api​/Admin/UpdateSpacePortName";
                public static readonly string PromoteAdmin =        @"​/api​/Admin/PromoteAdmin";
                public static readonly string DemoteAdmin =         @"​/api​/Admin/DemoteAdmin";
            }
        }

        private async Task<HttpResponseMessage> JsonPostRequestAsync(string endpoint, object obj)
        {
            string json = "";
            if(obj != null) json = JsonConvert.SerializeObject(obj);

            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(endpoint, data).ConfigureAwait(continueOnCapturedContext: false);
            return response;
        }
        private async Task<HttpResponseMessage> GetRequestAsync(string endpoint, string queryString = "")
        {
            var response = await _client.GetAsync(endpoint + queryString);
            return response;
        }
        private string QueryStringParse(object queryObj)
        {
            NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);
            PropertyInfo[] infos = queryObj.GetType().GetProperties();
            foreach (var info in infos)
            {
                queryString.Add(info.Name, info.GetValue(queryObj, null).ToString());
            }
            return '?' + queryString.ToString();
        }

        #region Account Requests
        public async Task<bool> RegisterAsync(string spaceShipModel, string name, string accountName, string password)
        {
            var response = await JsonPostRequestAsync(EndPoints.Account.register, new
            {
                name,
                accountName,
                password,
                spaceShipModel,
            });
            var responseData = await ClientResponseData.ToData(response);
            _log.LogClientData(responseData);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> LoginAsync(string accountName, string password)
        {
            var response = await JsonPostRequestAsync(EndPoints.Account.login, new
            {
                username = accountName,
                password = password
            });
            var responseData = await ClientResponseData.ToData(response);
            _log.LogClientData(responseData);
            var token = responseData.GetResponseValueWithKey("token");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return responseData?.IsStatusCode ?? false;
        }
        public async Task<bool> ChangeSpaceShipAsync(string spaceshipModel)
        {
            var response = await JsonPostRequestAsync(EndPoints.Account.changeSpaceShip + "/" + spaceshipModel, null);
            var responseData = await ClientResponseData.ToData(response);
            _log.LogClientData(responseData);
            return response.IsSuccessStatusCode;
        }
        public async Task<Homeworld> GetHomeworldAsync()
        {
            var response = await JsonPostRequestAsync(EndPoints.Account.getHomeworld, null);
            var responseData = await ClientResponseData.ToData(response);
            _log.LogClientData(responseData);
            var homeworld = responseData.ResponseAsObject<Homeworld>();
            return homeworld;
        }
        public async Task<SpaceShip> MySpaceShipAsync()
        {
            var response = await JsonPostRequestAsync(EndPoints.Account.MyShip, null); //
            var responseData = await ClientResponseData.ToData(response);
            _log.LogClientData(responseData);
            var spaceShip = responseData.ResponseAsObject<SpaceShip>();
            return spaceShip;
        }
        public async Task<Person> MyDataAsync()
        {
            var response = await JsonPostRequestAsync(EndPoints.Account.MyData, null); //
            var responseData = await ClientResponseData.ToData(response);
            _log.LogClientData(responseData);
            var person = responseData.ResponseAsObject<Person>();
            return person;
        }
        public async Task<IEnumerable<SpaceShip>> GetShipsAsync()
        {
            var response = await GetRequestAsync(EndPoints.Account.Ships);
            var responseData = await ClientResponseData.ToData(response);
            _log.LogClientData(responseData);
            var ships = responseData.ResponseAsObject<IEnumerable<SpaceShip>>();
            return ships;
        }

        #endregion
        #region Parking Requests
        public async Task<bool> Park(double minutes, int spacePortId)
        {
            var response = await JsonPostRequestAsync(EndPoints.Parking.park, new
            {
                minutes,
                spacePortId
            });
            var responseData = await ClientResponseData.ToData(response);
            _log.LogClientData(responseData);
            return response.IsSuccessStatusCode;
        }
        public async Task<IEnumerable<SpacePort>> GetSpacePortsAsync()
        {
            var response = await GetRequestAsync(EndPoints.Parking.getSpacePorts);
            var responseData = await ClientResponseData.ToData(response);
            _log.LogClientData(responseData);
            var ships = responseData.ResponseAsObject<IEnumerable<SpacePort>>();
            return ships;
        }
        public async Task<decimal> GetPriceAsync(int spacePortId, string spaceShipModel, double minutes)
        {
            var query = QueryStringParse(new { spacePortId, spaceShipModel, minutes });
            var response = await GetRequestAsync(EndPoints.Parking.parkingPrice, query);
            var responseData = await ClientResponseData.ToData(response);
            _log.LogClientData(responseData);
            var price = decimal.Parse(responseData.ResponseContentString);
            return price;
        }
        #endregion
        
        
    }

}

