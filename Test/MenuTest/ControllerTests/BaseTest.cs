namespace Test.MenuTest.ControllerTests;

public abstract class BaseTest
{
    protected IMenuService _service;
    protected Mock<IMenuService> _serviceMock;
    protected MenuController _controller;

    public BaseTest()
    {
        _serviceMock = new Mock<IMenuService>();
        _service = _serviceMock.Object;
        _controller = new MenuController(_service);
    }

    protected IEnumerable<MenuResponse> GetAll()
    {
        var result = new List<MenuResponse>();
        foreach (var item in Context.Menu)
        {
            result.Add(new MenuResponse(item.Id, item.StartDate, item.EndDate, item.Status));
        }
        return result;
    }
}
