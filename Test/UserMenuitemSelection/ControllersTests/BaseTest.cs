namespace Test.UserMenuitemSelection.ControllersTests;

public abstract class BaseTest
{
    protected UserMenuItemSelectionController _controller;
    protected IUserMenuItemSelectionService _service;
    protected Mock<IUserMenuItemSelectionService> _serviceMock;

    public BaseTest()
    {
        _serviceMock = new Mock<IUserMenuItemSelectionService>();
        _service = _serviceMock.Object;
        _controller = new UserMenuItemSelectionController(_service);
    }

    public static IEnumerable<UserMenuItemSelectionResponse> GetAll()
    {
        var result = new List<UserMenuItemSelectionResponse>();

        foreach (var item in Context.UserMenuItemSelections)
        {
            result.Add(new UserMenuItemSelectionResponse(item.Id, item.MenuitemId, item.UserId, item.Vote));
        }
        return result;
    }
}
