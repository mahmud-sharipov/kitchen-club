namespace Test.UserTest.ControllerTests;

public class GettingUserTests : BaseTest
{
    [Fact]
    public async Task Get_WhenCalled_ReturnsAllItems()
    {
        _serviceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(GetAll());
        var resultAct = await _controller.GetUsers();
        var okObject = resultAct.Result as OkObjectResult;
        var result = okObject.Value as IEnumerable<UserResponse>;

        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetById_UnknownGuidPassed_ReturnsNotFoundResponse()
    {
        var id = Guid.NewGuid();
        _serviceMock.Setup(s => s.GetAsync(id)).Callback(() => throw new NotFoundException(nameof(User), id));

        await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.GetUser(id));
    }

    [Fact]
    public async Task GetById_ExistingGuidPassed_ReturnsUserResponse()
    {
        var id = new Guid("2EA3E5EB-CBCB-4A67-B868-61B96F9A9D60");
        var response = GetAll().FirstOrDefault();
        _serviceMock.Setup(s => s.GetAsync(id)).ReturnsAsync(response);

        var resultAct = await _controller.GetUser(id);
        var okObject = resultAct.Result as OkObjectResult;
        var result = okObject.Value as UserResponse;

        Assert.Same(response, result);
    }

    [Fact]
    public async Task GetRoles_UnknownGuidPassed_ReturnsNotfoundResponse()
    {
        var userId = Guid.NewGuid();
        _serviceMock.Setup(s => s.GetRolesAsync(userId)).Callback(()=>throw new NotFoundException(nameof(User),userId));

        await Assert.ThrowsAsync<NotFoundException>(async ()=>await _controller.GetRoles(userId));
    }

    [Fact]
    public async Task GetRoles_ValidObjectPassed_ReturnsRolesOfUser()
    {
        var userId = new Guid("2EA3E5EB-CBCB-4A67-B868-61B96F9A9D60");

        var roles = new string[]
        {
            "Admin",
            "User"
        };

        _serviceMock.Setup(s => s.GetRolesAsync(userId)).ReturnsAsync(roles);

        var resultAct = await _controller.GetRoles(userId);
        var okObject = resultAct.Result as OkObjectResult;
        var result = okObject.Value as IEnumerable<string>;

        Assert.Same(roles, result);
    }
}
