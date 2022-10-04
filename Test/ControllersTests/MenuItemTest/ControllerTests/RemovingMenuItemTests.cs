namespace Test.MenuItemTest.ControllerTests;

public class RemovingMenuItemTests : BaseTest
{
    [Fact]
    public async Task Remove_UnknownGuidPassed_ReturnsNotFoundResponse()
    {
        var id = Guid.NewGuid();
        _serviceMock.Setup(s => s.DeleteAsync(id)).Callback(() => throw new NotFoundException(nameof(Menuitem), id));

        await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.DeleteMenuitem(id));
    }

    [Fact]
    public async Task Remove_ExistingGuidPassed_RemoveOneItem()
    {
        var id = new Guid("1DA1E472-625D-4A5B-8538-D2DC39EF4FEA");
        _serviceMock.Setup(s => s.DeleteAsync(id)).Returns(Task.CompletedTask);

        await _controller.DeleteMenuitem(id);

        _serviceMock.Verify(s => s.DeleteAsync(id), Times.Once);
    }
}
