namespace Test.RoleTest.ControllerTests;

public class CreatingRolesTest : BaseTest
{
    [Fact]
    public async void Create_ValidObjectPassed_ReturnsCreatedResponse()
    {
        var createRoleTest = new CreateRole("User");
        var response = new RoleResponse(Guid.NewGuid(),createRoleTest.Name, true);

        _serviceMock.Setup(s => s.CreateAsync(createRoleTest)).ReturnsAsync(response);
        var resultAct = await _controller.CreateRole(createRoleTest);
        var createdAction = resultAct as CreatedAtActionResult;
        var result = createdAction.Value as RoleResponse;

        Assert.Same(response, result);
    }
}
