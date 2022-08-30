namespace KitchenClube.Validators;

public class CreateUserMenuItemSelectionValidator:AbstractValidator<CreateUserMenuItemSelection>
{
    public CreateUserMenuItemSelectionValidator()
    {
        RuleFor(c => c.Vote).NotEmpty().NotNull();
        RuleFor(c => c.MenuitemId).NotEmpty().NotNull();
    }
}
