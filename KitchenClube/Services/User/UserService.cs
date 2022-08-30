namespace KitchenClube.Services;

public class UserService : ServiceBace<User>, IUserService
{
    private readonly IConfiguration _configuration;

    public UserService(KitchenClubContext context, IConfiguration configuration, IMapper mapper) : base(context, context.Users, mapper)
    {
        _configuration = configuration;
    }

    public async Task<IEnumerable<UserResponse>> GetAllAsync()
    {
        return await _context.Users.Select(u => _mapper.Map<User,UserResponse>(u)).ToListAsync();
    }

    public async Task<UserResponse> GetAsync(Guid id)
    {
        return _mapper.Map<User, UserResponse>(await FindOrThrowExceptionAsync(id));
    }

    public async Task<IEnumerable<UserResponse>> GetByRoleAsync(Guid id)
    {
        return await _context.Users.Where(u=>u.RoleId == id).Select(u=> _mapper.Map<User, UserResponse>(u)).ToListAsync();
    }

    public async Task UpdateAsync(Guid id, UpdateUser updateUser)
    {
        var user = await FindOrThrowExceptionAsync(id);
        user = _mapper.Map(updateUser, user);
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task<UserResponse> CreateAsync(CreateUser createUser)
    {
        var email = createUser.Email.ToLower();

        if (_context.Users.Any(u => u.Email.ToLower() == email))
            throw new BadRequestException("User with this email is already regitered");

        var user = _mapper.Map<CreateUser, User>(createUser);
        user.PasswordHash = CreatePasswordHash(createUser.Password);
        user.IsActive = true;
        user.RoleId = _context.Roles.Where(r => r.Name == "User").Select(r => r.Id).FirstOrDefault();

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return _mapper.Map<User,UserResponse>(user);
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
}
