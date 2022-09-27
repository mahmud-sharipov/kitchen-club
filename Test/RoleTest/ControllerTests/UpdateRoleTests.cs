namespace Test.RoleTest.ControllerTests;

public class UpdateRoleTests : BaseTest
{
    [Fact]
    public async void Update_UnknownGuidPassed_ReturnsNotFoundResponse()
    {
        var roleIdTest = Guid.NewGuid();
        var updateRoleTest = new UpdateRole("User", false);

        _serviceMock.Setup(s => s.UpdateAsync(roleIdTest, updateRoleTest))
            .Callback(() => throw new NotFoundException(nameof(Role), roleIdTest));

        await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.UpdateRole(roleIdTest, updateRoleTest));
    }

    [Fact]
    public async void Update_ExistingGuidPassed_VerifiesUpdate()
    {
        var roleIdTest = new Guid("5A527020-AE7A-4BA4-BD8D-9BFE06B3535C");
        var updateRoleTest = new UpdateRole("User", false);

        _serviceMock.Setup(s => s.UpdateAsync(roleIdTest, updateRoleTest)).Returns(Task.CompletedTask);
        await _controller.UpdateRole(roleIdTest, updateRoleTest);

        _serviceMock.Verify(s => s.UpdateAsync(roleIdTest, updateRoleTest), Times.Once);
    }
}