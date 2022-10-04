namespace KitchenClube.Validators;

public class CreateUserMenuItemSelectionValidator:AbstractValidator<CreateUserMenuitemSelection>
{
    public CreateUserMenuItemSelectionValidator()
    {
        RuleFor(c => c.MenuitemId).NotEmpty();
    }
}
