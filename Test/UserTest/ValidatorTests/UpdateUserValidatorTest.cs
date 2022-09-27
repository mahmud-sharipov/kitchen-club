namespace Test.UserTest.ValidatorTests;

public class UpdateUserValidatorTest
{
    private readonly UpdateUserValidator _validator;

    public UpdateUserValidatorTest()
    {
        _validator = new UpdateUserValidator();
    }

    [Theory]    
    [InlineData("", "+992927773377", false)]
    [InlineData("Testjon Tetov", "", false)]
    [InlineData("Testjon Testov", "+992927773377", true)]
    public void UpdateUser_InvalidAndValidObjectsPassed(string name, string phone, bool expected)
    {
        var updateUserTest = new UpdateUser(name, phone, true);

        var result = _validator.Validate(updateUserTest);

        Assert.Equal(expected, result.IsValid);
    }
}
