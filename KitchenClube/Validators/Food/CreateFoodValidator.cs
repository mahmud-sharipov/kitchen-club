namespace KitchenClube.Validators;

public class CreateFoodValidator : AbstractValidator<CreateFood>
{
    public CreateFoodValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}
