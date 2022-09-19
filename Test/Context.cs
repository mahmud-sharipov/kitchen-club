using KitchenClube.Models;
using System;
using System.Collections.Generic;

namespace Test;

public static class Context
{
    public static List<Food> Foods = new List<Food>()
    {
        new Food 
        { 
            Id = new Guid("D25AD448-3FA4-468E-AF78-839EA8A570C3"), 
            Name = "Osh", Description = "1 ба 1", 
            Image = "images/osh.png", 
            IsActive = true 
        },
        new Food 
        { 
            Id = new Guid("33203271-4317-48CB-A02D-5CF7BC94A9F2"), 
            Name = "Kazan-Kebab", 
            Description = "Бо гӯшти гӯспанд", 
            Image = "images/kazan.png", 
            IsActive = true 
        },
        new Food 
        { 
            Id = new Guid("31801C4C-CBD2-45D6-AF8A-3B21210562D9"), 
            Name = "Jazza", 
            Description = "Бо гӯшти гов", 
            Image = "images/jazza.png", 
            IsActive = true 
        }
    };

    public static List<MenuItem> MenuItems = new List<MenuItem>()
    {
        new MenuItem
        {
            Id = new Guid ("E273ABD8-411B-4243-B00A-E5EF92ADAFA1"),
            Day = DateTime.Now.AddDays(1),
            FoodId = new Guid("D25AD448-3FA4-468E-AF78-839EA8A570C3"),
            IsActive = true,
            MenuId = new Guid("3C8AF117-B57B-461B-9F4E-76C22BA101B4")
        },
        new MenuItem
        {
            Id = new Guid ("E5D5421F-D750-43FE-89D9-77784D3660DD"),
            Day = DateTime.Now.AddDays(2),
            FoodId = new Guid("33203271-4317-48CB-A02D-5CF7BC94A9F2"),
            IsActive = true,
            MenuId = new Guid("3C8AF117-B57B-461B-9F4E-76C22BA101B4")
        }
    };

    public static List<Menu> Menu = new List<Menu>()
    {
        new Menu()
        {
            Id = new Guid("3C8AF117-B57B-461B-9F4E-76C22BA101B4"),
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(30),
            Status = MenuStatus.Active
        },
        new Menu()
        {
            Id = new Guid("31DF1482-406C-45EC-90A8-792C29793594"),
            StartDate = DateTime.Now.AddDays(30),
            EndDate = DateTime.Now.AddDays(60),
            Status = MenuStatus.Active
        },

    };

    public static List<User> Users = new List<User>()
    {
        new User()
        {
            Id = new Guid(),
            Email = "nomalum1@gmail.com",
            FullName = "Kamolov Fariz",
            IsActive = true,
            PhoneNumber = "+12345",
            PasswordHash = "123"
        }
    };
}
