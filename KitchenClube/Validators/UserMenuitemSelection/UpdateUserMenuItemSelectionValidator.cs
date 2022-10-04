namespace KitchenClube.Validators;

public class UpdateUserMenuItemSelectionValidator:AbstractValidator<UpdateUserMenuitemSelection>
{
    public UpdateUserMenuItemSelectionValidator()
    {
        RuleFor(u => u.Vote).IsInEnum();
        RuleFor(u => u.MenuitemId).NotEmpty();
        RuleFor(u => u.UserId).NotEmpty();
    }
}
