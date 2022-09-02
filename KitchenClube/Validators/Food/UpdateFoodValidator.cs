namespace KitchenClube.Validators;

public class UpdateFoodValidator : AbstractValidator<UpdateFood>
{
    public UpdateFoodValidator()
    {
        RuleFor(u => u.Name).NotEmpty();
    }
}
