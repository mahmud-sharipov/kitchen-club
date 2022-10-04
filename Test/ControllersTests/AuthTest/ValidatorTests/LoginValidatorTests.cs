namespace Test.AuthTest.ValidatorTests;

public class LoginValidatorTests
{
    private readonly LoginValidator _validator;
    public LoginValidatorTests()
    {
        _validator = new LoginValidator();
    }

    [Theory]
    [InlineData("email@", false)]
    [InlineData("email@gmail.com", true)]
    public void LoginValidator_WrongAndRightEmailPassed(string email, bool expected)
    {
        var loginTest = new LoginUser(email, "123");
        var result = _validator.Validate(loginTest);
        Assert.Equal(expected, result.IsValid);
    }
}
