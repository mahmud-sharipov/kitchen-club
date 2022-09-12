namespace KitchenClube.Validators;

public class UpdateUserMenuItemSelectionValidator:AbstractValidator<UpdateUserMenuItemSelection>
{
    public UpdateUserMenuItemSelectionValidator()
    {
        RuleFor(u => u.Vote).NotEmpty();
        RuleFor(u => u.MenuitemId).NotEmpty();
        RuleFor(u => u.UserId).NotEmpty();
    }
}
