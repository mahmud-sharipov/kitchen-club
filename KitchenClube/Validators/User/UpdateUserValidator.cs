namespace KitchenClube.Validators;

public class UpdateUserValidator:AbstractValidator<UpdateUser>
{
    public UpdateUserValidator()
    {
        RuleFor(u => u.FullName).NotEmpty();
        RuleFor(u => u.PhoneNumber).NotEmpty();
    }
}
