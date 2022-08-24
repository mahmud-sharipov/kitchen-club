namespace KitchenClube.Services;

public class UserMenuItemSelectionService : ServiceBace<UserMenuItemSelection>, IUserMenuItemSelectionService
{
    public UserMenuItemSelectionService(KitchenClubContext context) : base(context, context.UserMenuItemSelections) { }

    public async Task<IEnumerable<UserMenuItemSelectionResponse>> GetAllAsync()
    {
        return await _context.UserMenuItemSelections
            .Select(u => ToDto(u)).ToListAsync();
    }

    public async Task<UserMenuItemSelectionResponse> GetAsync(Guid id)
    {
        return ToDto(await FindOrThrowExceptionAsync(id));
    }

    public async Task<IEnumerable<UserMenuItemSelectionResponse>> UserMenuItemSelectionsByUserId(Guid userId)
    {
        return await _context.UserMenuItemSelections.Where(u => u.UserId == userId)
            .Select(u => ToDto(u)).ToListAsync();
    }

    public async Task<IEnumerable<UserMenuItemSelectionResponse>> UserMenuItemSelectionsByMenuitemId(Guid menuitemId)
    {
        return await _context.UserMenuItemSelections.Where(u => u.MenuitemId == menuitemId)
            .Select(u => ToDto(u)).ToListAsync();
    }

    public async Task UpdateAsync(Guid id, UpdateUserMenuItemSelection updateUserMenuItemSelection)
    {
        var userMenuItemSelection = await FindOrThrowExceptionAsync(id);

        var menuItem = _context.MenuItems.Where(m => m.Id == userMenuItemSelection.MenuitemId).FirstOrDefault();
        if (menuItem.Day < DateTime.Now)
            throw new BadRequestException("Can not change past users' selections");

        var user = await _context.Users.FindOrThrowExceptionAsync(updateUserMenuItemSelection.UserId);

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
        var user = await _context.Users.FindOrThrowExceptionAsync(createUserMenuItemSelection.UserId);
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
        return ToDto(userMenuItemSelection);
    }

    public async Task DeleteAsync(Guid id)
    {
        var userMenuItemSelection = await FindOrThrowExceptionAsync(id);

        if (userMenuItemSelection.Menuitem.Day < DateTime.Now)
            throw new BadRequestException("Past user menu selections can not be deleted!");

        _context.UserMenuItemSelections.Remove(userMenuItemSelection);
        await _context.SaveChangesAsync();
    }

    private static UserMenuItemSelectionResponse ToDto(UserMenuItemSelection u)
    {
        return new UserMenuItemSelectionResponse(u.Id, u.MenuitemId, u.UserId, u.Vote);
    }

}
