namespace KitchenClube.Validators;

public class CreateRoleValidator:AbstractValidator<CreateRole>
{
    public CreateRoleValidator()
    {
        RuleFor(r => r.Name).NotEmpty();
    }
}
