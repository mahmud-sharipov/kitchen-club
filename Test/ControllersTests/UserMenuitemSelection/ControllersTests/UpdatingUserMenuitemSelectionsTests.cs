namespace Test.UserMenuitemSelection.ControllersTests;

public class UpdatingUserMenuitemSelectionsTestsV2 : BaseTest
{
    [Fact]
    public async Task Update_UnknownGuidPassed_ReturnsNotFoundReponse()
    {
        var userMenuitemSelectionIdTest = Guid.NewGuid();
        var menuitemId = new Guid("E5D5421F-D750-43FE-89D9-77784D3660DD");
        var userId = new Guid("3EA3E5EB-CBCB-4A67-B868-61B96F9A9D60");
        var updateTest = new UpdateUserMenuitemSelection(UserVote.No, menuitemId, userId);

        _serviceMock.Setup(s => s.UpdateAsync(userMenuitemSelectionIdTest, updateTest))
            .Callback(() => throw new NotFoundException(nameof(UserMenuitemSelection), userMenuitemSelectionIdTest));

        await Assert.ThrowsAsync<NotFoundException>(async () => 
                await _controller.PutUserMenuItemSelection(userMenuitemSelectionIdTest, updateTest));
    }

    [Fact]
    public async Task Update_ValidObjectPassed_ReturnsUpdatedResult()
    {
        var id = new Guid("D75AA3B6-B166-4DCF-A1EE-9E84C2BDA0D0");
        var menuitemId = new Guid("E5D5421F-D750-43FE-89D9-77784D3660DD");
        var userId = new Guid("3EA3E5EB-CBCB-4A67-B868-61B96F9A9D60");
        var updateTest = new UpdateUserMenuitemSelection(UserVote.Yes, menuitemId, userId);

        _serviceMock.Setup(s => s.UpdateAsync(id, updateTest)).Returns(Task.CompletedTask);
        await _controller.PutUserMenuItemSelection(id, updateTest);

        _serviceMock.Verify(s => s.UpdateAsync(id, updateTest), Times.Once);
    }
}