namespace KitchenClube.Validators;

public class CreateMenuValidator:AbstractValidator<CreateMenu>
{
    public CreateMenuValidator()
    {
        RuleFor(c=> c.StartDate).NotEqual(c=> c.EndDate).WithMessage("Start date and End date can not be equal.");//TODO: Remove message, 
        RuleFor(c=> c.StartDate).GreaterThan(c=> c.EndDate).WithMessage("End date must be greater than Start date."); //TODO: LessThen
    }
}
