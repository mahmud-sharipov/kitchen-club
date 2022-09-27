namespace Test.MenuTest.ControllerTests;

public class RemovingMenuTests : BaseTest
{
    [Fact]
    public async void Remove_UnknownGuidPassed_ReturnsNotFoundException()
    {
        var id = Guid.NewGuid();
        _serviceMock.Setup(s => s.DeleteAsync(id)).Callback(() => throw new NotFoundException(nameof(Menu), id));

        await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.DeleteMenu(id));
    }

    [Fact]
    public async void Remove_ExistingGuidPassed_RemoveOneItem()
    {
        var id = new Guid("31DF1482-406C-45EC-90A8-792C29793594");

        _serviceMock.Setup(s => s.DeleteAsync(id)).Returns(Task.CompletedTask);

        await _controller.DeleteMenu(id);

        _serviceMock.Verify((s) => s.DeleteAsync(id), Times.Once);
    }
}
