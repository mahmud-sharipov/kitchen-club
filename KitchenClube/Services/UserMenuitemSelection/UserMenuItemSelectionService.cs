namespace KitchenClube.Services;

public class UserMenuItemSelectionService : ServiceBace<UserMenuItemSelection>, IUserMenuItemSelectionService
{
    private readonly UserManager<User> _userManager;
    public UserMenuItemSelectionService(KitchenClubContext context, IMapper mapper, UserManager<User> userManager)
        : base(context, context.UserMenuItemSelections, mapper) 
    {
        _userManager = userManager;
    }

    public async Task<IEnumerable<UserMenuItemSelectionResponse>> GetAllAsync()
    {
        return await _context.UserMenuItemSelections
            .Select(u => _mapper.Map<UserMenuItemSelection, UserMenuItemSelectionResponse>(u)).ToListAsync();
    }

    public async Task<UserMenuItemSelectionResponse> GetAsync(Guid id)
    {
        return _mapper.Map<UserMenuItemSelection, UserMenuItemSelectionResponse>(await FindOrThrowExceptionAsync(id));
    }

    public async Task<IEnumerable<UserMenuItemSelectionResponse>> UserMenuItemSelectionsByUserId(Guid userId)
    {
        return await _context.UserMenuItemSelections.Where(u => u.UserId == userId)
            .Select(u => _mapper.Map<UserMenuItemSelection,UserMenuItemSelectionResponse>(u)).ToListAsync();
    }

    public async Task<IEnumerable<UserMenuItemSelectionResponse>> UserMenuItemSelectionsByMenuitemId(Guid menuitemId)
    {
        return await _context.UserMenuItemSelections.Where(u => u.MenuitemId == menuitemId)
            .Select(u => _mapper.Map<UserMenuItemSelection, UserMenuItemSelectionResponse>(u)).ToListAsync();
    }

    public async Task UpdateAsync(Guid id, UpdateUserMenuItemSelection updateUserMenuItemSelection)
    {
        var userMenuItemSelection = await FindOrThrowExceptionAsync(id);

        var menuItem = _context.MenuItems.FirstOrDefault(m => m.Id == userMenuItemSelection.MenuitemId);
        if (menuItem.Day < DateTime.Now)
            throw new BadRequestException("Can not change past users' selections");

        var user = await _userManager.Users.Where(u => u.Id == updateUserMenuItemSelection.UserId).FirstOrDefaultAsync();

        if (user is null)
            throw new NotFoundException("User", updateUserMenuItemSelection.UserId);

        if (user.IsActive == false)
            throw new BadRequestException("User can not select menu because he/she is not active");

        var menuitem = await _context.MenuItems.FindOrThrowExceptionAsync(updateUserMenuItemSelection.MenuitemId);
        if (menuitem.IsActive == false)
            throw new BadRequestException("Can not change because menuitem is not active");

        userMenuItemSelection.User = user;
        userMenuItemSelection.Menuitem = menuitem;
        userMenuItemSelection.Vote = updateUserMenuItemSelection.Vote;

        _context.UserMenuItemSelections.Update(userMenuItemSelection);
        await _context.SaveChangesAsync();
    }

    public async Task<UserMenuItemSelectionResponse> CreateAsync(CreateUserMenuItemSelection createUserMenuItemSelection)
    {
        var idUser = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid).Value;
        var user = await _userManager.Users.Where(u => u.Id == Guid.Parse(idUser)).FirstOrDefaultAsync();

        if (user is null)
            throw new NotFoundException("User", idUser);
        
        var menuItem = await _context.MenuItems.FindOrThrowExceptionAsync(createUserMenuItemSelection.MenuitemId);

        if (menuItem.Day < DateTime.Now)
            throw new BadRequestException("Can not add user selection because menuitem's day in past");

        var userMenuItemSelection = new UserMenuItemSelection
        {
            User = user,
            Menuitem = menuItem,
            Vote = createUserMenuItemSelection.Vote
        };

        _context.UserMenuItemSelections.Add(userMenuItemSelection);
        await _context.SaveChangesAsync();
        return _mapper.Map<UserMenuItemSelection, UserMenuItemSelectionResponse>(userMenuItemSelection);
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
