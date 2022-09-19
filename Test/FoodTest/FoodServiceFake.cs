using KitchenClube.Requests.Food;
using KitchenClube.Responses;
using KitchenClube.Services;
using KitchenClube.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using KitchenClube.Exceptions;

namespace Test.FoodTest;

public class FoodServiceFake : IFoodService
{
        public Task<FoodResponse> CreateAsync(CreateFood createFood)
    {
        var food = new Food()
        {
            Name = createFood.Name,
            Description = createFood.Description,
            Image = createFood.Image,
            IsActive = true
        };
        Context.Foods.Add(food);

        var foodResponse = new FoodResponse(food.Id, food.Name, food.Image, food.Description,food.IsActive);
        return Task.FromResult(foodResponse);
    }

    public Task<IEnumerable<FoodResponse>> GetAllAsync()
    {
        return Task.FromResult(Context.Foods.Select(f => new FoodResponse(f.Id,f.Name,f.Image,f.Description,f.IsActive)));
    }

    public Task<FoodResponse> GetAsync(Guid id)
    {
        var result = Context.Foods.Where(f => f.Id == id)
            .Select(f => new FoodResponse(f.Id, f.Name, f.Image, f.Description, f.IsActive)).FirstOrDefault();

        return result is null ? throw new NotFoundException(nameof(Food), id) : Task.FromResult(result);
    }

    public Task DeleteAsync(Guid id)
    {
        if (Context.MenuItems.Any(mi => mi.FoodId == id))
            throw new BadRequestException("Food cannot be deleted because it is used on some menu items!");

        var food = Context.Foods.Where(f=>f.Id == id).FirstOrDefault();

        if (food is null)
           throw new NotFoundException(nameof(Food), id);

        Context.Foods.Remove(food);

        return Task.CompletedTask;
    }

    public Task UpdateAsync(Guid id, UpdateFood updateFood)
    {
        var food = Context.Foods.Where(f => f.Id == id).FirstOrDefault();

        if (food is null)
            throw new NotFoundException(nameof(Food), id);

        food.Name = updateFood.Name;
        food.Description = updateFood.Description;
        food.IsActive = updateFood.IsActive;
        food.Image = updateFood.Image;
        
        return Task.CompletedTask;   
    }
}
