namespace KitchenClube.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly KitchenClubContext _context;

    public AuthController(IConfiguration configuration, KitchenClubContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    [HttpPost("login")]
    public ActionResult<LoginResponse> Login(LoginUser loginUser)
    {
        if (!Regex.IsMatch(loginUser.Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
            throw new BadRequestException("Can not login because your email is wrong");

        var user = _context.Users.FirstOrDefault(u => u.Email == loginUser.Email);
        if (user == null)
            throw new BadRequestException("Can not login because your email does not exsits in system");

        if (!VerifyPassword(loginUser.Password, user.PasswordHash))
            throw new BadRequestException("Can not login because your password is incorrect");

        var token = CreateToken(user);
        return Ok(new LoginResponse(token));
    }

    private bool VerifyPassword(string password, string passwordHash)
    {
        var salt = _configuration.GetSection("AppSettings:Salt").Value;
        using (var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(salt)))
        {
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Encoding.UTF8.GetString(computedHash).Equals(passwordHash);
        }
    }

    private string CreateToken(User user)
    {
        var key = Encoding.ASCII.GetBytes(_configuration["AppSettings:Token"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
            }),
            Expires = DateTime.UtcNow.AddHours(10),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        var token = tokenHandler.WriteToken(securityToken);
        return token;
    }
}
