namespace KitchenClube.Validators;

public class UpdateFoodValidator : AbstractValidator<UpdateFood>
{
    public UpdateFoodValidator()
    {
        RuleFor(u => u.Name).NotEmpty().NotNull();
        RuleFor(u => u.Image).NotEmpty().NotNull();
        RuleFor(u => u.Description).NotEmpty().NotNull();
    }
}
