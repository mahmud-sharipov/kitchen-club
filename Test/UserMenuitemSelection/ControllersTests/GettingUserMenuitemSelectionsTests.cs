namespace Test.UserMenuitemSelection.ControllersTests;

public class GettingUserMenuitemSelectionsTests : BaseTest
{
    [Fact]
    public async void Get_WhenCalled_ReturnsAListOfUserMenuitemSelectionResponse()
    {
        _serviceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(GetAll());
        var resultAct = await _controller.GetUserMenuItemSelections();
        var okResult = resultAct.Result as OkObjectResult;

        var result = okResult.Value as IEnumerable<UserMenuItemSelectionResponse>;

        Assert.Equal(3, result.Count());
    }

    [Fact]
    public async void GetById_UnknownGuidPassed_ReturnsNotFoundReponse()
    {
        var id = Guid.NewGuid();
        _serviceMock.Setup(s => s.GetAsync(id))
            .Callback(() => throw new NotFoundException(nameof(UserMenuItemSelection), id));

        await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.GetUserMenuItemSelection(id));
    }

    [Fact]
    public async void GetById_ExistingGuidPassed_ReturnsUserMenuItemSelectionResponse()
    {
        var id = new Guid("D75AA3B6-B166-4DCF-A1EE-9E84C2BDA0D0");
        var response = GetAll().First();

        _serviceMock.Setup(s => s.GetAsync(id)).ReturnsAsync(response);
        var resultAct = await _controller.GetUserMenuItemSelection(id);

        var okObject = resultAct.Result as OkObjectResult;
        var result = okObject.Value as UserMenuItemSelectionResponse;

        Assert.Same(response, result);
    }

    [Theory]
    [MemberData(nameof(DataTest.UserData), MemberType = typeof(DataTest))]
    public async void GetByUserId_UnknownAndExistingUserIdPassed_ReturnsUserMenuitemSelection(Guid id)
    {
        var response = GetAll().Where(u => u.UserId == id);

        _serviceMock.Setup(s => s.UserMenuItemSelectionsByUserId(id)).ReturnsAsync(response);
        var resultAct = await _controller.GetUserMenuItemSelectionsByUserId(id);
        var okResult = resultAct.Result as OkObjectResult;
        var result = okResult.Value as IEnumerable<UserMenuItemSelectionResponse>;

        Assert.Same(response, result);
    }

    [Theory]
    [MemberData(nameof(DataTest.MenuitemData), MemberType = typeof(DataTest))]
    public async void GetByMenuitemId_UnknownAndExistingMenuitemIdPassed_ReturnsUserMenuitemSelection(Guid id)
    {
        var response = GetAll().Where(u=>u.MenuitemId == id);
        _serviceMock.Setup(s=>s.UserMenuItemSelectionsByMenuitemId(id)).ReturnsAsync(response);
        var resultAct = await _controller.GetUserMenuItemSelectionsByMenuitemId(id);
        var okResult = resultAct.Result as OkObjectResult;
        var result = okResult.Value as IEnumerable<UserMenuItemSelectionResponse>;

        Assert.Same(response, result);
    }
}

class DataTest
{
    public static IEnumerable<object[]> UserData()
    {
        return new List<object[]>()
        {
            new object[]
            {
                new Guid("D75AA3B6-B166-4DCF-A1EE-9E84C2BDA0D0")
            },
            new object[]
            {
                Guid.NewGuid()
            }
        };
    }

    public static IEnumerable<object[]> MenuitemData()
    {
        return new List<object[]>()
        {
            new object[]
            {
                new Guid("E5D5421F-D750-43FE-89D9-77784D3660DD")
            },
            new object[]
            {
                Guid.NewGuid()
            }
        };
    }
}
