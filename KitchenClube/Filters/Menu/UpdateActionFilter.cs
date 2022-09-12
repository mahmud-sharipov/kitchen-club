using Microsoft.AspNetCore.Mvc.Filters;

namespace KitchenClube.Filters;

public class UpdateActionFilter : BaseFilter, IAsyncActionFilter
{
    public UpdateActionFilter(KitchenClubContext context) : base(context){}

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var id = (Guid)context.ActionArguments["id"];
        if (_context.MenuItems.Any(m => m.MenuId == id))
            throw new BadRequestException("Can not change dates of menu because it is used in menuitems");

        await next();
    }
}
