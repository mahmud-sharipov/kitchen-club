namespace KitchenClube.Validators;

public class CreateMenuValidator:AbstractValidator<CreateMenu>
{
    public CreateMenuValidator()
    {
        RuleFor(c=> c.StartDate).Equal(c=> c.EndDate).WithMessage("Start date and End date can not be equal.");
        RuleFor(c=> c.StartDate).GreaterThan(c=> c.EndDate).WithMessage("End date must be greater than Start date.");
    }
}
