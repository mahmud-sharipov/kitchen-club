namespace KitchenClube.Validators;

public class LoginValidator : AbstractValidator<LoginUser>
{
    public LoginValidator()
    {
        RuleFor(l => l.Email).EmailAddress();
    }
}
