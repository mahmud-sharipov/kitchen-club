namespace Test.FoodTest.ControllerTests;

public class CreatingFoodTests : BaseTest
{
    [Fact]
    public async void CreateFood_ValidObjectPassed_ReturnsCreatedResponse()
    {
        var testFood = new CreateFood("Test", "TestImage", "TestDescription");
        var response = new FoodResponse(Guid.NewGuid(), "Test", "TestImage", "TestDescription", true);

        _serviceMock.Setup(s => s.CreateAsync(testFood)).ReturnsAsync(response);
        var createdFood = await _controller.PostFood(testFood);

        var result = createdFood.Result as CreatedAtActionResult;

        Assert.Same(result.Value, response);
    }
}
