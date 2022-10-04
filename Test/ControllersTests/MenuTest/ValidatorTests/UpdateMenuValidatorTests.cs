namespace Test.MenuTest.ValidatorTests;

public class UpdateMenuValidatorTests
{
    private readonly UpdateMenuValidator _validator;

    public UpdateMenuValidatorTests()
    {
        _validator = new UpdateMenuValidator();
    }

    [Theory]
    [MemberData(nameof(ValidatorTestData))]
    public void UpdateMenu_LessAndEqualDatePassedInEndAndStartDate(DateTime startDate, DateTime endDate, bool expected)
    {
        var updateMenu = new UpdateMenu(startDate, endDate);
        
        var result = _validator.Validate(updateMenu);

        Assert.Equal(expected, result.IsValid);
    }

    public static IEnumerable<object[]> ValidatorTestData()
    {
        return new List<object[]>
        {
            new object[]
            {
                DateTime.Today.AddDays(1),
                DateTime.Today.AddDays(-5),
                false
            },
            new object[]
            {
                DateTime.Today.AddDays(1),
                DateTime.Today.AddDays(1),
                false
            },
            new object[]
            {
                DateTime.Today.AddDays(1),
                DateTime.Today.AddDays(30),
                true
            }
        };
    }
}
