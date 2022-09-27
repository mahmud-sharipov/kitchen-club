namespace Test.FoodTest.ControllerTests;

public class UpdatingFoodTests : BaseTest
{
    [Fact]
    public async void Update_InValidGuidPassed_ReturnsNotFoundException()
    {
        var inValidGuid = Guid.NewGuid();
        var updateFood = new UpdateFood("Name", "Image", "Description", true);

        _serviceMock.Setup(x => x.UpdateAsync(inValidGuid, updateFood))
            .Callback(() => throw new NotFoundException(nameof(Food), inValidGuid));

        await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.PutFood(inValidGuid, updateFood));
    }

    [Fact]
    public async void Update_ValidObjectAndGuidPassed_VerifiesUpdate()
    {
        var id = new Guid("31801C4C-CBD2-45D6-AF8A-3B21210562D9");
        var updateFood = new UpdateFood("Test", "Image", "Бо гӯшти гов", true);

        _serviceMock.Setup(x => x.UpdateAsync(id, updateFood)).Returns(Task.CompletedTask);
        var act = await _controller.PutFood(id, updateFood);

        _serviceMock.Verify(s => s.UpdateAsync(id, updateFood), Times.Once);
    }
}