namespace Test.MenuItemTest.ControllerTests;

public class RemovingMenuItemTests : BaseTest
{
    [Fact]
    public async void Remove_UnknownGuidPassed_ReturnsNotFoundResponse()
    {
        var id = Guid.NewGuid();
        _serviceMock.Setup(s => s.DeleteAsync(id)).Callback(() => throw new NotFoundException(nameof(MenuItem), id));

        await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.DeleteMenuItem(id));
    }

    [Fact]
    public async void Remove_ExistingGuidPassed_RemoveOneItem()
    {
        var id = new Guid("1DA1E472-625D-4A5B-8538-D2DC39EF4FEA");
        _serviceMock.Setup(s => s.DeleteAsync(id)).Returns(Task.CompletedTask);

        await _controller.DeleteMenuItem(id);

        _serviceMock.Verify(s => s.DeleteAsync(id), Times.Once);
    }
}
