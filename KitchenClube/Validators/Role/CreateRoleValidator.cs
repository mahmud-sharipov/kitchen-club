namespace KitchenClube.Validators;

public class CreateRoleValidator:AbstractValidator<Role>
{
    public CreateRoleValidator()
    {
        RuleFor(r => r.Name).NotEmpty();
    }
}
