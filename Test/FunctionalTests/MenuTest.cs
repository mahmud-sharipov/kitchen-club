namespace Test.FunctionalTests;

public class MenuTest
{
    private HttpClient _client;
    private WebApplicationFactory<KitchenClube.Program> _server;
    private string _token;
    private string _url;
    private Guid _menuTestId;

    public MenuTest()
    {
        _server = new WebApplicationFactory<KitchenClube.Program>();
        _client = _server.CreateClient();
        _token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9zaWQiOiIwOGRhOGMxMS1iOWU4LTQwNTItOGFiOC1kMGNlZTJkMWYzNjgiLCJyb2xlIjpbIlVzZXIiLCJBZG1pbiJdLCJuYmYiOjE2NjQ1MTAyOTQsImV4cCI6MTY2NDU0NjI5NCwiaWF0IjoxNjY0NTEwMjk0fQ.iQo6so1sLwYPB-VebmwfJrXhmd1bbx81LY8pwYcWpCI";
        _url = "api/1/menu/";
        _menuTestId = new Guid("08daa2be-d20d-42ef-8476-823d1a7fe6ca");
    }

    [Fact]
    public async Task CreateMenu_FunctionalTest()
    {
        var dateTimeNow = DateTime.Now;
        var createMenu = new CreateMenu(dateTimeNow, dateTimeNow);
        var json = JsonConvert.SerializeObject(createMenu);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var responseMessage = await _client.PostAsync(_url, data);
        Assert.Equal(HttpStatusCode.Unauthorized, responseMessage.StatusCode);

        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", _token);

        //BadRequest = same start and end date passing 
        responseMessage = await _client.PostAsync(_url, data);
        Assert.Equal(HttpStatusCode.BadRequest, responseMessage.StatusCode);

        createMenu = new CreateMenu(dateTimeNow, DateTime.Now.AddDays(1));
        json = JsonConvert.SerializeObject(createMenu);
        data = new StringContent(json, Encoding.UTF8, "application/json");

        responseMessage = await _client.PostAsync(_url, data);
        Assert.Equal(HttpStatusCode.Created, responseMessage.StatusCode);
    }

    [Fact]
    public async Task GetAllMenu_FunctionalTest()
    {
        var responseMessage = await _client.GetAsync(_url);
        Assert.Equal(HttpStatusCode.Unauthorized, responseMessage.StatusCode);

        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", _token);

        responseMessage = await _client.GetAsync(_url);
        Assert.Equal(HttpStatusCode.OK, responseMessage.StatusCode);
    }

    [Fact]
    public async Task GetMenu_FunctionalTest()
    {
        var responseMessage = await _client.GetAsync(_url + Guid.NewGuid());
        Assert.Equal(HttpStatusCode.Unauthorized, responseMessage.StatusCode);

        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", _token);

        //NotFound = unknown menu id passing
        responseMessage = await _client.GetAsync(_url + Guid.NewGuid());
        Assert.Equal(HttpStatusCode.NotFound, responseMessage.StatusCode);

        responseMessage = await _client.GetAsync(_url + _menuTestId);
        Assert.Equal(HttpStatusCode.OK, responseMessage.StatusCode);
    }

    [Fact]
    public async Task UpdateMenu_FunctionalTest()
    {
        var updateFood = new UpdateMenu(DateTime.Today, DateTime.Today);
        var json = JsonConvert.SerializeObject(updateFood);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var responseMessage = await _client.PutAsync(_url + _menuTestId, data);
        Assert.Equal(HttpStatusCode.Unauthorized, responseMessage.StatusCode);

        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", _token);

        //BadRequest = unknown menu id passing
        responseMessage = await _client.PutAsync(_url + Guid.NewGuid(), data);
        Assert.Equal(HttpStatusCode.BadRequest, responseMessage.StatusCode);

        //BadRequest =  same dates passing
        responseMessage = await _client.PutAsync(_url + _menuTestId, data);
        Assert.Equal(HttpStatusCode.BadRequest, responseMessage.StatusCode);

        //BadRequest = menu id in menuitem passing
        responseMessage = await _client.PutAsync(_url + new Guid("fbb70259-8cb1-4b3d-9da6-4a0b0586d45b"), data);
        Assert.Equal(HttpStatusCode.BadRequest, responseMessage.StatusCode);

        //BadRequest = closed menu id passing
        responseMessage = await _client.PutAsync(_url + new Guid("08da7132-2365-4d5a-8f10-2bcbc603f4fd"), data);
        Assert.Equal(HttpStatusCode.BadRequest, responseMessage.StatusCode);

        //BadRequest = greater start date passing
        updateFood = new UpdateMenu(DateTime.Today.AddDays(2), DateTime.Today);
        json = JsonConvert.SerializeObject(updateFood);
        data = new StringContent(json, Encoding.UTF8, "application/json");

        responseMessage = await _client.PutAsync(_url + _menuTestId, data);
        Assert.Equal(HttpStatusCode.BadRequest, responseMessage?.StatusCode);

        //NoContent
        updateFood = new UpdateMenu(DateTime.Now, DateTime.Now.AddDays(2));
        json = JsonConvert.SerializeObject(updateFood);
        data = new StringContent(json, Encoding.UTF8, "application/json");

        responseMessage = await _client.PutAsync(_url + _menuTestId, data);
        Assert.Equal(HttpStatusCode.NoContent, responseMessage.StatusCode);
    }

    [Fact]
    public async Task UpdateStatusCloseMenu_FunctionalTest()
    {
        var responseMessage = await _client.PutAsync(_url + _menuTestId + "/close", null);
        Assert.Equal(HttpStatusCode.Unauthorized, responseMessage.StatusCode);

        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", _token);

        //NotFound = unknown menu id passing
        responseMessage = await _client.PutAsync(_url + Guid.NewGuid() + "/close", null);
        Assert.Equal(HttpStatusCode.NotFound, responseMessage.StatusCode);

        responseMessage = await _client.PutAsync(_url + _menuTestId + "/close", null);
        Assert.Equal(HttpStatusCode.NoContent, responseMessage.StatusCode);
    }

    [Fact]
    public async Task UpdateStatusOpenClose_FunctionalTest()
    {
        var responseMessage = await _client.PutAsync(_url + _menuTestId + "/open", null);
        Assert.Equal(HttpStatusCode.Unauthorized, responseMessage.StatusCode);

        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", _token);
        
        //NotFound = unknown menu id passing
        responseMessage = await _client.PutAsync(_url + Guid.NewGuid() + "/open", null);
        Assert.Equal(HttpStatusCode.NotFound, responseMessage.StatusCode);

        responseMessage = await _client.PutAsync(_url + _menuTestId + "/open", null);
        Assert.Equal(HttpStatusCode.NoContent, responseMessage.StatusCode);
    }

    [Fact]
    public async Task DeleteMenu_FunctionalTest()
    {
        var responseMessage = await _client.DeleteAsync(_url + _menuTestId);
        Assert.Equal(HttpStatusCode.Unauthorized, responseMessage.StatusCode);

        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", _token);

        //NotFound = unknown menu id passing
        responseMessage = await _client.DeleteAsync(_url + Guid.NewGuid());
        Assert.Equal(HttpStatusCode.NotFound, responseMessage.StatusCode);

        responseMessage = await _client.DeleteAsync(_url + _menuTestId);
        Assert.Equal(HttpStatusCode.NoContent, responseMessage?.StatusCode);
    }
}