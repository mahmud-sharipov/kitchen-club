namespace Test.FunctionalTests;

public class MenuitemTest
{
    private WebApplicationFactory<KitchenClube.Program> _server;
    private HttpClient _client;
    private string _token;
    private string _url;
    private Guid _menuitemTestId;
    public MenuitemTest()
    {
        _server = new WebApplicationFactory<KitchenClube.Program>();
        _client = _server.CreateClient();
        _token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9zaWQiOiIwOGRhOGMxMS1iOWU4LTQwNTItOGFiOC1kMGNlZTJkMWYzNjgiLCJyb2xlIjpbIlVzZXIiLCJBZG1pbiJdLCJuYmYiOjE2NjQ1MTAyOTQsImV4cCI6MTY2NDU0NjI5NCwiaWF0IjoxNjY0NTEwMjk0fQ.iQo6so1sLwYPB-VebmwfJrXhmd1bbx81LY8pwYcWpCI";
        _url = "api/1/MenuItems/";
        _menuitemTestId = new Guid("08daa2c1-b546-44d9-8afd-f6dcc0df0105");
    }

    [Fact]
    public async Task CreateMenuitem_FunctionalTest()
    {
        var menuId = new Guid("08da874c-4c55-4067-8abb-aac66c68695a");
        var foodId = new Guid("08da874a-b452-49de-81ad-3ba65ca4a7ad");
        var data = CreateMenuitemData(DateTime.Now.AddYears(1), foodId, menuId);

        var responseMessage = await _client.PostAsync(_url, data);
        Assert.Equal(HttpStatusCode.Unauthorized, responseMessage.StatusCode);

        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", _token);

        //BadRequest: wrong day passing
        responseMessage = await _client.PostAsync(_url, data);
        Assert.Equal(HttpStatusCode.BadRequest, responseMessage.StatusCode);

        //BadRequest: unknown food id passing
        data = CreateMenuitemData(DateTime.Now, Guid.NewGuid(), menuId);
        responseMessage = await _client.PostAsync(_url, data);
        Assert.Equal(HttpStatusCode.NotFound, responseMessage.StatusCode);

        //BadRequest: unknown menu id passing
        data = CreateMenuitemData(DateTime.Now, foodId, Guid.NewGuid());
        responseMessage = await _client.PostAsync(_url, data);
        Assert.Equal(HttpStatusCode.NotFound, responseMessage.StatusCode);

        //Created
        data = CreateMenuitemData(DateTime.Today.AddMonths(1), foodId, menuId);
        responseMessage = await _client.PostAsync(_url, data);
        Assert.Equal(HttpStatusCode.Created, responseMessage.StatusCode);
    }

    [Fact]
    public async Task GetAllMenuitems_FunctionalTest()
    {
        var responseMessage = await _client.GetAsync(_url);
        Assert.Equal(HttpStatusCode.Unauthorized, responseMessage.StatusCode);

        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", _token);

        responseMessage = await _client.GetAsync(_url);

        Assert.Equal(HttpStatusCode.OK, responseMessage.StatusCode);
    }

    [Fact]
    public async Task GetMenuitem_FunctionalTest()
    {
        var responseMessage = await _client.GetAsync(_url);
        Assert.Equal(HttpStatusCode.Unauthorized, responseMessage.StatusCode);

        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", _token);

        //NotFound = unknown menuitem id passing
        responseMessage = await _client.GetAsync(_url + Guid.NewGuid());
        Assert.Equal(HttpStatusCode.NotFound, responseMessage.StatusCode);

        responseMessage = await _client.GetAsync(_url + _menuitemTestId);
        Assert.Equal(HttpStatusCode.OK, responseMessage.StatusCode);
    }

    [Fact]
    public async Task GetMenuitemByMenuId_FunctionalTest()
    {
        var responseMessage = await _client.GetAsync(_url + "menu/" + Guid.NewGuid());
        Assert.Equal(HttpStatusCode.Unauthorized, responseMessage.StatusCode);

        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", _token);

        //NotFound = unknown menu id passing
        responseMessage = await _client.GetAsync(_url + "menu/" + Guid.NewGuid());
        Assert.Equal(HttpStatusCode.NotFound, responseMessage.StatusCode);

        responseMessage = await _client.GetAsync(_url + "menu/" + new Guid("fbb70259-8cb1-4b3d-9da6-4a0b0586d45b"));
        Assert.Equal(HttpStatusCode.OK, responseMessage.StatusCode);
    }

    [Fact]
    public async Task GetMenuitemByFoodId_FunctionalTest()
    {
        var responseMessage = await _client.GetAsync(_url + "food/" + Guid.NewGuid());
        Assert.Equal(HttpStatusCode.Unauthorized, responseMessage.StatusCode);

        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", _token);

        //NotFound = unknown food id passing
        responseMessage = await _client.GetAsync(_url + "food/" + Guid.NewGuid());
        Assert.Equal(HttpStatusCode.NotFound, responseMessage.StatusCode);

        responseMessage = await _client.GetAsync(_url + "food/" + new Guid("3f6a9572-91b4-4f92-81cd-a2037de19de4"));
        Assert.Equal(HttpStatusCode.OK, responseMessage.StatusCode);
    }

    [Fact]
    public async Task UpdateMenuitem_FunctionalTest()
    {
        var menuId = new Guid("08da874c-4c55-4067-8abb-aac66c68695a");
        var foodId = new Guid("08da874a-b452-49de-81ad-3ba65ca4a7ad");

        var data = UpdateMenuitemData(DateTime.Now, menuId, foodId);
        var responseMessage = await _client.PutAsync(_url + Guid.NewGuid(), data);
        Assert.Equal(HttpStatusCode.Unauthorized, responseMessage.StatusCode);

        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", _token);

        //NotFound: unknown menuitem id passing
        responseMessage = await _client.PutAsync(_url + Guid.NewGuid(), data);
        Assert.Equal(HttpStatusCode.NotFound, responseMessage.StatusCode);

        //NotFound: unknown food id passing       
        data = UpdateMenuitemData(DateTime.Now, menuId, Guid.NewGuid());
        responseMessage = await _client.PutAsync(_url + _menuitemTestId, data);
        Assert.Equal(HttpStatusCode.NotFound, responseMessage.StatusCode);

        //NotFound: unknown menu id passing       
        data = UpdateMenuitemData(DateTime.Now, Guid.NewGuid(), foodId);
        responseMessage = await _client.PutAsync(_url + _menuitemTestId, data);
        Assert.Equal(HttpStatusCode.NotFound, responseMessage.StatusCode);

        //BadRequest: greater day passing       
        data = UpdateMenuitemData(DateTime.Now.AddYears(1), menuId, foodId);
        responseMessage = await _client.PutAsync(_url + _menuitemTestId, data);
        Assert.Equal(HttpStatusCode.BadRequest, responseMessage.StatusCode);

        //BadRequest: less day passing       
        data = UpdateMenuitemData(DateTime.Now.AddYears(-1), menuId, foodId);
        responseMessage = await _client.PutAsync(_url + _menuitemTestId, data);
        Assert.Equal(HttpStatusCode.BadRequest, responseMessage.StatusCode);

        //BadRequest: past menuitem updating
        data = UpdateMenuitemData(DateTime.Now, menuId, foodId);
        responseMessage = await _client.PutAsync(_url + new Guid("08da7140-1e24-47dc-80f6-410004bab02b"), data);
        Assert.Equal(HttpStatusCode.BadRequest, responseMessage.StatusCode);

        //BadRequest: users' selected menuitem updating
        data = UpdateMenuitemData(DateTime.Now, menuId, foodId);
        responseMessage = await _client.PutAsync(_url + new Guid("08da9c5b-3f13-40e1-884e-ebdee571e3c6"), data);
        Assert.Equal(HttpStatusCode.BadRequest, responseMessage.StatusCode);

        //NoContent
        data = UpdateMenuitemData(DateTime.Today.AddDays(10), menuId, foodId);
        responseMessage = await _client.PutAsync(_url + _menuitemTestId, data);
        Assert.Equal(HttpStatusCode.NoContent, responseMessage.StatusCode);
    }

    [Fact]
    public async Task DeleteMenuitem_FunctionalTest()
    {
        var responseMessage = await _client.DeleteAsync(_url + Guid.NewGuid());
        Assert.Equal(HttpStatusCode.Unauthorized, responseMessage.StatusCode);

        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", _token);

        //NotFound = unknown menuitemId passing
        responseMessage = await _client.DeleteAsync(_url + Guid.NewGuid());
        Assert.Equal(HttpStatusCode.NotFound, responseMessage.StatusCode);

        //BadRequest = past menuitem deleting
        responseMessage = await _client.DeleteAsync(_url + new Guid("08da7140-1e24-47dc-80f6-410004bab02b"));
        Assert.Equal(HttpStatusCode.BadRequest, responseMessage.StatusCode);

        //BadRequest = menuitem in usermenuitemselection existing deleting
        responseMessage = await _client.DeleteAsync(_url + new Guid("08da9c5b-3f13-40e1-884e-ebdee571e3c6"));
        Assert.Equal(HttpStatusCode.BadRequest, responseMessage.StatusCode);

        //NoContent
        responseMessage = await _client.DeleteAsync(_url + _menuitemTestId);
        Assert.Equal(HttpStatusCode.NoContent, responseMessage.StatusCode);
    }

    private StringContent CreateMenuitemData(DateTime dateTime, Guid foodId, Guid menuId)
    {
        var createMenuitem = new CreateMenuitem(dateTime, foodId, menuId);
        var json = JsonConvert.SerializeObject(createMenuitem);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }

    private StringContent UpdateMenuitemData(DateTime dateTime, Guid menuId, Guid foodId)
    {
        var updateMenuitem = new UpdateMenuitem(dateTime, foodId, menuId, true);
        var json = JsonConvert.SerializeObject(updateMenuitem);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }

}
