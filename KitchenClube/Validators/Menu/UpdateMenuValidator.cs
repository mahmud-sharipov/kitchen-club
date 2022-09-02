namespace KitchenClube.Validators;

public class UpdateMenuValidator:AbstractValidator<UpdateMenu>
{
    public UpdateMenuValidator()
    {
        RuleFor(m => m.StartDate).LessThan(m => m.EndDate);
        RuleFor(m => m.StartDate).NotEqual(m => m.EndDate);
    }
}
