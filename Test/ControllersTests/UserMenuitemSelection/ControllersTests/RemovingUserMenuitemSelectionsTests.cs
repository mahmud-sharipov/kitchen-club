namespace Test.UserMenuitemSelection.ControllersTests;

public class RemovingUserMenuitemSelectionsTests : BaseTest
{
    [Fact]
    public async Task Remove_UnknownGuidPassed_ReturnsNotFoundResponse()
    {
        var id = Guid.NewGuid();
        _serviceMock.Setup(s => s.DeleteAsync(id))
            .Callback(() => throw new NotFoundException(nameof(UserMenuitemSelection), id));

        await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.DeleteUserMenuItemSelection(id));
    }

    [Fact]
    public async Task Remove_ValidObjectPassed_ReturnsDeletedResponse()
    {
        var id = new Guid("D75AA3B6-B166-4DCF-A1EE-9E84C2BDA0D0");

        _serviceMock.Setup(s => s.DeleteAsync(id)).Returns(Task.CompletedTask);
        await _controller.DeleteUserMenuItemSelection(id);

        _serviceMock.Verify(s => s.DeleteAsync(id), Times.Once);
    }
}