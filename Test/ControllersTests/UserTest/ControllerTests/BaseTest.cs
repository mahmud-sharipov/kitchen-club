namespace Test.UserTest.ControllerTests;

public abstract class BaseTest
{
    protected UsersController _controller;
    protected Mock<IUserService> _serviceMock;
    protected IUserService _service;

    public BaseTest()
    {
        _serviceMock = new Mock<IUserService>();
        _service = _serviceMock.Object;
        _controller = new UsersController(_service);
    }
    protected IEnumerable<UserResponse> GetAll()
    {
        var result = new List<UserResponse>();
        foreach (var item in Context.Users)
        {
            result.Add(new UserResponse(item.Id, item.FullName, item.PhoneNumber, item.Email, item.IsActive));
        }
        return result;
    }
}
