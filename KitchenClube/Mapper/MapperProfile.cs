namespace KitchenClube;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<UpdateUser, User>();
        CreateMap<CreateUser, User>();
        CreateMap<User,UserResponse>();

        CreateMap<UpdateFood, Food>();
        CreateMap<CreateFood, Food>();
        CreateMap<Food, FoodResponse>();

        CreateMap<UpdateMenu, Menu>();
        CreateMap<CreateMenu, Menu>();
        CreateMap<Menu, MenuResponse>();

        CreateMap<MenuItem, MenuItemResponse>();

        CreateMap<UpdateRole, Role>();
        CreateMap<CreateRole, Role>();
        CreateMap<Role, RoleResponse>();

        CreateMap<UserMenuItemSelection, UserMenuItemSelectionResponse>();
    }
}