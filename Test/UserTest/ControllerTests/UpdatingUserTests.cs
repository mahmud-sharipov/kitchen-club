namespace Test.UserTest.ControllerTests;

public class UpdatingUserTests : BaseTest
{
    [Fact]
    public async void Update_InvalidUserIdPassed_ReturnsNotFoundReponse()
    {
        var userIdTest = Guid.NewGuid();
        var updateUserTest = new UpdateUser("Testjon Testov", "+992927773377", true);

        _serviceMock.Setup(s => s.UpdateAsync(userIdTest, updateUserTest))
            .Callback(() => throw new NotFoundException(nameof(User), userIdTest));

        await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.PutUser(userIdTest, updateUserTest));
    }

    [Fact]
    public async void Update_ValidObjectsPassed_VerifiesUpdate()
    {
        var userIdTest = new Guid("2EA3E5EB-CBCB-4A67-B868-61B96F9A9D60");
        var updateUserTest = new UpdateUser("Testjon Testov", "+992927773377", true);

        _serviceMock.Setup(s=>s.UpdateAsync(userIdTest, updateUserTest)).Returns(Task.CompletedTask);
        await _controller.PutUser(userIdTest, updateUserTest);

        _serviceMock.Verify(s=>s.UpdateAsync(userIdTest, updateUserTest), Times.Once());
    }

    [Fact]
    public async void UpdateRole_UnknownGuidPassed_ReturnsNotFoundResponse()
    {
        var userIdTest = Guid.NewGuid();
        var roles = new List<string>() { "User" };
        var updateRolesTest = new UpdateUserRole(roles);

        _serviceMock.Setup(s => s.UpdateAsync(userIdTest, updateRolesTest))
            .Callback(() => throw new NotFoundException(nameof(User), userIdTest));

        await Assert.ThrowsAsync<NotFoundException>(async () => 
                await _controller.PutUserRole(userIdTest, updateRolesTest));
    }

    [Fact]
    public async void UpdateRole_ValidObjectsPassed_ReturnsTaskCompleted()
    {
        var userIdTest = new Guid("2EA3E5EB-CBCB-4A67-B868-61B96F9A9D60");
        var roles = new List<string>() { "User" };
        var updateRolesTest = new UpdateUserRole(roles);

        _serviceMock.Setup(s => s.UpdateAsync(userIdTest, updateRolesTest)).Returns(Task.CompletedTask);
        await _controller.PutUserRole(userIdTest, updateRolesTest);

        _serviceMock.Verify(s => s.UpdateAsync(userIdTest, updateRolesTest), Times.Once());
    }

}
