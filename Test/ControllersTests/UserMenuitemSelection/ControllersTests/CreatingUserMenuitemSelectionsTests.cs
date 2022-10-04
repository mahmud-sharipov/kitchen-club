namespace Test.UserMenuitemSelection.ControllersTests;

public class CreatingUserMenuitemSelectionsTests : BaseTest
{
    [Fact]
    public async Task Create_InValidMenuitemGuidPassed_ReturnsNotFoundResponse()
    {
        var menuitemId = Guid.NewGuid();
        var userMenuitemSelection = new CreateUserMenuitemSelection(UserVote.Yes, menuitemId);

        _serviceMock.Setup(s => s.CreateAsync(userMenuitemSelection))
            .Callback(() => throw new NotFoundException(nameof(Menuitem),menuitemId));

        await Assert.ThrowsAsync<NotFoundException>(async () => 
            await _controller.PostUserMenuItemSelection(userMenuitemSelection));
    }

    [Fact]
    public async Task Create_ValidDayAndMenuitemIdPassed_ReturnsCreateResponse()
    {
        var userId = new Guid("2EA3E5EB-CBCB-4A67-B868-61B96F9A9D60");
        var menuitemId = new Guid("E5D5421F-D750-43FE-89D9-77784D3660DD");
        var userMenuitemSelection = new CreateUserMenuitemSelection(UserVote.Yes, menuitemId);
        var response = new UserMenuitemSelectionResponse(Guid.NewGuid(), menuitemId, userId, userMenuitemSelection.Vote);

        _serviceMock.Setup(s => s.CreateAsync(userMenuitemSelection)).ReturnsAsync(response);
        var resultAct = await _controller.PostUserMenuItemSelection(userMenuitemSelection);
        var okResult = resultAct.Result as CreatedAtActionResult;
        var result = okResult.Value as UserMenuitemSelectionResponse;

        Assert.Same(response, result);
    }
}
