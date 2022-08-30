namespace KitchenClube.Validators;

public class CreateUserValidator : AbstractValidator<CreateUser>
{
    public CreateUserValidator()
    {
        RuleFor(u => u.FullName).NotEmpty().NotNull();  
        RuleFor(u => u.PhoneNumber).NotEmpty().NotNull();
        RuleFor(u => u.Password).NotEmpty().NotNull();
        RuleFor(u=>u.Email).NotEmpty().EmailAddress();
    }
}
