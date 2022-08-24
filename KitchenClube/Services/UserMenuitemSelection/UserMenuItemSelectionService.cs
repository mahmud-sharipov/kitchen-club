namespace KitchenClube.Services;

public class UserMenuItemSelectionService : ServiceBace<UserMenuItemSelection>, IUserMenuItemSelectionService
{
    public UserMenuItemSelectionService(KitchenClubContext context):base(context,context.UserMenuItemSelections) {}

    public async Task<IEnumerable<UserMenuItemSelectionResponse>> GetAllAsync()
    {
        return await _context.UserMenuItemSelections
            .Select(u => Todto(u)).ToListAsync();
    }

    public async Task<UserMenuItemSelectionResponse> GetAsync(Guid id)
    {
        return Todto(await FindAsync(id));
    }

    public async Task<IEnumerable<UserMenuItemSelectionResponse>> UserMenuItemSelectionsByUserId(Guid userId)
    {
        return await _context.UserMenuItemSelections.Where(u => u.UserId == userId)
            .Select(u => new UserMenuItemSelectionResponse(u.Id, u.MenuitemId, u.UserId, u.Vote)).ToListAsync();
    }

    public async Task<IEnumerable<UserMenuItemSelectionResponse>> UserMenuItemSelectionsByMenuitemId(Guid menuitemId)
    {
        return await _context.UserMenuItemSelections.Where(u => u.MenuitemId == menuitemId)
            .Select(u => new UserMenuItemSelectionResponse(u.Id, u.MenuitemId, u.UserId, u.Vote)).ToListAsync();
    }

    public async Task UpdateAsync(Guid id, UpdateUserMenuItemSelection updateUserMenuItemSelection)
    {
        var userMenuItemSelection = await FindAsync(id);

        var menuItem = _context.MenuItems.Where(m => m.Id == userMenuItemSelection.MenuitemId).FirstOrDefault();
        if (menuItem.Day < DateTime.Now)
            throw new BadRequestException("Can not change past users' selections");

        var user = _context.Users.FirstOrDefault(u => u.Id == updateUserMenuItemSelection.UserId);
        if (user is null)
            throw new NotFoundException(nameof(User), updateUserMenuItemSelection.UserId);

        if (user.IsActive == false)
            throw new BadRequestException("User can not select menu because he/she is not active");

        var menuitem = _context.MenuItems.FirstOrDefault(m => m.Id == updateUserMenuItemSelection.MenuitemId);
        if (menuitem is null)
            throw new NotFoundException(nameof(MenuItem), updateUserMenuItemSelection.MenuitemId);

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
        var user = _context.Users.FirstOrDefault(u => u.Id == createUserMenuItemSelection.UserId);
        if (user == null)
            throw new NotFoundException(nameof(User), createUserMenuItemSelection.UserId);

        var menuItem = _context.MenuItems.FirstOrDefault(m => m.Id == createUserMenuItemSelection.MenuitemId);
        if (menuItem == null)
            throw new NotFoundException(nameof(MenuItem), createUserMenuItemSelection.MenuitemId);

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
        return Todto(userMenuItemSelection);
    }

    public async Task DeleteAsync(Guid id)
    {
        var userMenuItemSelection = await FindAsync(id);

        if (userMenuItemSelection.Menuitem.Day < DateTime.Now)
            throw new BadRequestException("Past user menu selections can not be deleted!");

        _context.UserMenuItemSelections.Remove(userMenuItemSelection);
        await _context.SaveChangesAsync();
    }

    private static UserMenuItemSelectionResponse Todto(UserMenuItemSelection u)
    {
        return new UserMenuItemSelectionResponse(u.Id, u.MenuitemId, u.UserId, u.Vote);
    }

}
