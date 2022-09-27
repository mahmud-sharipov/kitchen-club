namespace Test.UserTest.ControllerTests;

public class CreatingUserTests : BaseTest
{
    [Fact]
    public async void Create_ValidObjectPassed_ReturnsCreatedResponse()
    {
        var rolesTest = new string[] { "User" };
        var createUserTest = new CreateUser("Testjon Testov", "+992927773377", "test@gmail.com", rolesTest, "5555");
        var response = new UserResponse(Guid.NewGuid(), createUserTest.FullName, createUserTest.PhoneNumber, 
            createUserTest.Email, true);
        _serviceMock.Setup(s => s.CreateAsync(createUserTest)).ReturnsAsync(response);

        var resultAct = await _controller.PostUser(createUserTest);
        var okResult = resultAct.Result as CreatedAtActionResult;
        var result = okResult.Value as UserResponse;

        Assert.Same(response,result);
    }
}
