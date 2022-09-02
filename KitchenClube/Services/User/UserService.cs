namespace KitchenClube.Services;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly KitchenClubContext _context;
    protected readonly IHttpContextAccessor _contextAccessor;

    public UserService(IMapper mapper, UserManager<User> userManager, KitchenClubContext context, IHttpContextAccessor contextAccessor)
    {
        _mapper = mapper;
        _userManager = userManager;
        _context = context;
        _contextAccessor = contextAccessor;
    }

    public async Task<IEnumerable<UserResponse>> GetAllAsync()
    {
        var userResponse = await _userManager.Users.Select(u => new UserResponse(u.Id,
        u.FullName, u.PhoneNumber, u.Email, //string.Join(",", _userManager.GetRolesAsync(u)),
        "", u.IsActive)).ToListAsync();

        return userResponse;
    }

    public async Task<UserResponse> GetAsync(Guid id)
    {
        var user = await _userManager.Users.Where(u => u.Id == id).FirstOrDefaultAsync();

        if (user == null)
            throw new NotFoundException("User", id);

        var userResponse = new UserResponse(
                user.Id,
                user.FullName,
                user.PhoneNumber,
                user.Email,
                string.Join(",", _userManager.GetRolesAsync(user).Result.ToArray()),
                user.IsActive);
        return userResponse;
    }

    public async Task UpdateAsync(Guid id, UpdateUser updateUser)
    {
        var user = await _userManager.Users.Where(u => u.Id == id).FirstOrDefaultAsync();

        if (user == null)
            throw new NotFoundException("User", id);

        _mapper.Map(updateUser, user);

        await _userManager.UpdateAsync(user);
    }

    public async Task UpdateAsync(Guid id, UpdateUserRole updateUserRole)
    {
        var user = await _userManager.Users.Where(u => u.Id == id).FirstOrDefaultAsync();

        var userRoles = await _userManager.GetRolesAsync(user);

        var addedRoles = updateUserRole.Roles.Except(userRoles);
        var removedRoles = userRoles.Except(updateUserRole.Roles);

        await _userManager.AddToRolesAsync(user, addedRoles);
        await _userManager.RemoveFromRolesAsync(user, removedRoles);
    }

    public async Task<UserResponse> CreateAsync(CreateUser createUser)
    {
        var email = createUser.Email.ToLower();

        if (_userManager.Users.Any(u => u.Email == createUser.Email))
            throw new BadRequestException("User with this email is already regitered");

        var user = _mapper.Map<CreateUser, User>(createUser);
        user.IsActive = true;

        var result = await _userManager.CreateAsync(user, createUser.Password);

        await _userManager.AddToRolesAsync(user, createUser.Roles);

        return new UserResponse(user.Id, user.FullName, user.PhoneNumber, user.Email,
            string.Join(",", _userManager.GetRolesAsync(user).Result.ToArray()), user.IsActive);
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await _userManager.Users.Where(u => u.Id == id).FirstOrDefaultAsync();

        if (user == null)
            throw new NotFoundException("User",id);

        if (_context.UserMenuItemSelections.Any(u => u.UserId == id))
            throw new BadRequestException("User can not be deleted because he/she has made menu selections");

        await _userManager.DeleteAsync(user);
    }

    public async Task ResetPasswordAsync(ResetPasswordUser reset)
    {
        var user = _userManager.FindByIdAsync(_contextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid).Value).Result;
        user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, reset.NewPassword);
        await _userManager.UpdateAsync(user);
    }
}
