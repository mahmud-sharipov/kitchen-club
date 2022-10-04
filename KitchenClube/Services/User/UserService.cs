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
        return await _userManager.Users.Select(u => _mapper.Map<User, UserResponse>(u)).ToListAsync();
    }

    public async Task<UserResponse> GetAsync(Guid id)
    {
        var user = await _userManager.Users.Where(u => u.Id == id).FirstOrDefaultAsync();

        if (user == null)
            throw new NotFoundException("User", id);

        return _mapper.Map<User, UserResponse>(user);
    }

    public async Task<IEnumerable<string>> GetRolesAsync(Guid userId)
    {
        var user = await _userManager.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();

        if (user == null)
            throw new NotFoundException("User", userId);

        return await _userManager.GetRolesAsync(user);
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

        if (user is null)
            throw new NotFoundException(nameof(User), id);

        var userRoles = await _userManager.GetRolesAsync(user);

        var newRoles = updateUserRole.Roles.Except(userRoles);
        var oldRoles = userRoles.Except(updateUserRole.Roles);

        await _userManager.AddToRolesAsync(user, newRoles);
        await _userManager.RemoveFromRolesAsync(user, oldRoles);
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
            user.IsActive);
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
