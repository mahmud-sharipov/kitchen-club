namespace Test.MenuItemTest.ControllerTests;

public abstract class BaseTest
{
    protected MenuitemsController _controller;
    protected IMenuitemService _service;
    protected Mock<IMenuitemService> _serviceMock;

    public BaseTest()
    {
        _serviceMock = new Mock<IMenuitemService>();
        _service = _serviceMock.Object;
        _controller = new MenuitemsController(_service);
    }

    protected IEnumerable<MenuitemResponse> GetAll()
    {
        var result = new List<MenuitemResponse>();

        foreach (var item in Context.MenuItems)
        {
            result.Add(new MenuitemResponse(item.Id, item.Day,item.FoodId,item.MenuId,item.IsActive));
        }
        return result;
    }
}
