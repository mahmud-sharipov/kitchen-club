namespace Test.RoleTest.ValidatorTests;

public class CreateRoleValidatorTests
{
    private readonly CreateRoleValidator _validator;

    public CreateRoleValidatorTests()
    {
        _validator = new CreateRoleValidator();
    }

    [Theory]
    [InlineData("", false)]
    [InlineData("Tester", true)]
    public void CreateRoleValidator_EmptyAndNotEmptyRoleNamePassed_Returns(string roleName , bool expected)
    {
        var createRoleTest = new CreateRole(roleName);

        var result = _validator.Validate(createRoleTest);

        Assert.Equal(expected ,result.IsValid);
    }
}
