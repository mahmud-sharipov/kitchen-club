namespace KitchenClube.Filters;

public class GetAllActionFilter : Attribute, IAsyncActionFilter
{
    private readonly UserManager<User> _userManager;

    public GetAllActionFilter(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var resultContext = await next();

        var result = (resultContext.Result as ObjectResult).Value as List<UserResponse>;
        var newResponse = new List<UserResponse>();

        foreach(var userResponse in result)
        {
            var user = await _userManager.FindByIdAsync(userResponse.Id.ToString());
            var newUserResponse = new UserResponse(
                user.Id,
                user.FullName,
                user.PhoneNumber,
                user.Email,
                string.Join(",", _userManager.GetRolesAsync(user).Result.ToArray()),
                user.IsActive);
            newResponse.Add(newUserResponse);
        }

        resultContext.Result = new OkObjectResult(newResponse);
    }
}
