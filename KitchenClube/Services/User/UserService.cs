namespace KitchenClube.Services;

public class UserService : ServiceBace<User>, IUserService
{
    private readonly IConfiguration _configuration;
    public UserService(KitchenClubContext context, IConfiguration configuration) : base(context, context.Users)
    {
        _configuration = configuration;
    }

    public async Task<IEnumerable<UserResponse>> GetAllAsync()
    {
        return await _context.Users.Select(u => Todto(u)).ToListAsync();
    }

    public async Task<UserResponse> GetAsync(Guid id)
    {
        return Todto(await FindOrThrowExceptionAsync(id));
    }

    public async Task UpdateAsync(Guid id, UpdateUser updateUser)
    {
        var user = await FindOrThrowExceptionAsync(id);

        user.FullName = updateUser.FullName;
        user.PhoneNumber = updateUser.PhoneNumber;
        user.IsActive = updateUser.IsActive;

        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
    public async Task<UserResponse> CreateAsync(CreateUser createUser)
    {
        var email = createUser.Email.ToLower();

        if (!Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
            throw new BadRequestException("Can not login because your email is wrong");

        if (_context.Users.Any(u => u.Email.ToLower() == email))
            throw new BadRequestException("User with this email is already regitered");

        var user = new User();
        user.FullName = createUser.FullName;
        user.Email = createUser.Email;
        user.IsActive = true;
        user.PhoneNumber = createUser.PhoneNumber;
        user.PasswordHash = CreatePasswordHash(createUser.Password);
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return Todto(user);
    }

    private string CreatePasswordHash(string password)
    {
        var salt = Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Salt").Value);
        using (var hmac = new HMACSHA512(salt))
        {
            return Encoding.UTF8.GetString(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await FindOrThrowExceptionAsync(id);

        if (_context.UserMenuItemSelections.Any(u => u.UserId == id))
            throw new BadRequestException("User can not be deleted because he/she has made menu selections");

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    private static UserResponse Todto(User user)
    {
        return new UserResponse(user.Id, user.FullName, user.PhoneNumber, user.Email, user.IsActive);
    }
}
