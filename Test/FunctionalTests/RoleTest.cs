namespace Test.FunctionalTests;

public class RoleTest
{
    private WebApplicationFactory<KitchenClube.Program> _server;
    private HttpClient _client;
    private string _url;
    private string _token;
    private Guid _roleTestId;

    public RoleTest()
    {
        _server = new WebApplicationFactory<KitchenClube.Program>();
        _client = _server.CreateClient();
        _url = "api/1/roles/";
        _token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9zaWQiOiIwOGRhOGMxMS1iOWU4LTQwNTItOGFiOC1kMGNlZTJkMWYzNjgiLCJyb2xlIjpbIlVzZXIiLCJBZG1pbiJdLCJuYmYiOjE2NjQ1MTAyOTQsImV4cCI6MTY2NDU0NjI5NCwiaWF0IjoxNjY0NTEwMjk0fQ.iQo6so1sLwYPB-VebmwfJrXhmd1bbx81LY8pwYcWpCI";
        _roleTestId = new Guid("08daa2bc-ffd4-4e90-8404-dd812195993b");
    }

    [Fact]
    public async Task CreateRole_FunctionalTest()
    {
        var data = CreateRoleData("nameRole");
        var responseMessage = await _client.PostAsync(_url, data);
        Assert.Equal(HttpStatusCode.Unauthorized, responseMessage.StatusCode);

        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", _token);

        //BadRequest = wrong role name passing
        data = CreateRoleData("");
        responseMessage = await _client.PostAsync(_url, data);
        Assert.Equal(HttpStatusCode.BadRequest, responseMessage.StatusCode);

        data = CreateRoleData("Name");
        responseMessage = await _client.PostAsync(_url, data);
        Assert.Equal(HttpStatusCode.Created, responseMessage.StatusCode);
    }

    [Fact]
    public async Task GetRoles_FunctionalTest()
    {
        var responseMessage = await _client.GetAsync(_url);
        Assert.Equal(HttpStatusCode.Unauthorized, responseMessage.StatusCode);

        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", _token);

        responseMessage = await _client.GetAsync(_url);
        Assert.Equal(HttpStatusCode.OK, responseMessage.StatusCode);
    }

    [Fact]
    public async Task GetRole_FunctionalTest()
    {
        var url = _url + Guid.NewGuid();
        var responseMessage = await _client.GetAsync(_url + Guid.NewGuid());
        Assert.Equal(HttpStatusCode.Unauthorized, responseMessage.StatusCode);

        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", _token);

        //NotFound = unknown role id passing
        responseMessage = await _client.GetAsync(_url + Guid.NewGuid());
        Assert.Equal(HttpStatusCode.NotFound, responseMessage.StatusCode);

        responseMessage = await _client.GetAsync(_url + _roleTestId);
        Assert.Equal(HttpStatusCode.OK, responseMessage.StatusCode);
    }

    [Fact]
    public async Task UpdateRole_FunctionalTest()
    {
        var data = UpdateRoleData("name");
        var responseMessage = await _client.PutAsync(_url + Guid.NewGuid(), data);
        Assert.Equal(HttpStatusCode.Unauthorized, responseMessage.StatusCode);

        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", _token);

        //BadRequest = wrong role name passing
        data = UpdateRoleData("");
        responseMessage = await _client.PutAsync(_url + _roleTestId, data);
        Assert.Equal(HttpStatusCode.BadRequest, responseMessage.StatusCode);

        //BadRequest = wrong role name passing
        data = UpdateRoleData("RoleNameTest");
        responseMessage = await _client.PutAsync(_url + Guid.NewGuid(), data);
        Assert.Equal(HttpStatusCode.NotFound, responseMessage.StatusCode);

        data = UpdateRoleData("TestRoleName");
        responseMessage = await _client.PutAsync(_url + _roleTestId, data);
        Assert.Equal(HttpStatusCode.NoContent, responseMessage.StatusCode);
    }

    [Fact]
    public async Task DeleteRole_FunctionalTest()
    {
        var url = _url + Guid.NewGuid();
        var responseMessage = await _client.DeleteAsync(_url + Guid.NewGuid());
        Assert.Equal(HttpStatusCode.Unauthorized, responseMessage.StatusCode);

        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", _token);

        //NotFound = unknown role id passing
        responseMessage = await _client.DeleteAsync(_url + Guid.NewGuid());
        Assert.Equal(HttpStatusCode.NotFound, responseMessage.StatusCode);

        responseMessage = await _client.DeleteAsync(_url + _roleTestId);
        Assert.Equal(HttpStatusCode.NoContent, responseMessage.StatusCode);
    }

    private StringContent UpdateRoleData(string name)
    {
        var createRole = new CreateRole(name);
        var json = JsonConvert.SerializeObject(createRole);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }

    private StringContent CreateRoleData(string nameRole)
    {
        var createRole = new CreateRole(nameRole);
        var json = JsonConvert.SerializeObject(createRole);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }
}
