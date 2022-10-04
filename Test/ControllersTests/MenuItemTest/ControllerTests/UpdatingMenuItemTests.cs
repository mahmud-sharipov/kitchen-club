namespace Test.MenuItemTest.ControllerTests;

public class UpdatingMenuItemTests : BaseTest
{
    [Fact]
    public async Task Update_InValidGuidPassed_ReturnsNotFoundReponse()
    {
        var id = Guid.NewGuid();
        var updateMenuitem = new UpdateMenuitem(DateTime.Now, new Guid("33203271-4317-48CB-A02D-5CF7BC94A9F2"),
            new Guid("F823140D-70B0-40A0-BF54-03874C6BA0C9"), true);

        _serviceMock.Setup(s => s.UpdateAsync(id, updateMenuitem))
            .Callback(() => throw new NotFoundException(nameof(Menuitem), id));

        await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.PutMenuitem(id, updateMenuitem));
    }

    [Fact]
    public async Task Update_ValidGuidAndObjectPassed_ReturnsUpdateResponse()
    {
        var menuItemId = new Guid("E5D5421F-D750-43FE-89D9-77784D3660DD");

        var updateMenuItem =
            new UpdateMenuitem(DateTime.Now.AddDays(2), new Guid("31801C4C-CBD2-45D6-AF8A-3B21210562D9"),
            new Guid("3C8AF117-B57B-461B-9F4E-76C22BA101B4"), true);

        _serviceMock.Setup(s => s.UpdateAsync(menuItemId, updateMenuItem)).Returns(Task.CompletedTask);
        await _controller.PutMenuitem(menuItemId, updateMenuItem);

        _serviceMock.Verify(s=>s.UpdateAsync(menuItemId, updateMenuItem), Times.Once);
    }
}

