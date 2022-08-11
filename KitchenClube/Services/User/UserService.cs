namespace KitchenClube.Services;

public class UserService : IUserService
{
    private readonly KitchenClubContext _context;
    public UserService(KitchenClubContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<UserResponse>> GetAllAsync()
    {
        return await _context.Users.Select(u => Todto(u)).ToListAsync();
    }

    public async Task<UserResponse> GetAsync(Guid id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
            throw new NotFoundException(nameof(User), id);

        return Todto(user);
    }

    public async Task UpdateAsync(Guid id, UpdateUser updateUser)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == id);
        if (user is null)
            throw new NotFoundException(nameof(User), id);

        user.FullName = updateUser.FullName;
        user.PhoneNumber = updateUser.PhoneNumber;
        user.IsActive = updateUser.IsActive;

        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
    public async Task<UserResponse> CreateAsync(CreateUser createUser)
    {
        var email = createUser.Email.ToLower();
        if (_context.Users.Any(u => u.Email.ToLower() == email))
            throw new BadRequestException("User with this email is already regitered");

        var user = new User();
        user.FullName = createUser.FullName;
        user.Email = createUser.Email;
        user.IsActive = true;
        user.PhoneNumber = createUser.PhoneNumber;
        user.PasswordHash = createUser.Password;
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return Todto(user);

    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            throw new NotFoundException(nameof(user), id);

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
