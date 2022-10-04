namespace Test.RoleTest.ControllerTests;

public class BaseTest
{
    protected readonly RolesController _controller;
    protected readonly Mock<IRoleService> _serviceMock;
    protected readonly IRoleService _service;

    public BaseTest()
    {
        _serviceMock = new Mock<IRoleService>();
        _service = _serviceMock.Object;
        _controller = new RolesController(_service);
    }

    protected IEnumerable<RoleResponse> GetAll()
    {
        var result = new List<RoleResponse>();
        foreach (var item in Context.Roles)
        {
            result.Add(new RoleResponse(item.Id, item.Name, item.IsActive));
        }
        return result;
    }
}
