using Microsoft.AspNetCore.Mvc.Filters;

namespace KitchenClube.Filters;

public class DeleteActionFilter : BaseFilter, IAsyncActionFilter
{
    public DeleteActionFilter(KitchenClubContext context) : base(context){}

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var id = (Guid)context.ActionArguments["id"];
        if (_context.MenuItems.Any(mi => mi.FoodId == id))
            throw new BadRequestException("Food cannot be deleted because it is used on some menu items!");

        await next();
    }
}
