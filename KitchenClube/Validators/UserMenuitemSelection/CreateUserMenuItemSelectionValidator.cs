namespace KitchenClube.Validators;

public class CreateUserMenuItemSelectionValidator:AbstractValidator<CreateUserMenuItemSelection>
{
    public CreateUserMenuItemSelectionValidator()
    {
        //TODO: You do not need to call NotNull if you have NotEmpty
        RuleFor(c => c.Vote).NotEmpty().NotNull();
        RuleFor(c => c.MenuitemId).NotEmpty().NotNull();
    }
}
