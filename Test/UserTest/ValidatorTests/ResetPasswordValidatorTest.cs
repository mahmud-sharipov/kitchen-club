namespace Test.UserTest.ValidatorTests;

public class ResetPasswordValidatorTest
{
    private readonly ResetPasswordValidator _validator;
    public ResetPasswordValidatorTest()
    {
        _validator = new ResetPasswordValidator();
    }

    [Theory]
    [InlineData("", false)]
    [InlineData("123456", true)]
    public void Reset_EmptyAndNotEmptyNewPasswordPassed(string newPassword, bool expected)
    {
        var resetPasswordTest = new ResetPasswordUser(newPassword);

        var result = _validator.Validate(resetPasswordTest);

        Assert.Equal(expected, result.IsValid);
    }
}
