namespace KitchenClube.Validators;

public class UpdateUserValidator:AbstractValidator<UpdateUser>
{
    public UpdateUserValidator()
    {
        RuleFor(u => u.FullName).NotEmpty().NotNull();
        RuleFor(u => u.PhoneNumber).NotEmpty().NotNull();
        RuleFor(u => u.RoleId).NotEmpty();
        RuleFor(u => u.IsActive).NotEmpty().NotNull();
    }
}
