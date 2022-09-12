namespace KitchenClube.Controllers.V1;

[Route("api/{v:apiVersion}/auth/")]
[ApiController]
[ApiVersion("1.0")]
public class AuthController : ControllerBase
{
    private readonly Services.V1.IAuthService _authService;
    public AuthController(Services.V1.IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(LoginUser loginUser)
    {
        return Ok(await _authService.Login(loginUser));
    }
}
