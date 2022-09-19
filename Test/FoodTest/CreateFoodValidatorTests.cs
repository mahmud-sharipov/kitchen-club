namespace Test.FoodTest;

public class CreateFoodValidatorTests
{
    private readonly CreateFoodValidator _createFood;

    public CreateFoodValidatorTests()
    {
        _createFood = new CreateFoodValidator();
    }

    [Fact]
    public void CreateFoodValidator_NameIsEmpty_IsNotValid()
    {
        var nameEmpty = new CreateFood("", "Image", "Description");
        var result = _createFood.Validate(nameEmpty);
        Assert.False(result.IsValid);
    }

    [Fact]
    public void CreateFoodValidator_NameIsNotEmpty_IsValid()
    {
        var nameEmpty = new CreateFood("", "Image", "Description");
        var result = _createFood.Validate(nameEmpty);
        Assert.True(result.IsValid);
    }
}
