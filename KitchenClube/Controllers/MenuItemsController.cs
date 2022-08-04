﻿using KitchenClube.Data;
using KitchenClube.Exceptions;
using KitchenClube.Requests.MenuItem;
using KitchenClube.Responses;

namespace KitchenClube.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MenuItemsController : ControllerBase
{
    private readonly KitchenClubContext _context;

    public MenuItemsController(KitchenClubContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MenuItemResponse>>> GetMenuItems()
    {
        return await _context.MenuItems
            .Select(m => Todto(m)).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MenuItemResponse>> GetMenuItem(Guid id)
    {
        var menuItem = await _context.MenuItems.FindAsync(id);

        if (menuItem == null)
            throw new NotFoundException(nameof(MenuItem),id);

        return
            Todto(menuItem);
    }

    private static MenuItemResponse Todto(MenuItem menuItem)
    {
        return new MenuItemResponse(menuItem.Id, menuItem.Day, menuItem.FoodId, menuItem.MenuId, menuItem.IsActive);
    }

    [HttpGet("menu/{menuId}")]
    public async Task<ActionResult<IEnumerable<MenuItemResponse>>> GetMenuItemsByMenuId(Guid menuId)
    {
        return await _context.MenuItems.Where(mi => mi.MenuId == menuId)
            .Select(m => new MenuItemResponse(m.Id, m.Day, m.FoodId, m.MenuId, m.IsActive)).ToListAsync();
    }

    [HttpGet("food/{foodId}")]
    public async Task<ActionResult<IEnumerable<MenuItemResponse>>> GetMenuItemsByFoodId(Guid foodId)
    {
        return await _context.MenuItems.Where(mi => mi.FoodId == foodId)
            .Select(m => new MenuItemResponse(m.Id, m.Day, m.FoodId, m.MenuId, m.IsActive)).ToListAsync();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutMenuItem(Guid id, UpdateMenuItemRequest menuItemRequest)
    {
        var menuItem = _context.MenuItems.FirstOrDefault(x => x.Id == id);
        if (menuItem is null)
            throw new NotFoundException(nameof(MenuItem),id);

        var food = _context.Foods.FirstOrDefault(x => x.Id == menuItemRequest.FoodId);
        if (food is null)
            throw new NotFoundException(nameof(Food), menuItemRequest.FoodId);

        var menu = _context.Menu.FirstOrDefault(x => x.Id == menuItemRequest.MenuId);
        if (menu is null)
            throw new NotFoundException(nameof(Menu), menuItemRequest.MenuId);

        if (menuItemRequest.Day > menu.EndDate || menuItemRequest.Day < menu.StartDate)
            throw new BadRequestException("Date is out of menu period!");

        if (menuItem.Day < menuItemRequest.Day)
            throw new BadRequestException("Can not change the date of the past menu.");

        menuItem.Food = food;
        menuItem.Menu = menu;
        menuItem.Day = menuItemRequest.Day;
        menuItem.IsActive = menuItemRequest.IsActive;
        _context.MenuItems.Update(menuItem);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<MenuItem>> PostMenuItem(CreateMenuItemRequest menuItemRequest)
    {
        var menu = _context.Menu.FirstOrDefault(x => x.Id == menuItemRequest.MenuId);
        if (menu is null)
            throw new NotFoundException(nameof(Menu), menuItemRequest.MenuId);

        if (menu.Status is MenuStatus.Closed)
            throw new BadRequestException("Can not create menuitem because menu is closed.");

        var food = _context.Foods.FirstOrDefault(x => x.Id == menuItemRequest.FoodId);
        if (food is null)
            throw new NotFoundException(nameof(Food),menuItemRequest.FoodId);

        if (menuItemRequest.Day > menu.EndDate || menuItemRequest.Day < menu.StartDate)
            throw new BadRequestException("Date is out of menu period!");

        var menuItem = new MenuItem() {
            IsActive = true,
            Food = food,
            Menu = menu,
            Day = menuItemRequest.Day,
        };
        _context.MenuItems.Add(menuItem);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetMenuItem", new { id = menuItem.Id }, menuItem);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMenuItem(Guid id)
    {
        var menuItem = await _context.MenuItems.FindAsync(id);
        if (menuItem == null) {
            throw new NotFoundException(nameof(MenuItem),id);
        }
        if (menuItem.Day < DateTime.Now)
            throw new BadRequestException("Can not delete, because menuitem's day in past");

        _context.MenuItems.Remove(menuItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
