namespace KitchenClube.Services;
public class MenuService : ServiceBace<Menu>, IMenuService
{
    public MenuService(KitchenClubContext context) : base(context,context.Menu){}

    public async Task<IEnumerable<MenuResponse>> GetAllAsync()
    {
        return await _context.Menu.Select(u => ToDto(u)).ToListAsync();
    }

    public async Task<MenuResponse> GetAsync(Guid id)
    {
        return ToDto(await FindOrThrowExceptionAsync(id));
    }

    public async Task UpdateAsync(Guid id, UpdateMenu updateMenu)
    {
        var menu = await FindOrThrowExceptionAsync(id);

        if (updateMenu.StartDate > updateMenu.EndDate)
            throw new BadRequestException("End date must be greater than Start date.");

        if (updateMenu.StartDate == updateMenu.EndDate)
            throw new BadRequestException("Start date and End date can not be equal.");

        if (menu.Status is MenuStatus.Closed)
            throw new BadRequestException("Сan not be changed because the menu is closed.");

        if (_context.MenuItems.Any(m => m.MenuId == id))
            throw new BadRequestException("Can not change dates of menu because it is used in menuitems");

        menu.StartDate = updateMenu.StartDate;
        menu.EndDate = updateMenu.EndDate;

        _context.Update(menu);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateStatusCloseAsync(Guid id)
    {
        var menu = await FindOrThrowExceptionAsync(id);

        menu.Status = MenuStatus.Closed;
        _context.Update(menu);
        await _context.SaveChangesAsync();        
    }

    public async Task UpdateStatusOpenAsync(Guid id)
    {
        var menu = await FindOrThrowExceptionAsync(id);

        menu.Status = MenuStatus.Active;
        _context.Update(menu);
        await _context.SaveChangesAsync();
    }

    public async Task<MenuResponse> CreateAsync(CreateMenu createMenu)
    {
        if (createMenu.StartDate == createMenu.EndDate)
            throw new BadRequestException("Start date and End date can not be equal.");

        if (createMenu.EndDate < createMenu.StartDate)
            throw new BadRequestException("End date must be greater than Start date.");

        var menu = new Menu
        {
            StartDate = createMenu.StartDate,
            EndDate = createMenu.EndDate,
            Status = MenuStatus.Draft
        };
        _context.Menu.Add(menu);

        await _context.SaveChangesAsync();
        return ToDto(menu);
    }

    public async Task DeleteAsync(Guid id)
    {
        var menu = await FindOrThrowExceptionAsync(id);

        if (menu.Status is MenuStatus.Closed)
            throw new BadRequestException("Сan not delete because menu is closed.");

        if (_context.MenuItems.Any(m => m.MenuId == id))
            throw new BadRequestException("Can not delete because menu is used in menuitem");

        _context.Menu.Remove(menu);
        await _context.SaveChangesAsync();
    }

    private static MenuResponse ToDto(Menu menu)
    {
        return new MenuResponse(menu.Id, menu.StartDate, menu.EndDate, menu.Status);
    }
}
