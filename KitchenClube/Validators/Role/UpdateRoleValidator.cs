namespace KitchenClube.Validators;

public class UpdateRoleValidator:AbstractValidator<UpdateRole>
{
    public UpdateRoleValidator()
    {
        RuleFor(r => r.Name).NotEmpty();
    }
}
