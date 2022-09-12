namespace KitchenClube.Services.V2;

public interface IAuthService
{
    Task<LoginResponse> Login(LoginUser loginUser);
}
