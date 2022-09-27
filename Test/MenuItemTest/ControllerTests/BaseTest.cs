namespace Test.MenuItemTest.ControllerTests;

public abstract class BaseTest
{
    protected MenuItemsController _controller;
    protected IMenuItemService _service;
    protected Mock<IMenuItemService> _serviceMock;

    public BaseTest()
    {
        _serviceMock = new Mock<IMenuItemService>();
        _service = _serviceMock.Object;
        _controller = new MenuItemsController(_service);
    }

    protected IEnumerable<MenuItemResponse> GetAll()
    {
        var result = new List<MenuItemResponse>();

        foreach (var item in Context.MenuItems)
        {
            result.Add(new MenuItemResponse(item.Id, item.Day,item.FoodId,item.MenuId,item.IsActive));
        }
        return result;
    }
}
