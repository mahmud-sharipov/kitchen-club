namespace Test.MenuTest.ControllerTests;

public class GettingMenuTests : BaseTest
{
    [Fact]
    public async Task Get_WhenCalled_ReturnsAllItems()
    {
        _serviceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(GetAll());
        var resultAct = await _controller.GetMenu();

        var okResult = resultAct.Result as OkObjectResult;
        var result = okResult.Value as IEnumerable<MenuResponse>;

        Assert.Equal(3, result.Count());
    }

    [Fact]
    public async Task GetById_UnknownGuid_Passed_ReturnsNotFoundResponse()
    {
        var id = Guid.NewGuid();
        _serviceMock.Setup(s => s.GetAsync(id)).Callback(() => throw new NotFoundException(nameof(Menu), id));

        await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.GetMenu(id));
    }

    [Fact]
    public async Task GetById_ExistingGuidPassed_ReturnsFoodResponse()
    {
        var id = new Guid("3C8AF117-B57B-461B-9F4E-76C22BA101B4");
        var response = GetAll().First();

        _serviceMock.Setup(s => s.GetAsync(id)).ReturnsAsync(response);
        var resultAct = await _controller.GetMenu(id);
        var okObject = resultAct.Result as OkObjectResult;

        var result = okObject.Value as MenuResponse;

        Assert.Same(response, result);
    }
}
