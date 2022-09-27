namespace KitchenClube.Validators.User;

public class UpdateUserRoleValidator : AbstractValidator<UpdateUserRole>
{
    public UpdateUserRoleValidator()
    {
        RuleFor(u => u.Roles).NotEmpty();
    }
}
