using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using SpacePark_API;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;


//https://timdeschryver.dev/blog/how-to-test-your-csharp-web-api

namespace SpaceParkTests
{
    public class WeatherForecastControllerTests: IClassFixture<WebApplicationFactory<SpacePark_API.Startup>>
    {
        public HttpClient Client { get; }

        public WeatherForecastControllerTests(WebApplicationFactory<SpacePark_API.Startup> fixture)
        {
            Client = fixture.CreateClient();
        }

        [Fact]
        public async Task Get_Should_Retrieve_Forecast()
        {
            var response = await Client.GetAsync("/weatherforecast/");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task Get_Should_Retrieve_()
        {
            var response = await Client.GetAsync("/weatherforecast/");

            var forecast = JsonConvert.DeserializeObject<WeatherForecast[]>(await response.Content.ReadAsStringAsync());
            Assert.Equal(5, forecast.Length);
        }
    }
}
