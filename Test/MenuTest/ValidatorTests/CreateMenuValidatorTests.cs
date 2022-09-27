namespace Test.MenuTest.ValidatorTests;

public class CreateMenuValidatorTests
{
    private readonly CreateMenuValidator _validator;
    public CreateMenuValidatorTests()
    {
        _validator = new CreateMenuValidator();
    }

    [Theory]
    [MemberData(nameof(CreateMenuValidatorTestData))]
    public void CreateMenuValidator(DateTime startDate, DateTime endDate, bool expected)
    {
        var createMenutest = new CreateMenu(startDate, endDate);

        var result = _validator.Validate(createMenutest);

        Assert.Equal(expected, result.IsValid);
    }

    public static IEnumerable<object[]> CreateMenuValidatorTestData()
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
