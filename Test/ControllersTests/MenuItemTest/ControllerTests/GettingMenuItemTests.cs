namespace Test.MenuItemTest.ControllerTests;

public class GettingMenuItemTests : BaseTest
{
    [Fact]
    public async Task Get_WhenCalled_ReturnsAllItems()
    {
        _serviceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(GetAll());
        var resultAct = await _controller.GetMenuitems();

        var okResult = resultAct.Result as OkObjectResult;
        var result = okResult.Value as IEnumerable<MenuitemResponse>;

        Assert.Equal(4, result.Count());
    }

    [Fact]
    public async Task GetById_UnknownGuidPassed_ReturnsNotFoundResponse()
    {
        var id = Guid.NewGuid();
        _serviceMock.Setup(s => s.GetAsync(id)).Callback(() => throw new NotFoundException(nameof(Menuitem), id));

        await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.GetMenuitem(id));
    }

    [Fact]
    public async Task GetById_ExistingGuidPassed_ReturnsMenuItemResponse()
    {
        var id = new Guid("E273ABD8-411B-4243-B00A-E5EF92ADAFA1");
        var response = GetAll().First();

        _serviceMock.Setup(s => s.GetAsync(id)).ReturnsAsync(response);
        var resultAct = await _controller.GetMenuitem(id);
        var okObject = resultAct.Result as OkObjectResult;

        var result = okObject.Value as MenuitemResponse;

        Assert.Same(response, result);
    }
}