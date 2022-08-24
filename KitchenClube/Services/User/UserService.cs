namespace KitchenClube.Services;

public class UserService : ServiceBace<User>, IUserService
{
    public UserService(KitchenClubContext context):base(context,context.Users) {}
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
        if (_context.Users.Any(u => u.Email.ToLower() == email))
            throw new BadRequestException("User with this email is already regitered");

        var user = new User
        {
            FullName = createUser.FullName,
            Email = createUser.Email,
            IsActive = true,
            PhoneNumber = createUser.PhoneNumber,
            PasswordHash = createUser.Password
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return Todto(user);

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
