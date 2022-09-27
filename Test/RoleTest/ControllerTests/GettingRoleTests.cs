namespace Test.RoleTest.ControllerTests;

public class GettingRoleTests : BaseTest
{
    [Fact]
    public async void Get_WhenCalled_ReturnsAListOfRoleResponse()
    {
        _serviceMock.Setup(s=>s.GetAllAsync()).ReturnsAsync(GetAll());
        var resultAct = await _controller.GetRoles();
        var okObject = resultAct.Result as OkObjectResult;
        var result = okObject.Value as IEnumerable<RoleResponse>;

        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async void GetById_UnknownRoleIdPassed_ReturnsNotFoundResponse()
    {
        var roleIdTest = Guid.NewGuid();

        _serviceMock.Setup(s => s.GetAsync(roleIdTest))
            .Callback(() => throw new NotFoundException(nameof(Role), roleIdTest));

        await Assert.ThrowsAsync<NotFoundException>(async ()=> await _controller.GetRole(roleIdTest));
    }

    [Fact]
    public async void GetById_ExistingRoleIdPassed_ReturnsRoleResponse()
    {
        var roleIdTest = new Guid("5A527020-AE7A-4BA4-BD8D-9BFE06B3535C");
        var response = GetAll().First();

        _serviceMock.Setup(s=>s.GetAsync(roleIdTest)).ReturnsAsync(response);
        var resultAct = await _controller.GetRole(roleIdTest);
        var okObject = resultAct.Result as OkObjectResult;
        var result = okObject.Value as RoleResponse;

        Assert.Same(response, result);
    }
}
