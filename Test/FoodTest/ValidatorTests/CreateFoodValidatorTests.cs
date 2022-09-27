namespace Test.FoodTest.ValidatorTests;

public class CreateFoodValidatorTests
{
    private readonly CreateFoodValidator _createFood;

    public CreateFoodValidatorTests()
    {
        _createFood = new CreateFoodValidator();
    }

    [Theory]
    [InlineData("", false)]
    [InlineData("Name", true)]
    public void CreaateFoodValidator_EmptyAndNotEmptyNamePassed(string name, bool expected)
    {
        var createFood = new CreateFood(name, "Image", "Description");

        var result = _createFood.Validate(createFood);

        Assert.Equal(expected, result.IsValid);
    }
}
