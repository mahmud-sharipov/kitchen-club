namespace Test.MenuTest.ControllerTests;

public class CreatingMenuTests : BaseTest
{
    [Fact]
    public async Task CreateMenu_ValidObjectPassed_ReturnsCreatedResponse()
    {
        var testMenu = new CreateMenu(DateTime.Now, DateTime.Now.AddDays(10));
        var response = new MenuResponse(Guid.NewGuid(), testMenu.StartDate, testMenu.EndDate, MenuStatus.Active);

        _serviceMock.Setup(s => s.CreateAsync(testMenu)).ReturnsAsync(response);
        var createdFood = await _controller.PostMenu(testMenu);

        var result = createdFood.Result as CreatedAtActionResult;

        Assert.Same(result.Value, response);
    }
}
