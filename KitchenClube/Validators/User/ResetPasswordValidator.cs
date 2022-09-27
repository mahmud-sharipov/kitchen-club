namespace KitchenClube.Validators.User;

public class ResetPasswordValidator : AbstractValidator<ResetPasswordUser>
{
    public ResetPasswordValidator()
    {
        RuleFor(r => r.NewPassword).NotEmpty();
    }
}
