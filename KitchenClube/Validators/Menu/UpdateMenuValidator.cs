namespace KitchenClube.Validators;

public class UpdateMenuValidator:AbstractValidator<UpdateMenu>
{
    public UpdateMenuValidator()
    {
        RuleFor(m => m.StartDate).GreaterThan(m => m.EndDate).WithMessage("End date must be greater than Start date.");
        RuleFor(m => m.StartDate).Equal(m => m.EndDate).WithMessage("Start date and End date can not be equal.");
    }
}
