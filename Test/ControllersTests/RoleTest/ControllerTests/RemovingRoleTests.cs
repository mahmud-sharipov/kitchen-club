namespace Test.RoleTest.ControllerTests;

public class RemovingRoleTests : BaseTest
{
    [Fact]
    public async Task Remove_UnknownRoleIdPassed_ReturnsNotFoundResponse()
    {
        var roleIdTest = Guid.NewGuid();

        _serviceMock.Setup(s => s.DeleteAsync(roleIdTest))
            .Callback(() => throw new NotFoundException(nameof(Role), roleIdTest));

        await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.DeleteRole(roleIdTest));
    }

    [Fact]
    public async Task Remove_ExistingRoleIdPassed_RemoveOneItem()
    {
        var roleIdTest = new Guid("5A527020-AE7A-4BA4-BD8D-9BFE06B3535C");

        _serviceMock.Setup(s => s.DeleteAsync(roleIdTest));
        await _controller.DeleteRole(roleIdTest);

        _serviceMock.Verify(s => s.DeleteAsync(roleIdTest), Times.Once);
    }
}
