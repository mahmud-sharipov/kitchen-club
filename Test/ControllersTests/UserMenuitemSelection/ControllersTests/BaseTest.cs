namespace Test.UserMenuitemSelection.ControllersTests;

public abstract class BaseTest
{
    protected UserMenuItemSelectionController _controller;
    protected IUserMenuitemSelectionService _service;
    protected Mock<IUserMenuitemSelectionService> _serviceMock;

    public BaseTest()
    {
        _serviceMock = new Mock<IUserMenuitemSelectionService>();
        _service = _serviceMock.Object;
        _controller = new UserMenuItemSelectionController(_service);
    }

    public static IEnumerable<UserMenuitemSelectionResponse> GetAll()
    {
        var result = new List<UserMenuitemSelectionResponse>();

        foreach (var item in Context.UserMenuItemSelections)
        {
            result.Add(new UserMenuitemSelectionResponse(item.Id, item.MenuitemId, item.UserId, item.Vote));
        }
        return result;
    }
}
