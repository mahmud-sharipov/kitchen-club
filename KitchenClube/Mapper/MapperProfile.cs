namespace KitchenClube;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<UpdateUser, User>();
        CreateMap<CreateUser, User>().ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
        CreateMap<User,UserResponse>();

        CreateMap<UpdateFood, Food>();
        CreateMap<CreateFood, Food>();
        CreateMap<Food, FoodResponse>();

        CreateMap<UpdateMenu, Menu>();
        CreateMap<CreateMenu, Menu>();
        CreateMap<Menu, MenuResponse>();

        CreateMap<Menuitem, MenuitemResponse>();

        CreateMap<UpdateRole, Role>();
        CreateMap<CreateRole, Role>();
        CreateMap<Role, RoleResponse>();

        CreateMap<UserMenuitemSelection, UserMenuitemSelectionResponse>();
    }
}