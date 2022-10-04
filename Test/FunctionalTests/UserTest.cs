namespace Test.FunctionalTests;

public class UserTest
{
    private WebApplicationFactory<KitchenClube.Program> _server;
    private HttpClient _client;
    private string _token;
    private string _url;
    private Guid _userTestId;

    public UserTest()
    {
        _server = new WebApplicationFactory<KitchenClube.Program>();
        _client = _server.CreateClient();
        _token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9zaWQiOiIwOGRhOGMxMS1iOWU4LTQwNTItOGFiOC1kMGNlZTJkMWYzNjgiLCJyb2xlIjpbIlVzZXIiLCJBZG1pbiJdLCJuYmYiOjE2NjQ1MTAyOTQsImV4cCI6MTY2NDU0NjI5NCwiaWF0IjoxNjY0NTEwMjk0fQ.iQo6so1sLwYPB-VebmwfJrXhmd1bbx81LY8pwYcWpCI";
        _url = "api/1/Users/";
        _userTestId = new Guid("08daa2bd-8e6d-473b-8734-f250f28f5d1f");
    }

    [Fact]
    public async Task CreateUser_FunctionalTest()
    {
        var data = CreateUserData("", "", "", new string[0], "");
        var responseMessage = await _client.PostAsync(_url, data);
        Assert.Equal(HttpStatusCode.Unauthorized, responseMessage.StatusCode);

        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", _token);

        //BadRequest = wrong name passing
        responseMessage = await _client.PostAsync(_url, data);
        Assert.Equal(HttpStatusCode.BadRequest, responseMessage.StatusCode);

        //BadRequest = wrong email passing
        data = CreateUserData("Test", "+992-92-888-88-99", "name@", new string[0], "");
        responseMessage = await _client.PostAsync(_url, data);
        Assert.Equal(HttpStatusCode.BadRequest, responseMessage.StatusCode);

        //BadRequest = wrong role passing
        data = CreateUserData("Test", "+992-92-888-88-99", "name@gmail.com", new string[1] { "rerer" }, "");
        responseMessage = await _client.PostAsync(_url, data);
        Assert.Equal(HttpStatusCode.BadRequest, responseMessage.StatusCode);

        //BadRequest = short password passing
        data = CreateUserData("Test", "+992-92-888-88-99", "name@gmail.com", new string[1] { "User" }, "123");
        responseMessage = await _client.PostAsync(_url, data);
        Assert.Equal(HttpStatusCode.BadRequest, responseMessage.StatusCode);

        //BadRequest = wrong password passing
        data = CreateUserData("TestName", "+992-92-888-88-99", "test@gmail.com", new string[1] { "User" }, "1234567");
        responseMessage = await _client.PostAsync(_url, data);
        Assert.Equal(HttpStatusCode.BadRequest, responseMessage.StatusCode);

        //BadRequest = existing email passing
        data = CreateUserData("TestName", "+992-92-888-88-99", "string@gmail.com", new string[1] { "User" }, "1234567");
        responseMessage = await _client.PostAsync(_url, data);
        Assert.Equal(HttpStatusCode.BadRequest, responseMessage.StatusCode);

        data = CreateUserData("TestName", "+992-92-888-88-99", "testing@gmail.com", new string[1] { "User" }, "1234567abc");
        responseMessage = await _client.PostAsync(_url, data);
        Assert.Equal(HttpStatusCode.Created, responseMessage.StatusCode);
    }

    [Fact]
    public async Task GetUsers_FunctionalTest()
    {
        var responseMessage = await _client.GetAsync(_url);
        Assert.Equal(HttpStatusCode.Unauthorized, responseMessage.StatusCode);

        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", _token);

        responseMessage = await _client.GetAsync(_url);
        Assert.Equal(HttpStatusCode.OK, responseMessage.StatusCode);
    }

    [Fact]
    public async Task GetUser_FunctionalTest()
    {
        var responseMessage = await _client.GetAsync(_url + Guid.NewGuid());
        Assert.Equal(HttpStatusCode.Unauthorized, responseMessage.StatusCode);

        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", _token);

        //NotFound = unknown user id passing
        responseMessage = await _client.GetAsync(_url + Guid.NewGuid());
        Assert.Equal(HttpStatusCode.NotFound, responseMessage.StatusCode);

        responseMessage = await _client.GetAsync(_url + _userTestId);
        Assert.Equal(HttpStatusCode.OK, responseMessage.StatusCode);
    }

    [Fact]
    public async Task GetUserRoles_FunctionalTest()
    {
        var responseMessage = await _client.GetAsync(_url + "roles/" + Guid.NewGuid());
        Assert.Equal(HttpStatusCode.Unauthorized, responseMessage.StatusCode);

        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", _token);

        //NotFound = unknown user id passing
        responseMessage = await _client.GetAsync(_url + "roles/" + Guid.NewGuid());
        Assert.Equal(HttpStatusCode.NotFound, responseMessage.StatusCode);

        responseMessage = await _client.GetAsync(_url + "roles/" + _userTestId);
        Assert.Equal(HttpStatusCode.OK, responseMessage.StatusCode);
    }

    [Fact]
    public async Task UpdateUser_FunctionalTest()
    {
        var data = UpdateUserData("", "", true);
        var responseMessage = await _client.PutAsync(_url + Guid.NewGuid(), data);
        Assert.Equal(HttpStatusCode.Unauthorized, responseMessage.StatusCode);

        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", _token);

        //Notfound = unknown user id passing
        data = UpdateUserData("Test", "999", true);
        responseMessage = await _client.PutAsync(_url + Guid.NewGuid(), data);
        Assert.Equal(HttpStatusCode.NotFound, responseMessage.StatusCode);

        //Badrequest = wrong name passing
        data = UpdateUserData("", "999", true);
        responseMessage = await _client.PutAsync(_url + _userTestId, data);
        Assert.Equal(HttpStatusCode.BadRequest, responseMessage.StatusCode);

        //Badrequest = wrong phone number passing
        data = UpdateUserData("Test", "", true);
        responseMessage = await _client.PutAsync(_url + _userTestId, data);
        Assert.Equal(HttpStatusCode.BadRequest, responseMessage.StatusCode);

        data = UpdateUserData("TestingName", "+992-92-888-77-99", true);
        responseMessage = await _client.PutAsync(_url + _userTestId, data);
        Assert.Equal(HttpStatusCode.NoContent, responseMessage.StatusCode);
    }

    [Fact]
    public async Task UpdateUserRoles_FunctionalTest()
    {
        var data = UpdateUserRolesData(new List<string> { "string" });
        var responseMessage = await _client.PutAsync(_url + "roles/" + Guid.NewGuid(), data);
        Assert.Equal(HttpStatusCode.Unauthorized, responseMessage.StatusCode);

        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", _token);

        //Badrequest = wrong roles passing
        data = UpdateUserRolesData(new List<string>());
        responseMessage = await _client.PutAsync(_url + "roles/" + _userTestId, data);
        Assert.Equal(HttpStatusCode.BadRequest, responseMessage.StatusCode);

        //InternamServerError = wrong role name passing
        data = UpdateUserRolesData(new List<string> { "string" });
        responseMessage = await _client.PutAsync(_url + "roles/" + _userTestId, data);
        Assert.Equal(HttpStatusCode.InternalServerError, responseMessage.StatusCode);

        //Notfound = wrong user id passing
        data = UpdateUserRolesData(new List<string> { "User" });
        responseMessage = await _client.PutAsync(_url + "roles/" + Guid.NewGuid(), data);
        Assert.Equal(HttpStatusCode.NotFound, responseMessage.StatusCode);

        data = UpdateUserRolesData(new List<string> { "User" });
        responseMessage = await _client.PutAsync(_url + "roles/" + _userTestId, data);
        Assert.Equal(HttpStatusCode.NoContent, responseMessage.StatusCode);
    }

    [Fact]
    public async Task DeleteUser_FunctionalTest()
    {
        var responseMessage = await _client.DeleteAsync(_url + Guid.NewGuid());
        Assert.Equal(HttpStatusCode.Unauthorized, responseMessage.StatusCode);

        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", _token);

        //Notfound = unknown user id passing
        responseMessage = await _client.DeleteAsync(_url + Guid.NewGuid());
        Assert.Equal(HttpStatusCode.NotFound, responseMessage.StatusCode);

        //Badrequest = user id in usermenuitemselection passing
        responseMessage = await _client.DeleteAsync(_url + new Guid("08da8c11-b9e8-4052-8ab8-d0cee2d1f368"));
        Assert.Equal(HttpStatusCode.BadRequest, responseMessage.StatusCode);

        responseMessage = await _client.DeleteAsync(_url + _userTestId);
        Assert.Equal(HttpStatusCode.NoContent, responseMessage.StatusCode);
    }

    private StringContent UpdateUserRolesData(List<string> roles)
    {
        var updateUser = new UpdateUserRole(roles);
        var json = JsonConvert.SerializeObject(updateUser);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }

    private StringContent UpdateUserData(string name, string phone, bool activity)
    {
        var updateUser = new UpdateUser(name, phone, activity);
        var json = JsonConvert.SerializeObject(updateUser);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }

    private StringContent CreateUserData(string name, string phone, string email, string[] roles, string password)
    {
        var createUser = new CreateUser(name, phone, email, roles, password);
        var json = JsonConvert.SerializeObject(createUser);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }
}
