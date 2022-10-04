namespace Test.RoleTest.ValidatorTests;

public class UpdateRoleValidatorTests
{
    private readonly UpdateRoleValidator _validator;

    public UpdateRoleValidatorTests()
    {
        _validator = new UpdateRoleValidator();
    }

    [Theory]
    [InlineData("", false)]
    [InlineData("Tester", true)]

    public void UpdateRoleValidator_EmptyAndNotEmptyRoleNamePassed(string roleName, bool expected)
    {
        var updateRoleTest = new UpdateRole(roleName, true);

        var result = _validator.Validate(updateRoleTest);

        Assert.Equal(expected, result.IsValid);
    }
}
