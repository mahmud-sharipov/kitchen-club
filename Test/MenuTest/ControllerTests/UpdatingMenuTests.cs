namespace Test.MenuTest.ControllerTests;

public class UpdatingMenuTests : BaseTest
{
    [Fact]
    public async void Update_InValidGuidPassed_ReturnsNotFoundException()
    {
        var id = Guid.NewGuid();
        var updateMenu = new UpdateMenu(DateTime.Now, DateTime.Now.AddDays(30));

        _serviceMock.Setup(s => s.UpdateAsync(id, updateMenu))
            .Callback(() => throw new NotFoundException(nameof(Menu), id));

        await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.PutMenu(id, updateMenu));
    }

    [Fact]
    public async void UpdateStatusOpen_InValidGuidPassed_ReturnsNotFoundException()
    {
        var id = Guid.NewGuid();

        _serviceMock.Setup(s => s.UpdateStatusOpenAsync(id))
            .Callback(() => throw new NotFoundException(nameof(Menu), id));
     
        await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.PutMenuStatusOpen(id));
    }

    [Fact]
    public async void UpdateStatusOpen_ValidGuidPassed_VerifiesStatusUpdate()
    {
        var id = new Guid("F823140D-70B0-40A0-BF54-03874C6BA0C9");

        _serviceMock.Setup(s=>s.UpdateStatusOpenAsync(id)).Returns(Task.CompletedTask);
        await _controller.PutMenuStatusOpen(id);

        _serviceMock.Verify(s => s.UpdateStatusOpenAsync(id), Times.Once);
    }

    [Fact]
    public async void UpdateStatusClose_InValidGuidPassed_ReturnsNotFoundException()
    {
        var id = Guid.NewGuid();

        _serviceMock.Setup(s => s.UpdateStatusCloseAsync(id))
            .Callback(() => throw new NotFoundException(nameof(Menu), id));

        await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.PutMenuStatusClose(id));
    }

    [Fact]
    public async void UpdateStatusClose_ValidGuidPassed_VerifiesStatusUpdate()
    {
        var id = new Guid("F823140D-70B0-40A0-BF54-03874C6BA0C9");

        _serviceMock.Setup(s => s.UpdateStatusCloseAsync(id)).Returns(Task.CompletedTask);
        await _controller.PutMenuStatusClose(id);

        _serviceMock.Verify(s => s.UpdateStatusCloseAsync(id), Times.Once);
    }
}
