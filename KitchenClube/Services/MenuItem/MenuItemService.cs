namespace KitchenClube.Services;

public class MenuitemService : ServiceBace<Menuitem>, IMenuitemService
{
    public MenuitemService(KitchenClubContext context, IMapper mapper) : base(context, context.MenuItems, mapper) { }

    public async Task<IEnumerable<MenuitemResponse>> GetAllAsync()
    {
        return await _context.MenuItems.Select(m => _mapper.Map<Menuitem, MenuitemResponse>(m)).ToListAsync();
    }

    public async Task<MenuitemResponse> GetAsync(Guid id)
    {
        return _mapper.Map<Menuitem, MenuitemResponse>(await FindOrThrowExceptionAsync(id));
    }

    public async Task<IEnumerable<MenuitemResponse>> GetMenuitemsByMenuId(Guid menuId)
    {
        if (!_context.MenuItems.Any(mi => mi.MenuId == menuId))
            throw new NotFoundException(nameof(Menu), menuId);
        
        return await _context.MenuItems.Where(mi => mi.MenuId == menuId && mi.Menu.Status == MenuStatus.Active)
            .Select(m => _mapper.Map<Menuitem, MenuitemResponse>(m)).ToListAsync();
    }

    public async Task<IEnumerable<MenuitemResponse>> GetMenuitemsByFoodId(Guid foodId)
    {
        if(!_context.MenuItems.Any(mi=>mi.FoodId == foodId))
            throw new NotFoundException(nameof(Food), foodId);

        return await _context.MenuItems.Where(mi => mi.FoodId == foodId && mi.Menu.Status == MenuStatus.Active)
            .Select(m => _mapper.Map<Menuitem, MenuitemResponse>(m)).ToListAsync();
    }

    public async Task UpdateAsync(Guid id, UpdateMenuitem updateMenuItem)
    {
        var menuItem = await FindOrThrowExceptionAsync(id);

        var food = await _context.Foods.FindOrThrowExceptionAsync(updateMenuItem.FoodId);
        var menu = await _context.Menu.FindOrThrowExceptionAsync(updateMenuItem.MenuId);

        if (updateMenuItem.Day > menu.EndDate || updateMenuItem.Day < menu.StartDate)
            throw new BadRequestException("Date is out of menu period!");

        if (menuItem.Day < updateMenuItem.Day)
            throw new BadRequestException("Can not change the date of the past menu.");

        if (_context.UserMenuItemSelections.Any(u => u.MenuitemId == id))
            throw new BadRequestException("Can not update, because some users selected this menuitem");

        menuItem.Day = updateMenuItem.Day;
        menuItem.IsActive = updateMenuItem.IsActive;
        menuItem.Food = food;
        menuItem.Menu = menu;

        _context.MenuItems.Update(menuItem);
        await _context.SaveChangesAsync();
    }

    public async Task<MenuitemResponse> CreateAsync(CreateMenuitem createMenuItem)
    {
        var menu = await _context.Menu.FindOrThrowExceptionAsync(createMenuItem.MenuId);

        if (menu.Status is MenuStatus.Closed)
            throw new BadRequestException("Can not create menuitem because menu is closed.");

        var food = await _context.Foods.FindOrThrowExceptionAsync(createMenuItem.FoodId);

        if (createMenuItem.Day > menu.EndDate || createMenuItem.Day < menu.StartDate)
            throw new BadRequestException("Date is out of menu period!");

        var menuItem = new Menuitem()
        {
            IsActive = true,
            Food = food,
            Menu = menu,
            Day = createMenuItem.Day,
        };
        _context.MenuItems.Add(menuItem);
        await _context.SaveChangesAsync();
        return _mapper.Map<Menuitem, MenuitemResponse>(menuItem);
    }

    public async Task DeleteAsync(Guid id)
    {
        var menuItem = await FindOrThrowExceptionAsync(id);

        if (menuItem.Day < DateTime.Now)
            throw new BadRequestException("Can not delete, because menuitem's day in past");

        if (_context.UserMenuItemSelections.Any(u => u.MenuitemId == id))
            throw new BadRequestException("Can not delete, because some users selected this menuitem");

        _context.MenuItems.Remove(menuItem);
        await _context.SaveChangesAsync();
    }
}
