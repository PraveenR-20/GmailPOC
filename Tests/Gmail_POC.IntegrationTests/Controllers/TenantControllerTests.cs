using Gmail_POC.API;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Gmail_POC.IntegrationTests.Controllers
{
    
    public class TenantControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Startup> _factory;
        
        public TenantControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _factory.ClientOptions.BaseAddress = TestHelper.GetTestBaseUrl();
            _client = _factory.CreateClient();
        }

        #region EndPoint-GETALL 

        [Fact]
        public async Task GetAll_ShouldReturnOk_WhenTenantsAvailable()
        {
            //Arrange 

            //Act
            var response = await _client.GetAsync(_factory.ClientOptions.BaseAddress);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetAll_ShouldReturnTenantDetails_WhenSuccess()
        {
            //Arrange 

            //Act
            var response = await _client.GetAsync(_factory.ClientOptions.BaseAddress);
            response.EnsureSuccessStatusCode();
            JArray outputData = JsonConvert.DeserializeObject<JArray>(await response.Content.ReadAsStringAsync());

            //Assert
            Assert.NotNull(outputData.First["tenantId"]);
            Assert.NotNull(outputData.First["tenantName"]);
        }

        #endregion

        #region Endpoint-GET{id}
        [Fact]
        public async Task Get_ShouldReturnOk_WhenValidTenantId()
        {
            //Arrange 
            long tenantId = await GetValidTenantId();
            var requestUri = $"{_factory.ClientOptions.BaseAddress}/{tenantId}";

            //Act
            var response = await _client.GetAsync(requestUri);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Get_ShouldReturnNotFound_WhenInvalidTenantId()
        {
            //Arrange 
            long tenantId = -1;
            var requestUri = $"{_factory.ClientOptions.BaseAddress}/{tenantId}";

            //Act
            var response = await _client.GetAsync(requestUri);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Get_ShouldReturnTenantDetails_WhenValidTenantId()
        {
            //Arrange 
            long tenantId = await GetValidTenantId(); //TO-DO Get a Valid Id from DB
            var requestUri = $"{_factory.ClientOptions.BaseAddress}/{tenantId}";

            //Act
            var response = await _client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var outputData = JObject.Parse(await response.Content.ReadAsStringAsync());
            //Assert
            Assert.NotNull(outputData["tenantId"]);
            Assert.NotNull(outputData["tenantName"]);
        }

        #endregion

        #region Endpoint-POST
        [Fact]
        public async Task Post_ShouldReturnCreated_WhenValidInput()
        {
            //Arrange 
            var inputData = JsonConvert.SerializeObject(new
            {
                tenantName = $"TenantName_{DateTime.Now}"
            });

            //Act
            var response = await _client.PostAsync(_factory.ClientOptions.BaseAddress, new StringContent(inputData, Encoding.UTF8, "application/json"));

            //Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task Post_ShouldReturnBadRequest_WhenInvalidRequest()
        {
            //Arrange 
            var inputData = JsonConvert.SerializeObject(new
            {
                nonExistingProp = "This is invalid property"
            });

            //Act
            var response = await _client.PostAsync(_factory.ClientOptions.BaseAddress, new StringContent(inputData, Encoding.UTF8, "application/json"));

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Post_ShouldReturnBadRequest_WhenDuplicateTenantName()
        {
            string existingTenantName = await GetExistingTenantName();
            //Arrange 
            var inputData = JsonConvert.SerializeObject(new
            {
                tenantName = existingTenantName
            });

            //Act
            var response = await _client.PostAsync(_factory.ClientOptions.BaseAddress, new StringContent(inputData, Encoding.UTF8, "application/json"));

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Post_CreatedDataCanBeRetrived_WhenPostSuccessful()
        {
            //Arrange 
            var inputData = JsonConvert.SerializeObject(new
            {
                tenantName = $"TenantName_{DateTime.Now}"
            });

            //Act
            var response = await _client.PostAsync(_factory.ClientOptions.BaseAddress, new StringContent(inputData, Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
            var outputData = JObject.Parse(await response.Content.ReadAsStringAsync());
            var getResponse = await _client.GetAsync($"{_factory.ClientOptions.BaseAddress}/{outputData.GetValue("tenantId")}");

            //Assert
            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        }

        [Fact]
        public async Task Post_ShouldReturnTenantId_WhenPostSuccessful()
        {
            //Arrange 
            var inputData = JsonConvert.SerializeObject(new
            {
                tenantName = $"TenantName_{DateTime.Now}"
            });

            //Act
            var response = await _client.PostAsync(_factory.ClientOptions.BaseAddress, new StringContent(inputData, Encoding.UTF8, "application/json"));

            //Assert
            response.EnsureSuccessStatusCode();

            var outputData = JObject.Parse(await response.Content.ReadAsStringAsync());
            //Assert
            Assert.NotNull(outputData["tenantId"]);
        }

        [Fact]
        public async Task Post_ShouldReturnLocationHeader_WhenPostSuccessful()
        {
            //Arrange 
            var inputData = JsonConvert.SerializeObject(new
            {
                tenantName = $"TenantName_{DateTime.Now}"
            });

            //Act
            var response = await _client.PostAsync(_factory.ClientOptions.BaseAddress, new StringContent(inputData, Encoding.UTF8, "application/json"));

            //Assert
            response.EnsureSuccessStatusCode();
            var outputData = JObject.Parse(await response.Content.ReadAsStringAsync());
            var expectedUri = $"{ _factory.ClientOptions.BaseAddress}/{outputData.GetValue("tenantId")}";

            Assert.NotNull(response.Headers.Location);
            Assert.Equal(expectedUri, response.Headers.Location.AbsoluteUri);
        }

        #endregion

        #region Helper Functions

        private async Task<long> GetValidTenantId()
        {
            var response = await _client.GetAsync(_factory.ClientOptions.BaseAddress);
            response.EnsureSuccessStatusCode();
            JArray outputData = JsonConvert.DeserializeObject<JArray>(await response.Content.ReadAsStringAsync());
            long tenantId = long.Parse(outputData.First["tenantId"].ToString());
            //Assert
            return tenantId;
        }

        private async Task<string> GetExistingTenantName()
        {
            var response = await _client.GetAsync(_factory.ClientOptions.BaseAddress);
            JArray outputData = JsonConvert.DeserializeObject<JArray>(await response.Content.ReadAsStringAsync());
            //Assert
            return outputData.First["tenantName"].ToString();
        }
        #endregion
    }
}
