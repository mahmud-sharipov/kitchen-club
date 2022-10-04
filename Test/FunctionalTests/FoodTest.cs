namespace Test.FunctionalTests;

public class FoodTest
{
    private HttpClient _client;
    private WebApplicationFactory<KitchenClube.Program> _server;
    private string _token;
    private string _url;
    private Guid _foodTest;

    public FoodTest()
    {
        _server = new WebApplicationFactory<KitchenClube.Program>();
        _client = _server.CreateClient();
        _token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9zaWQiOiIwOGRhOGMxMS1iOWU4LTQwNTItOGFiOC1kMGNlZTJkMWYzNjgiLCJyb2xlIjpbIlVzZXIiLCJBZG1pbiJdLCJuYmYiOjE2NjQ1MTAyOTQsImV4cCI6MTY2NDU0NjI5NCwiaWF0IjoxNjY0NTEwMjk0fQ.iQo6so1sLwYPB-VebmwfJrXhmd1bbx81LY8pwYcWpCI";
        _foodTest = new Guid("08daa2be-7abb-4c82-8fae-4968bef507b0"); //created food id
        _url = "api/1/foods/";
    }

    [Fact]
    public async Task CreateFood_FunctionalTest()
    {
        var createFood = new CreateFood("TestFood", "TestImage", "TestDescription");
        var json = JsonConvert.SerializeObject(createFood);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var responseMessage = await _client.PostAsync(_url, data);
        Assert.Equal(HttpStatusCode.Unauthorized, responseMessage.StatusCode);

        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", _token);

        responseMessage = await _client.PostAsync(_url, data);
        Assert.Equal(HttpStatusCode.Created, responseMessage.StatusCode);
    }

    [Fact]
    public async Task GetAllFoods_FunctionalTest()
    {
        var responseMessage = await _client.GetAsync(_url);
        Assert.Equal(HttpStatusCode.Unauthorized, responseMessage.StatusCode);

        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", _token);

        responseMessage = await _client.GetAsync(_url);
        Assert.Equal(HttpStatusCode.OK, responseMessage.StatusCode);
    }

    [Fact]
    public async Task GetFood_FunctionalTest()
    {
        var responseMessage = await _client.GetAsync(_url + Guid.NewGuid());
        Assert.Equal(HttpStatusCode.Unauthorized, responseMessage.StatusCode);

        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", _token);

        responseMessage = await _client.GetAsync(_url + Guid.NewGuid());
        Assert.Equal(HttpStatusCode.NotFound, responseMessage.StatusCode);

        responseMessage = await _client.GetAsync(_url + _foodTest);
        Assert.Equal(HttpStatusCode.OK, responseMessage.StatusCode);
    }

    [Fact]
    public async Task UpdateFood_FunctionalTest()
    {
        var updateFood = new UpdateFood("TestFoodTesting", "", "Descr", false);
        var json = JsonConvert.SerializeObject(updateFood);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var responseMessage = await _client.PutAsync(_url + _foodTest, data);
        Assert.Equal(HttpStatusCode.Unauthorized, responseMessage.StatusCode);

        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", _token);

        responseMessage = await _client.PutAsync(_url + Guid.NewGuid(), data);
        Assert.Equal(HttpStatusCode.NotFound, responseMessage.StatusCode);

        responseMessage = await _client.PutAsync(_url + _foodTest, data);
        Assert.Equal(HttpStatusCode.NoContent, responseMessage.StatusCode);
    }

    [Fact]
    public async Task DeleteFood_FunctionalTest()
    {
        var responseMessage = await _client.DeleteAsync(_url + _foodTest);
        Assert.Equal(HttpStatusCode.Unauthorized, responseMessage.StatusCode);

        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", _token);

        responseMessage = await _client.DeleteAsync(_url + Guid.NewGuid());
        Assert.Equal(HttpStatusCode.NotFound, responseMessage.StatusCode);

        responseMessage = await _client.DeleteAsync(_url + _foodTest);
        Assert.Equal(HttpStatusCode.NoContent, responseMessage.StatusCode);
    }

}
