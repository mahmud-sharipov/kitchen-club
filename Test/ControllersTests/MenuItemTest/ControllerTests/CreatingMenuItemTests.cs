namespace Test.MenuItemTest.ControllerTests;

public class CreatingMenuItemTests : BaseTest
{
    [Fact]
    public async Task Create_ValidObjectPassed_ReturnsCreatedResponse()
    {
        var menuId = new Guid("3C8AF117-B57B-461B-9F4E-76C22BA101B4"); 
        var foodId = new Guid("31801C4C-CBD2-45D6-AF8A-3B21210562D9");
        var createMenuitemTest = new CreateMenuitem(DateTime.Now.AddDays(2), foodId, menuId);

        var response = new MenuitemResponse(Guid.NewGuid(), 
            createMenuitemTest.Day, createMenuitemTest.FoodId, createMenuitemTest.MenuId, true);

        _serviceMock.Setup(s => s.CreateAsync(createMenuitemTest)).ReturnsAsync(response);
        var resultAct = await _controller.PostMenuitem(createMenuitemTest);
        var okResult = resultAct.Result as CreatedAtActionResult;
        var result = okResult.Value as MenuitemResponse;

        Assert.Same(response, result);
    }
}
