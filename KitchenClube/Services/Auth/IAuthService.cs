namespace KitchenClube.Services;

public interface IAuthService
{
    Task<LoginResponse> Login(LoginUser loginUser);
}
