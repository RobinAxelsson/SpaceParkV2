using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace StarwarsConsoleClient.Networking
{
    public class ClientResponseData
    {
        public string TimeStamp { get; private set; }
        public string StatusCode { get; private set; }
        public bool IsSuccessStatusCode { get; private set; }
        public HttpRequestHeaders RequestHeaders { get; private set; }
        public HttpResponseHeaders ResponseHeaders { get; private set; }
        public string RequestMethod { get; private set; }
        public string RequestUri { get; private set; }
        public string RequestContentString { get; private set; }
        public string ResponseContentString { get; set; }
        public T ResponseAsObject<T>() => JsonConvert.DeserializeObject<T>(ResponseContentString);
        public string GetResponseValueWithKey(string key)
        {
            var loginResponseValuePairs = JsonConvert.DeserializeObject<Dictionary<string, string>>(ResponseContentString);
            var val = loginResponseValuePairs.GetValueOrDefault(key);
            return val;
        }
        private ClientResponseData() { }
        public static async Task<ClientResponseData> ToData(HttpResponseMessage response)
        {
            ClientResponseData clientResponseData = null;
            using (response)
            {
                var responseContentString = await response.Content.ReadAsStringAsync();
                var request = response.RequestMessage;
                var requestContentString = await request.Content.ReadAsStringAsync();

                clientResponseData = new ClientResponseData()
                {
                    TimeStamp = DateTime.Now.ToString("G"),
                    StatusCode = response.StatusCode.ToString(),
                    RequestMethod = request.Method.Method,
                    RequestUri = request.RequestUri.AbsoluteUri,
                    RequestHeaders = request.Headers,
                    ResponseContentString = responseContentString,
                    RequestContentString = requestContentString,
                    ResponseHeaders = response.Headers,
                    IsSuccessStatusCode = response.IsSuccessStatusCode,
                };
            }
            return clientResponseData;
        }
    }
}


