namespace KitchenClube.Services;
public class MenuService : ServiceBace<Menu>, IMenuService
{
    public MenuService(KitchenClubContext context, IMapper mapper) : base(context,context.Menu, mapper){}

    public async Task<IEnumerable<MenuResponse>> GetAllAsync()
    {
        return await _context.Menu.Select(u => _mapper.Map<Menu, MenuResponse>(u)).ToListAsync();
    }

    public async Task<MenuResponse> GetAsync(Guid id)
    {
        return _mapper.Map<Menu, MenuResponse>(await FindOrThrowExceptionAsync(id));
    }

    public async Task UpdateAsync(Guid id, UpdateMenu updateMenu)
    {
        var menu = await FindOrThrowExceptionAsync(id);

        if (_context.MenuItems.Any(m => m.MenuId == id))
            throw new BadRequestException("Can not change dates of menu because it is used in menuitems");

        if (menu.Status is MenuStatus.Closed)
            throw new BadRequestException("Сan not be changed because the menu is closed.");

        menu = _mapper.Map(updateMenu, menu);
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
        var menu = _mapper.Map<CreateMenu, Menu>(createMenu);
        menu.Status = MenuStatus.Draft;

        _context.Menu.Add(menu);

        await _context.SaveChangesAsync();
        return _mapper.Map<Menu, MenuResponse>(menu);
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
}
