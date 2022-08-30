﻿namespace KitchenClube.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly KitchenClubContext _context;

    public AuthService(IConfiguration configuration, KitchenClubContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    public async Task<LoginResponse> Login(LoginUser loginUser)
    {
        var user = await _context.Users.Where(u => u.Email == loginUser.Email).FirstOrDefaultAsync();
        if (user == null)
            throw new BadRequestException("Can not login because your email does not exsits in system");

        if (user.IsActive == false)
            throw new BadRequestException("Can not login because user is not active");
        
        if (!VerifyPassword(loginUser.Password, user.PasswordHash))
            throw new BadRequestException("Can not login because your password is incorrect");

        var token = CreateToken(user);
        return new LoginResponse(token);
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
                new Claim(ClaimTypes.Role, user.Role.Name)
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