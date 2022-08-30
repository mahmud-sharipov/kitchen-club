namespace KitchenClube.Validators;

public class CreateFoodValidator : AbstractValidator<CreateFood>
{
    public CreateFoodValidator()
    {
        RuleFor(x => x.Name).NotEmpty().NotNull();
        RuleFor(u=>u.Image).NotEmpty().NotNull();
        RuleFor(u => u.Description).NotEmpty().NotNull();
    }
}
