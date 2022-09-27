namespace Test.UserMenuitemSelection.ValidatorTests;

public class CreateUserMenuitemSelectionValidatorTests
{
    private CreateUserMenuItemSelectionValidator _validator;

    public CreateUserMenuitemSelectionValidatorTests()
    {
        _validator = new CreateUserMenuItemSelectionValidator();
    }

    [Theory]
    [MemberData(nameof(UserMenuitemSelectionData))]
    public void Create_EmptyAndNotEmptyMenuitemIdPassed(Guid id, bool expected)
    {
        var createTest = new CreateUserMenuItemSelection(UserVote.Yes, id);
        var result = _validator.Validate(createTest);
        Assert.Equal(expected, result.IsValid);
    }

    public static IEnumerable<object[]> UserMenuitemSelectionData()
    {
        return new List<object[]>
        {
            new object[]
            {
                Guid.Empty,
                false
            },
            new object[]
            {
                Guid.NewGuid(),
                true
            }
        };
    }
}
