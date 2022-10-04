namespace Test.FoodTest.ControllerTests;

public class GettingFoodTests : BaseTest
{
    [Fact]
    public async Task Get_WhenCalled_ReturnsAllItems()
    {
        _serviceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(GetAll());
        var resultAct = await _controller.GetFoods();

        var okResult = resultAct.Result as OkObjectResult;
        var result = okResult.Value as IEnumerable<FoodResponse>;

        Assert.Equal(3, result.Count());
    }

    [Fact]
    public async Task GetById_UnknownGuidPassed_ReturnsNotFoundResponse()
    {
        var foodIdTest = Guid.NewGuid();
        _serviceMock.Setup(s => s.GetAsync(foodIdTest))
            .Callback(() => throw new NotFoundException(nameof(Food), foodIdTest));

        await Assert.ThrowsAsync<NotFoundException>(async ()=> await _controller.GetFood(foodIdTest));
    }

    [Fact]
    public async Task GetById_ExistingGuidPassed_RightItem()
    {
        var id = new Guid("D25AD448-3FA4-468E-AF78-839EA8A570C3");
        
        var first = GetAll().First();

        _serviceMock.Setup(s => s.GetAsync(id)).ReturnsAsync(first);
        var resultAct = await _controller.GetFood(id);

        var okResult = resultAct.Result as OkObjectResult;
        var result = okResult.Value as FoodResponse;

        Assert.Same(result,first);
    }
}
