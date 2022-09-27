namespace Test.UserTest.ControllerTests;

public class RemovingUserTests : BaseTest
{
    [Fact]
    public async void Remove_UnknownGuidPassed_ReturnsNotFoundResponse()
    {
        var userIdTest = Guid.NewGuid();

        _serviceMock.Setup(s => s.DeleteAsync(userIdTest))
            .Callback(() => throw new NotFoundException(nameof(User), userIdTest));

        await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.DeleteUser(userIdTest));
    }

    [Fact]
    public async void Remove_ExisitngGuidPassed_ReturnsTaskCompleted()
    {
        var userIdTest = new Guid("2EA3E5EB-CBCB-4A67-B868-61B96F9A9D60");

        _serviceMock.Setup(s=>s.DeleteAsync(userIdTest)).Returns(Task.CompletedTask);
        await _controller.DeleteUser(userIdTest);

        _serviceMock.Verify(s=>s.DeleteAsync(userIdTest), Times.Once);
    }
}