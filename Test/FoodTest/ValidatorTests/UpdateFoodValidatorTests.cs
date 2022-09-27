namespace Test.FoodTest.ValidatorTests;

public class UpdateFoodValidatorTests
{
    private readonly UpdateFoodValidator _updateFood;

    public UpdateFoodValidatorTests()
    {
        _updateFood = new UpdateFoodValidator();
    }

    [Theory]
    [InlineData("", false)]
    [InlineData("Tester", true)]
    public void UpdateFoodValidator_EmptyAndNotEmptyNamePassed(string name, bool expected)
    {
        var updateFood = new UpdateFood(name, "Image", "", true);

        var result = _updateFood.Validate(updateFood);

        Assert.Equal(expected, result.IsValid);
    }
}
