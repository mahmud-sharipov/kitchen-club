namespace KitchenClube.Validators;

public class UpdateUserMenuItemSelectionValidator:AbstractValidator<UpdateUserMenuItemSelection>
{
    public UpdateUserMenuItemSelectionValidator()
    {
        RuleFor(u => u.Vote).NotEmpty().NotNull();
        RuleFor(u => u.MenuitemId).NotEmpty().NotNull();
        RuleFor(u => u.UserId).NotEmpty().NotNull();
    }
}
