using KitchenClube.Controllers;
using KitchenClube.Exceptions;
using KitchenClube.Requests.Food;
using KitchenClube.Responses;
using KitchenClube.Services;
using KitchenClube.Validators;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Test.FoodTest;

public class FoodControllerTest
{
    private readonly IFoodService _service;
    private readonly FoodsController _controller;
    private readonly CreateFoodValidator _createFood;
    private readonly UpdateFoodValidator _updateFood;

    public FoodControllerTest()
    {
        _createFood = new CreateFoodValidator();
        _updateFood = new UpdateFoodValidator();
        _service = new FoodServiceFake();
        _controller = new FoodsController(_service);
    }

    [Fact]
    public async void Get_WhenCalled_ReturnsOkResult()
    {
        var result = await _controller.GetFoods();
        Assert.IsType<ActionResult<IEnumerable<FoodResponse>>>(result);
    }

    [Fact]
    public async void Get_WhenCalled_ReturnsAllItems()
    {
        var resultAct = await _controller.GetFoods(); 
        
        var okResult = resultAct.Result as OkObjectResult;
        var result = okResult.Value as IEnumerable<FoodResponse>;

        Assert.Equal(3, result.Count());
    }

    [Fact]
    public async void GetById_UnknownGuidPassed_ReturnsNotFoundResponse()
    {
        await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.GetFood(Guid.NewGuid()));
    }

    [Fact]
    public async void GetById_ExistingGuidPassed_ReturnsFoodResponse()
    {
        var id = new Guid("D25AD448-3FA4-468E-AF78-839EA8A570C3");

        var resultAct = await _controller.GetFood(id);

        Assert.IsType<ActionResult<FoodResponse>>(resultAct);
    }

    [Fact]
    public async void GetById_ExistingGuidPassed_RightItem()
    {
        var id = new Guid("D25AD448-3FA4-468E-AF78-839EA8A570C3");

        var resultAct = await _controller.GetFood(id);

        var okResult = resultAct.Result as OkObjectResult;
        var result = okResult.Value as FoodResponse;

        Assert.Equal(id, result.Id);
    }

    [Fact]
    public void Add_InValidObjectPassed_ReturnsNotEmpty()
    {
        var nameEmpty = new CreateFood("","Image", "Description");

        var result = _createFood.Validate(nameEmpty);
        
        Assert.False(result.IsValid);
    }

    [Fact]
    public async void Add_ValidObjectPassed_ReturnsCreatedResponse()
    {
        var testFood = new CreateFood("Test", "TestImage", "TestDescription");

        var createdFood = await _controller.PostFood(testFood);
        
        var result = createdFood.Result as CreatedAtActionResult;

        Assert.IsType<CreatedAtActionResult>(result);
    }

    [Fact]
    public async void Add_ValidObjectPassed_ReturnsHasCreatedItemResponse()
    {
        var testFood = new CreateFood("Test", "TestImage", "TestDescription");

        var createdFood = await _controller.PostFood(testFood);

        var okResult = createdFood.Result as CreatedAtActionResult;
        var result = okResult.Value as FoodResponse;

        Assert.Equal("Test",result.Name);
    }

    [Fact]
    public void Update_InValidObjectPassed_ReturnsNotEmpty()
    {
        var nameEmpty = new UpdateFood("", "Image", "Description", true);

        var result = _updateFood.Validate(nameEmpty);

        Assert.False(result.IsValid);
    }

    [Fact]
    public async void Update_InValidGuidPassed_ReturnsNotFoundException()
    {
        var inValidGuid = Guid.NewGuid();
        var updateFood = new UpdateFood("Name", "Image", "Description", true);

        await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.PutFood(inValidGuid, updateFood));
    }

    [Fact]
    public async void Update_ValidObjectAndGuidPassed_ReturnsNoContent()
    {
        var id = new Guid("31801C4C-CBD2-45D6-AF8A-3B21210562D9");
        var updateFood = new UpdateFood("Test","Image", "Бо гӯшти гов", true);

        var act = await _controller.PutFood(id, updateFood);

        Assert.IsType<NoContentResult>(act);
    }

    [Fact]
    public async void Remove_NotExistingGuidPassed_ReturnsNotFoundException()
    {
        await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.DeleteFood(Guid.NewGuid()));
    }

    [Fact]
    public async void Remove_ExistingGuidPassedInMenuItem_ReturnsExistsException()
    {
        var existingGuid = new Guid("D25AD448-3FA4-468E-AF78-839EA8A570C3");

        await Assert.ThrowsAsync<BadRequestException>(async ()=> await _controller.DeleteFood(existingGuid));
    }
   
    [Fact]
    public async void Remove_ExistingGuidPassed_ReturnsNoContentResult()
    {
        var existingGuid = new Guid("31801C4C-CBD2-45D6-AF8A-3B21210562D9");

        var noContent = await _controller.DeleteFood(existingGuid);

        Assert.IsType<NoContentResult>(noContent);
    }

    [Fact]
    public async void Remove_ExistingGuidPassed_RemoveOneItem()
    {
        var existingGuid = new Guid("31801C4C-CBD2-45D6-AF8A-3B21210562D9");

        await _controller.DeleteFood(existingGuid);

        var getAll = await _service.GetAllAsync();
        var countFoods = getAll.Count();

        Assert.Equal(2, countFoods);
    }
}
