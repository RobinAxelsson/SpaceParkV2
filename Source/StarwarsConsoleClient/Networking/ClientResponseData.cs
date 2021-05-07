using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace StarwarsConsoleClient.Networking
{
    public class ClientResponseData
    {
        public string TimeStamp { get; private set; }
        public string StatusCode { get; private set; }
        public bool IsSuccessStatusCode { get; private set; }
        public string RequestHeaders { get; private set; }
        public string RequestMethod { get; private set; }
        public string RequestUri { get; private set; }
        public string RequestContentString { get; private set; }
        public string ResponseHeaders { get; private set; }
        public string ResponseContentString { get { return JsonFormat(_responseContentString); } }
        private string _responseContentString;
        public T ResponseAsObject<T>() => JsonConvert.DeserializeObject<T>(_responseContentString);
        public string GetResponseValueWithKey(string key)
        {
            var loginResponseValuePairs = JsonConvert.DeserializeObject<Dictionary<string, string>>(ResponseContentString);
            var val = loginResponseValuePairs.GetValueOrDefault(key);
            if (val == null) throw new Exception("Key could not be found");
            return val;
        }
        private ClientResponseData() { }
        public static async Task<ClientResponseData> ToData(HttpResponseMessage response)
        {
            ClientResponseData clientResponseData = null;
            using (response)
            {
                var request = response.RequestMessage;
                var requestContentString = await request.Content.ReadAsStringAsync();
                var responseContentString = await response.Content.ReadAsStringAsync();

                clientResponseData = new ClientResponseData()
                {
                    TimeStamp = DateTime.Now.ToString("G"),
                    StatusCode = response.StatusCode.ToString(),
                    RequestMethod = request.Method.Method,
                    RequestUri = request.RequestUri.AbsoluteUri,
                    RequestHeaders = JsonFormat(request.Headers),
                    RequestContentString = JsonFormat(requestContentString),
                    _responseContentString = responseContentString,
                    ResponseHeaders = JsonFormat(response.Headers),
                    IsSuccessStatusCode = response.IsSuccessStatusCode,
                };
            }
            return clientResponseData;
        }
        private static string JsonFormat(object obj) => JsonConvert.SerializeObject(obj, Formatting.Indented);
    }
}


