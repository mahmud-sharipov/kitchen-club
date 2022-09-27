namespace Test.FoodTest.ControllerTests;

public class RemovingFoodTests : BaseTest
{
    [Fact]
    public async void Remove_UnknownGuidPassed_ReturnsNotFoundException()
    {
        var foodIdTest = Guid.NewGuid();

        _serviceMock.Setup(x => x.DeleteAsync(foodIdTest))
            .Callback(() => throw new NotFoundException(nameof(Food), foodIdTest));

        await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.DeleteFood(foodIdTest));
    }

    [Fact]
    public async void Remove_ExistingGuidPassed_RemoveOneItem()
    {
        var existingFoodIdTest = new Guid("31801C4C-CBD2-45D6-AF8A-3B21210562D9");

        _serviceMock.Setup(x => x.DeleteAsync(existingFoodIdTest));
        await _controller.DeleteFood(existingFoodIdTest);

        _serviceMock.Verify(s => s.DeleteAsync(existingFoodIdTest), Times.Once());
    }
}
