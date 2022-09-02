namespace KitchenClube.Validators;

public class CreateUserMenuItemSelectionValidator:AbstractValidator<CreateUserMenuItemSelection>
{
    public CreateUserMenuItemSelectionValidator()
    {
        RuleFor(c => c.MenuitemId).NotEmpty();
    }
}
