namespace Test.FunctionalTests;

public class UserMenuitemSelectionTest
{
    private WebApplicationFactory<KitchenClube.Program> _server;
    private HttpClient _client;
    private string _token;
    private string _url;
    private Guid _userMenuitemSelectionTestId;

    public UserMenuitemSelectionTest()
    {
        _server = new WebApplicationFactory<KitchenClube.Program>();
        _client = _server.CreateClient();
        _token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9zaWQiOiIwOGRhOGMxMS1iOWU4LTQwNTItOGFiOC1kMGNlZTJkMWYzNjgiLCJyb2xlIjpbIlVzZXIiLCJBZG1pbiJdLCJuYmYiOjE2NjQ4NTYyMzgsImV4cCI6MTY2NDg5MjIzNywiaWF0IjoxNjY0ODU2MjM4fQ.PQr-YIJAbOYx6PykWhTjqwcfovHCAJpAZIjQ32xJCqA";
        _url = "api/1/UserMenuItemSelection/";
        _userMenuitemSelectionTestId = new Guid("08daa2c6-24b9-40ac-860f-8b5cb8ac1fad");
    }

    [Fact]
    public async Task CreateUserMenuitem_FunctionalTest()
    {
        var data = CreateUMS(UserVote.Yes, Guid.NewGuid());

        var responseMessage = await _client.PostAsync(_url, data);
        Assert.Equal(HttpStatusCode.Unauthorized, responseMessage.StatusCode);

        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", _token);

        //NotFound = wrong menuitem id passing
        responseMessage = await _client.PostAsync(_url, data);
        Assert.Equal(HttpStatusCode.NotFound, responseMessage.StatusCode);

        //BadRequest = past menuitem id passing
        data = CreateUMS(UserVote.Yes, new Guid("08da7140-1e24-47dc-80f6-410004bab02b"));
        responseMessage = await _client.PostAsync(_url, data);
        Assert.Equal(HttpStatusCode.BadRequest, responseMessage.StatusCode);

        //Created
        data = CreateUMS(UserVote.No, new Guid("08da874a-b452-49de-81ad-3ba65ca4b756"));
        responseMessage = await _client.PostAsync(_url, data);
        Assert.Equal(HttpStatusCode.Created, responseMessage.StatusCode);
    }

    [Fact]
    public async Task GetUserMenuitemSelections_FunctionalTest()
    {
        var responseMessage = await _client.GetAsync(_url);
        Assert.Equal(HttpStatusCode.Unauthorized, responseMessage.StatusCode);

        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", _token);

        responseMessage = await _client.GetAsync(_url);
        Assert.Equal(HttpStatusCode.OK, responseMessage.StatusCode);
    }

    [Fact]
    public async Task GetUserMenuitemSelection_FunctionalTest()
    {
        var responseMessage = await _client.GetAsync(_url + Guid.NewGuid());
        Assert.Equal(HttpStatusCode.Unauthorized, responseMessage.StatusCode);

        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", _token);

        responseMessage = await _client.GetAsync(_url + Guid.NewGuid());
        Assert.Equal(HttpStatusCode.NotFound, responseMessage.StatusCode);

        responseMessage = await _client.GetAsync(_url + _userMenuitemSelectionTestId);
        Assert.Equal(HttpStatusCode.OK, responseMessage.StatusCode);
    }

    [Fact]
    public async Task GetUserMenuitemSelectionByUserId_FunctionalTest()
    {
        var responseMessage = await _client.GetAsync(_url + "user/" + Guid.NewGuid());
        Assert.Equal(HttpStatusCode.Unauthorized, responseMessage.StatusCode);

        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", _token);

        //NotFound = unknown user id passing
        responseMessage = await _client.GetAsync(_url + "user/" + Guid.NewGuid());
        Assert.Equal(HttpStatusCode.NotFound, responseMessage.StatusCode);

        //Badrequest = usermenuitemselections with such user id do not exist
        responseMessage = await _client.GetAsync(_url + "user/" + new Guid("08da8c07-fa47-43fa-85cc-8202edf7b0bb"));
        Assert.Equal(HttpStatusCode.BadRequest, responseMessage.StatusCode);

        responseMessage = await _client.GetAsync(_url + "user/" + new Guid("08da8c11-b9e8-4052-8ab8-d0cee2d1f368"));
        Assert.Equal(HttpStatusCode.OK, responseMessage.StatusCode);
    }

    [Fact]
    public async Task GetUserMenuitemSelectionByMenuitemId_FunctionalTest()
    {
        var responseMessage = await _client.GetAsync(_url + "menuitem/" + Guid.NewGuid());
        Assert.Equal(HttpStatusCode.Unauthorized, responseMessage.StatusCode);

        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", _token);

        //NotFound = unknown menuitem id passing
        responseMessage = await _client.GetAsync(_url + "menuitem/" + Guid.NewGuid());
        Assert.Equal(HttpStatusCode.NotFound, responseMessage.StatusCode);

        //Badrequest = usermenuitemselections with such menuitem id do not exist
        responseMessage = await _client.GetAsync(_url + "menuitem/" + new Guid("08da7140-1e24-47dc-80f6-410004bab02b"));
        Assert.Equal(HttpStatusCode.BadRequest, responseMessage.StatusCode);

        responseMessage = await _client.GetAsync(_url + "menuitem/" + new Guid("08da7140-4111-404c-8d47-714a4b7ac085"));
        Assert.Equal(HttpStatusCode.OK, responseMessage.StatusCode);
    }

    [Fact]
    public async Task UpdateUserMenuitemSelection_FunctionalTest()
    {
        var data = UpdateUMS(UserVote.Yes, Guid.NewGuid(), Guid.NewGuid());
        var responseMessage = await _client.PutAsync(_url + Guid.NewGuid(), data);
        Assert.Equal(HttpStatusCode.Unauthorized, responseMessage.StatusCode);

        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", _token);

        //BadRequest = unknown usermenuitem id passing
        data = UpdateUMS(UserVote.Yes, Guid.NewGuid(), Guid.NewGuid());
        responseMessage = await _client.PutAsync(_url + Guid.NewGuid(), data);
        Assert.Equal(HttpStatusCode.NotFound, responseMessage.StatusCode);

        //BadRequest = unknown menuitem id passing
        data = UpdateUMS(UserVote.Yes, Guid.NewGuid(), new Guid("08da8c11-b9e8-4052-8ab8-d0cee2d1f368"));
        responseMessage = await _client.PutAsync(_url + _userMenuitemSelectionTestId, data);
        Assert.Equal(HttpStatusCode.NotFound, responseMessage.StatusCode);

        //BadRequest = unknown user id passing
        data = UpdateUMS(UserVote.Yes, new Guid("08da874a-b452-49de-81ad-3ba65ca4b756"),
           Guid.NewGuid());
        responseMessage = await _client.PutAsync(_url + _userMenuitemSelectionTestId, data);
        Assert.Equal(HttpStatusCode.NotFound, responseMessage.StatusCode);

        data = UpdateUMS(UserVote.Yes, new Guid("08da874a-b452-49de-81ad-3ba65ca4b756"),
            new Guid("08da8c11-b9e8-4052-8ab8-d0cee2d1f368"));
        responseMessage = await _client.PutAsync(_url + _userMenuitemSelectionTestId, data);
        Assert.Equal(HttpStatusCode.NoContent, responseMessage.StatusCode);
    }

    [Fact]
    public async Task DeleteUserMenuitemSelection_FunctionalTest()
    {
        var responseMessage = await _client.DeleteAsync(_url + Guid.NewGuid());
        Assert.Equal(HttpStatusCode.Unauthorized, responseMessage.StatusCode);

        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", _token);

        responseMessage = await _client.DeleteAsync(_url + Guid.NewGuid());
        Assert.Equal(HttpStatusCode.NotFound, responseMessage.StatusCode);

        //Badrequest = past menuitem's day passing
        responseMessage = await _client.DeleteAsync(_url + new Guid("08da7140-4111-404c-8d47-714a4b7ac076"));
        Assert.Equal(HttpStatusCode.BadRequest, responseMessage.StatusCode);

        responseMessage = await _client.DeleteAsync(_url + _userMenuitemSelectionTestId);
        Assert.Equal(HttpStatusCode.NoContent, responseMessage.StatusCode);
    }

    private StringContent UpdateUMS(UserVote vote, Guid menuitem, Guid userId)
    {
        var updateUMS = new UpdateUserMenuitemSelection(vote, menuitem, userId);
        var json = JsonConvert.SerializeObject(updateUMS);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }

    private StringContent CreateUMS(UserVote vote, Guid menuitemId)
    {
        var createUserMenuitemSelection = new CreateUserMenuitemSelection(vote, menuitemId);
        var json = JsonConvert.SerializeObject(createUserMenuitemSelection);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }
}
