namespace KitchenClube.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<User> _userManager;

    public AuthService(IConfiguration configuration, UserManager<User> userManager)
    {
        _configuration = configuration;
        _userManager = userManager;
    }

    public async Task<LoginResponse> Login(LoginUser loginUser)
    {
        var identityUser = await ValidateUser(loginUser);

        if (loginUser == null || identityUser == null)
        {
            throw new BadRequestException("Login failed! Please check details and try again.");
        }

        var token = await GenerateToken(identityUser);
        return new LoginResponse(token);
    }

    private async Task<User> ValidateUser(LoginUser loginUser)
    {
        var identityUser = await _userManager.FindByNameAsync(loginUser.Email);
        if (identityUser != null)
        {
            if (identityUser.IsActive == false)
                throw new BadRequestException("Can not login because user is not active");

            var result = _userManager.PasswordHasher.VerifyHashedPassword(identityUser, identityUser.PasswordHash, loginUser.Password);
            return result == PasswordVerificationResult.Failed ? throw new BadRequestException("Can not login because your password is incorrect") : identityUser;
        }
        return null;
    }

    private async Task<string> GenerateToken(User identityUser)
    {
        var key = Encoding.ASCII.GetBytes(_configuration["AppSettings:Token"]);

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Sid, identityUser.Id.ToString()),
        };
        foreach(var role in await _userManager.GetRolesAsync(identityUser))
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(10),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
