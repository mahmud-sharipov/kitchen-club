namespace Test.FoodTest;

public abstract class BaseTest
{
    protected IFoodService _service;
    protected Mock<IFoodService> _serviceMock;
    protected FoodsController _controller;

    public BaseTest()
    {
        _serviceMock = new Mock<IFoodService>();
        _service = _serviceMock.Object;
        _controller = new FoodsController(_service);
    }

    protected IEnumerable<FoodResponse> GetAll()
    {
        var result = new List<FoodResponse>();
        foreach (var item in Context.Foods)
        {
            result.Add(new FoodResponse(item.Id, item.Name, item.Image, item.Description, item.IsActive));
        }

        return result;
    }
}
