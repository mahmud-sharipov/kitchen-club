namespace KitchenClube.Controllers.V2;

[Route("api/{v:apiVersion}/auth/")]

//[Route("api/[controller]")]

[ApiController]
[ApiVersion("2.0")]
public class AuthController : ControllerBase
{
    private readonly Services.V2.IAuthService _authService;
    public AuthController(Services.V2.IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(LoginUser loginUser)
    {
        return Ok(await _authService.Login(loginUser));
    }
}
