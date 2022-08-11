namespace KitchenClube.Services;

public class MenuItemService : IMenuItemService
{
    private readonly KitchenClubContext _context;
    public MenuItemService(KitchenClubContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MenuItemResponse>> GetAllAsync()
    {
        return await _context.MenuItems
            .Select(m => Todto(m)).ToListAsync();
    }

    public async Task<MenuItemResponse> GetAsync(Guid id)
    {
        var menuItem = await _context.MenuItems.FindAsync(id);

        if (menuItem == null)
            throw new NotFoundException(nameof(MenuItem), id);

        return
            Todto(menuItem);
    }
    public async Task<IEnumerable<MenuItemResponse>> MenuItemsByMenuId(Guid menuId)
    {
        return await _context.MenuItems.Where(mi => mi.MenuId == menuId)
            .Select(m => new MenuItemResponse(m.Id, m.Day, m.FoodId, m.MenuId, m.IsActive)).ToListAsync();
    }

    public async Task<IEnumerable<MenuItemResponse>> MenuItemsByFoodId(Guid foodId)
    {
        return await _context.MenuItems.Where(mi => mi.FoodId == foodId)
            .Select(m => new MenuItemResponse(m.Id, m.Day, m.FoodId, m.MenuId, m.IsActive)).ToListAsync();
    }

    public async Task UpdateAsync(Guid id, UpdateMenuItem updateMenuItem)
    {
        var menuItem = _context.MenuItems.FirstOrDefault(x => x.Id == id);
        if (menuItem is null)
            throw new NotFoundException(nameof(MenuItem), id);

        var food = _context.Foods.FirstOrDefault(x => x.Id == updateMenuItem.FoodId);
        if (food is null)
            throw new NotFoundException(nameof(Food), updateMenuItem.FoodId);

        var menu = _context.Menu.FirstOrDefault(x => x.Id == updateMenuItem.MenuId);
        if (menu is null)
            throw new NotFoundException(nameof(Menu), updateMenuItem.MenuId);

        if (updateMenuItem.Day > menu.EndDate || updateMenuItem.Day < menu.StartDate)
            throw new BadRequestException("Date is out of menu period!");

        if (menuItem.Day < updateMenuItem.Day)
            throw new BadRequestException("Can not change the date of the past menu.");

        menuItem.Food = food;
        menuItem.Menu = menu;
        menuItem.Day = updateMenuItem.Day;
        menuItem.IsActive = updateMenuItem.IsActive;
        _context.MenuItems.Update(menuItem);
        await _context.SaveChangesAsync();
    }

    public async Task<MenuItemResponse> CreateAsync(CreateMenuItem createMenuItem)
    {
        var menu = _context.Menu.FirstOrDefault(x => x.Id == createMenuItem.MenuId);
        if (menu is null)
            throw new NotFoundException(nameof(Menu), createMenuItem.MenuId);

        if (menu.Status is MenuStatus.Closed)
            throw new BadRequestException("Can not create menuitem because menu is closed.");

        var food = _context.Foods.FirstOrDefault(x => x.Id == createMenuItem.FoodId);
        if (food is null)
            throw new NotFoundException(nameof(Food), createMenuItem.FoodId);

        if (createMenuItem.Day > menu.EndDate || createMenuItem.Day < menu.StartDate)
            throw new BadRequestException("Date is out of menu period!");

        var menuItem = new MenuItem()
        {
            IsActive = true,
            Food = food,
            Menu = menu,
            Day = createMenuItem.Day,
        };
        _context.MenuItems.Add(menuItem);
        await _context.SaveChangesAsync();
        return Todto(menuItem);
    }

    public async Task DeleteAsync(Guid id)
    {
        var menuItem = await _context.MenuItems.FindAsync(id);
        if (menuItem == null)
        {
            throw new NotFoundException(nameof(MenuItem), id);
        }
        if (menuItem.Day < DateTime.Now)
            throw new BadRequestException("Can not delete, because menuitem's day in past");

        _context.MenuItems.Remove(menuItem);
        await _context.SaveChangesAsync();
    }

    private static MenuItemResponse Todto(MenuItem menuItem)
    {
        return new MenuItemResponse(menuItem.Id, menuItem.Day, menuItem.FoodId, menuItem.MenuId, menuItem.IsActive);
    }

}
