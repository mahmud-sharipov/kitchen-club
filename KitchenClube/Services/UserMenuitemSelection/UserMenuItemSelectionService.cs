namespace KitchenClube.Services;

public class UserMenuitemSelectionService : ServiceBace<UserMenuitemSelection>, IUserMenuitemSelectionService
{
    private readonly UserManager<User> _userManager;
    public UserMenuitemSelectionService(KitchenClubContext context, IMapper mapper, UserManager<User> userManager)
        : base(context, context.UserMenuItemSelections, mapper)
    {
        _userManager = userManager;
    }

    public async Task<IEnumerable<UserMenuitemSelectionResponse>> GetAllAsync()
    {
        return await _context.UserMenuItemSelections
            .Select(u => _mapper.Map<UserMenuitemSelection, UserMenuitemSelectionResponse>(u)).ToListAsync();
    }

    public async Task<UserMenuitemSelectionResponse> GetAsync(Guid id)
    {
        return _mapper.Map<UserMenuitemSelection, UserMenuitemSelectionResponse>(await FindOrThrowExceptionAsync(id));
    }

    public async Task<IEnumerable<UserMenuitemSelectionResponse>> UserMenuitemSelectionsByUserId(Guid userId)
    {
        var user = await _context.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();

        if (user is null)
            throw new NotFoundException(nameof(User), userId);

        var userMenuitemSelections =
            await _context.UserMenuItemSelections.Where(u => u.UserId == userId).ToListAsync();

        if (userMenuitemSelections.Count() == 0)
            throw new BadRequestException("This user does not have any menuitem selections");

        return userMenuitemSelections.Select(u => _mapper.Map<UserMenuitemSelection, UserMenuitemSelectionResponse>(u));
    }

    public async Task<IEnumerable<UserMenuitemSelectionResponse>> UserMenuitemSelectionsByMenuitemId(Guid menuitemId)
    {
        var menuitem = await _context.MenuItems.Where(u => u.Id == menuitemId).FirstOrDefaultAsync();

        if (menuitem is null)
            throw new NotFoundException(nameof(Menuitem), menuitemId);

        var userMenuitemSelections =
            await _context.UserMenuItemSelections.Where(u => u.MenuitemId == menuitemId).ToListAsync();

        if (userMenuitemSelections.Count() == 0)
            throw new BadRequestException("There are no any user menuitem selections with such menuitem");

        return userMenuitemSelections.Select(u => _mapper.Map<UserMenuitemSelection, UserMenuitemSelectionResponse>(u));
    }

    public async Task UpdateAsync(Guid id, UpdateUserMenuitemSelection updateUserMenuItemSelection)
    {
        var userMenuItemSelection = await FindOrThrowExceptionAsync(id);

        var menuitem = await _context.MenuItems.FindOrThrowExceptionAsync(updateUserMenuItemSelection.MenuitemId);

        if (menuitem.Day < DateTime.Now)
            throw new BadRequestException("Can not change past users' selections");

        if (menuitem.IsActive == false)
            throw new BadRequestException("Can not change because menuitem is not active");

        var user = await _userManager.Users.Where(u => u.Id == updateUserMenuItemSelection.UserId).FirstOrDefaultAsync();

        if(user is null)
            throw new NotFoundException(nameof(User), updateUserMenuItemSelection.UserId);

        if (user.IsActive == false)
            throw new BadRequestException("User can not select menu because he/she is not active");

        userMenuItemSelection.User = user;
        userMenuItemSelection.Menuitem = menuitem;
        userMenuItemSelection.Vote = updateUserMenuItemSelection.Vote;

        _context.UserMenuItemSelections.Update(userMenuItemSelection);
        await _context.SaveChangesAsync();
    }

    public async Task<UserMenuitemSelectionResponse> CreateAsync(CreateUserMenuitemSelection createUserMenuItemSelection)
    {
        var idUser = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid).Value;
        var user = await _userManager.Users.Where(u => u.Id == Guid.Parse(idUser)).FirstOrDefaultAsync();

        var menuItem = await _context.MenuItems.FindOrThrowExceptionAsync(createUserMenuItemSelection.MenuitemId);

        if (menuItem.Day < DateTime.Now)
            throw new BadRequestException("Can not add user selection because menuitem's day in past");

        var userMenuItemSelection = new UserMenuitemSelection
        {
            User = user,
            Menuitem = menuItem,
            Vote = createUserMenuItemSelection.Vote
        };

        _context.UserMenuItemSelections.Add(userMenuItemSelection);
        await _context.SaveChangesAsync();
        return _mapper.Map<UserMenuitemSelection, UserMenuitemSelectionResponse>(userMenuItemSelection);
    }

    public async Task DeleteAsync(Guid id)
    {
        var userMenuItemSelection = await FindOrThrowExceptionAsync(id);

        if (userMenuItemSelection.Menuitem.Day < DateTime.Now)
            throw new BadRequestException("Past user menu selections can not be deleted!");

        _context.UserMenuItemSelections.Remove(userMenuItemSelection);
        await _context.SaveChangesAsync();
    }
}
