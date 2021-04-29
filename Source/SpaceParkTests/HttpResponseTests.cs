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
    public class HttpResponseTests: IClassFixture<WebApplicationFactory<SpacePark_API.Startup>>
    {
        public HttpClient Client { get; }

        public HttpResponseTests(WebApplicationFactory<SpacePark_API.Startup> fixture)
        {
            Client = fixture.CreateClient();
        }

        [Fact]
        public async Task Post_Register_ExpectOK()
        {
            var response = await Client.GetAsync("/api/register/lukeskywalker");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task Post_Register_ExpectNotFound()
        {
            var response = await Client.GetAsync("/api/register/luke%20skywalker/");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
