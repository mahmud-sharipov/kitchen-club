namespace Test.UserTest.ValidatorTests;

public class CreateUserValidatorTest
{
    private readonly CreateUserValidator _validator;
    public CreateUserValidatorTest()
    {
        _validator = new CreateUserValidator();
    }

    [Theory]
    [MemberData(nameof(ValidatorData))]
    public void CreateUser_InvalidAndValidObjectsPassed(string name, string phone, string email, 
        string[] roles, string password, bool expected)
    {
        var createUserTest = new CreateUser(name, phone, email, roles, password);

        var result = _validator.Validate(createUserTest);

        Assert.Equal(expected, result.IsValid);
    }

    public static IEnumerable<object[]> ValidatorData()
    {
        var roles = new string[] { "User" };
        var name = "Testjon Testov";
        var phone = "992927773377";
        var email = "test@gmail.com";
        var password = "123456";

        return new List<object[]>
        {
            new object[]
            {
                "", phone, email, roles, password, false
            },
            new object[]
            {
                name, "", email, roles, password, false
            },
            new object[]
            {
                name, phone, "", roles, password, false
            },
            new object[]
            {
                name, phone, email, new string[0], password, false
            },
            new object[]
            {
                name, phone, email, roles, "", false
            },
            new object[]
            {
                name, phone, email, roles, "123", false
            },
            new object[]
            {
                name, phone, email, roles, password, true
            }
        };
    }
}
