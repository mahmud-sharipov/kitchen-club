namespace KitchenClube.Extentions;

public static class ServicesExtention
{
    public static IServiceCollection AddingServices(this IServiceCollection services)
    {
        services.AddScoped<IFoodService, FoodService>();
        services.AddScoped<IMenuService, MenuService>();
        services.AddScoped<IMenuItemService, MenuItemService>();
        services.AddScoped<IUserMenuItemSelectionService, UserMenuItemSelectionService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IRoleService, RoleService>();

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        return services;
    }
}
