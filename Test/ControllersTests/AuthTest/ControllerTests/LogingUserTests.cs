namespace Test.AuthTest.ControllerTests;

public class LogingUserTests
{
    private Mock<IAuthService> _servicesMock;
    private IAuthService _service;
    private AuthController _controller;

    public LogingUserTests()
    {
        _servicesMock = new Mock<IAuthService>();
        _service = _servicesMock.Object;
        _controller = new AuthController(_service);
    }
    [Fact]
    public void Login_UnknownLoginDataPassed_ReturnsNotFoundResponse()
    {
        var email = "test@gmail.com";
        var password = "123";
        var loginUserTest = new LoginUser(email, password);

        _servicesMock.Setup(s => s.Login(loginUserTest)).Callback(() => throw new BadRequestException(""));
        Assert.ThrowsAsync<BadRequestException>(async () => await _controller.Login(loginUserTest));
    }

    [Fact]
    public async Task Login_ExistingLoginDataPassed_ReturnsLoginResponse()
    {
        var email = "nomalum1@gmail.com";
        var password = "1234567";
        var loginUserTest = new LoginUser(email, password);
        var response = new LoginResponse("Token");

        _servicesMock.Setup(s => s.Login(loginUserTest)).ReturnsAsync(response);
        var resultAct = await _controller.Login(loginUserTest);
        var okObject = resultAct.Result as OkObjectResult;
        var result = okObject.Value as LoginResponse;

        Assert.Same(response, result);
    }
}
