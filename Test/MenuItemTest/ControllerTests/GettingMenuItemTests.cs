namespace Test.MenuItemTest.ControllerTests;

public class GettingMenuItemTests : BaseTest
{
    [Fact]
    public async void Get_WhenCalled_ReturnsAllItems()
    {
        _serviceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(GetAll());
        var resultAct = await _controller.GetMenuItems();

        var okResult = resultAct.Result as OkObjectResult;
        var result = okResult.Value as IEnumerable<MenuItemResponse>;

        Assert.Equal(4, result.Count());
    }

    [Fact]
    public async void GetById_UnknownGuidPassed_ReturnsNotFoundResponse()
    {
        var id = Guid.NewGuid();
        _serviceMock.Setup(s => s.GetAsync(id)).Callback(() => throw new NotFoundException(nameof(MenuItem), id));

        await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.GetMenuItem(id));
    }

    [Fact]
    public async void GetById_ExistingGuidPassed_ReturnsMenuItemResponse()
    {
        var id = new Guid("E273ABD8-411B-4243-B00A-E5EF92ADAFA1");
        var response = GetAll().First();

        _serviceMock.Setup(s => s.GetAsync(id)).ReturnsAsync(response);
        var resultAct = await _controller.GetMenuItem(id);
        var okObject = resultAct.Result as OkObjectResult;

        var result = okObject.Value as MenuItemResponse;

        Assert.Same(response, result);
    }
}