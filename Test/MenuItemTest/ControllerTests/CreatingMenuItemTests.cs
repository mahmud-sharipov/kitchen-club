namespace Test.MenuItemTest.ControllerTests;

public class CreatingMenuItemTests : BaseTest
{
    [Fact]
    public async void Create_ValidObjectPassed_ReturnsCreatedResponse()
    {
        var menuId = new Guid("3C8AF117-B57B-461B-9F4E-76C22BA101B4"); 
        var foodId = new Guid("31801C4C-CBD2-45D6-AF8A-3B21210562D9");
        var createMenuitemTest = new CreateMenuItem(DateTime.Now.AddDays(2), foodId, menuId);

        var response = new MenuItemResponse(Guid.NewGuid(), 
            createMenuitemTest.Day, createMenuitemTest.FoodId, createMenuitemTest.MenuId, true);

        _serviceMock.Setup(s => s.CreateAsync(createMenuitemTest)).ReturnsAsync(response);
        var resultAct = await _controller.PostMenuItem(createMenuitemTest);
        var okResult = resultAct.Result as CreatedAtActionResult;
        var result = okResult.Value as MenuItemResponse;

        Assert.Same(response, result);
    }
}
