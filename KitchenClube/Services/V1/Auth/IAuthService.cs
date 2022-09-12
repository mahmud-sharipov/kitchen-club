namespace KitchenClube.Services.V1;

public interface IAuthService
{
    Task<LoginResponse> Login(LoginUser loginUser);
}
