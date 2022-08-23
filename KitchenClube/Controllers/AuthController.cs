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
    public ActionResult<string> Login(LoginUser loginUser)
    {
        if (!Regex.IsMatch(loginUser.Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
            throw new BadRequestException("Can not login because your email is wrong");

        var user = _context.Users.FirstOrDefault(u => u.Email == loginUser.Email);
        if (user == null)
            throw new BadRequestException("Can not login because your email does not exsits in system");

        if (!VerifyPassword(loginUser.Password, user.PasswordHash))
            throw new BadRequestException("Can not login because your password is incorrect");

        var token = CreateToken(user);
        return Ok(token);
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
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(_configuration.GetSection("AppSettings:Token").Value));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken
            (
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: creds
            );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}
