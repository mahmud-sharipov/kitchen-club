namespace KitchenClube.Validators;

public class CreateMenuValidator:AbstractValidator<CreateMenu>
{
    public CreateMenuValidator()
    {
        RuleFor(c => c.StartDate).NotEqual(c => c.EndDate);
        RuleFor(c => c.StartDate).LessThan(c => c.EndDate);
    }
}
