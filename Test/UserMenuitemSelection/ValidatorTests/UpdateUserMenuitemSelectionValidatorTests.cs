namespace Test.UserMenuitemSelection.ValidatorTests;

public class UpdateUserMenuitemSelectionValidatorTests
{
    private UpdateUserMenuItemSelectionValidator _validator;

    public UpdateUserMenuitemSelectionValidatorTests()
    {
        _validator = new UpdateUserMenuItemSelectionValidator();
    }

    [Theory]
    [MemberData(nameof(UpdateUserMenuitemSelectionData))]
    public void Update_EmptyAndNotEmptyUpdateDataPassed(Guid menuitemId, Guid userId, bool expected)
    {
        var updateTest = new UpdateUserMenuItemSelection(UserVote.Yes, menuitemId, userId);

        var result = _validator.Validate(updateTest);

        Assert.Equal(expected, result.IsValid);
    }

    public static IEnumerable<object[]> UpdateUserMenuitemSelectionData()
    {
        return new List<object[]>()
        {            
            new object[]
            {
                Guid.Empty,
                new Guid("3EA3E5EB-CBCB-4A67-B868-61B96F9A9D60"),
                false
            },
             new object[]
            {
                new Guid("E5D5421F-D750-43FE-89D9-77784D3660DD"),
                Guid.Empty,
                false
            },
              new object[]
            {
                new Guid("E5D5421F-D750-43FE-89D9-77784D3660DD"),
                new Guid("3EA3E5EB-CBCB-4A67-B868-61B96F9A9D60"),
                true
            }
        };
    }
}
