namespace KitchenClube.Validators;

public class CreateUserValidator : AbstractValidator<CreateUser>
{
    public CreateUserValidator()
    {
        RuleFor(u => u.FullName).NotEmpty();  
        RuleFor(u => u.PhoneNumber).NotEmpty();
        RuleFor(u => u.Roles).NotEmpty();
        RuleFor(u => u.Email).EmailAddress();
        RuleFor(u => u.Password).NotEmpty().MinimumLength(6)
            .Matches("[a-z]").WithMessage("Password should contain one or more letters");
    }
}
