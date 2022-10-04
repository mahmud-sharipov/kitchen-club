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

    public static List<User> Users = new List<User>()
    {
        new User()
        {
            Id = new Guid("2EA3E5EB-CBCB-4A67-B868-61B96F9A9D60"),
            Email = "nomalum1@gmail.com",
            FullName = "Kamolov Fariz",
            IsActive = true,
            PhoneNumber = "+12345",
            PasswordHash = "1234567"            
        },
        new User()
        {
            Id = new Guid("3EA3E5EB-CBCB-4A67-B868-61B96F9A9D60"),
            Email = "example@gmail.com",
            FullName = "Azizov Gafur",
            IsActive = true,
            PhoneNumber = "+456789",
            PasswordHash = "7890123"
        },
    };

    public static List<Role> Roles = new List<Role>()
    {
        new Role("Admin")
        {
            Id = new Guid("5A527020-AE7A-4BA4-BD8D-9BFE06B3535C"),
            IsActive = true
        },
        new Role("User")
        {
           Id = new Guid("0205617B-8FE1-4370-B78A-7F03F733CBBA"),
           IsActive = true
        }
    };

    public static Dictionary<Guid, List<Guid>> UserRoles()
    {
        return new Dictionary<Guid, List<Guid>>()
        {
            {
                new Guid("2EA3E5EB-CBCB-4A67-B868-61B96F9A9D60"),
                new List<Guid>()
                {
                    new Guid("5A527020-AE7A-4BA4-BD8D-9BFE06B3535C"),
                    new Guid("0205617B-8FE1-4370-B78A-7F03F733CBBA")
                }
            },
            {
                new Guid("3EA3E5EB-CBCB-4A67-B868-61B96F9A9D60"),
                new List<Guid>()
                {
                    new Guid("0205617B-8FE1-4370-B78A-7F03F733CBBA")
                }
            }
        };        
    }

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
        new Menu()
        {
            Id = new Guid("F823140D-70B0-40A0-BF54-03874C6BA0C9"),
            StartDate = DateTime.Now.AddDays(30),
            EndDate = DateTime.Now.AddDays(60),
            Status = MenuStatus.Closed
        },
    };

    public static List<Menuitem> MenuItems = new List<Menuitem>()
    {
        new Menuitem
        {
            Id = new Guid ("E273ABD8-411B-4243-B00A-E5EF92ADAFA1"),
            Day = DateTime.Now.AddDays(1),
            FoodId = new Guid("D25AD448-3FA4-468E-AF78-839EA8A570C3"),
            IsActive = true,
            MenuId = new Guid("3C8AF117-B57B-461B-9F4E-76C22BA101B4")
        },
        new Menuitem
        {
            Id = new Guid ("E5D5421F-D750-43FE-89D9-77784D3660DD"),
            Day = DateTime.Now.AddDays(2),
            FoodId = new Guid("33203271-4317-48CB-A02D-5CF7BC94A9F2"),
            IsActive = true,
            MenuId = new Guid("3C8AF117-B57B-461B-9F4E-76C22BA101B4")
        },
        new Menuitem
        {
            Id = new Guid ("1DA1E472-625D-4A5B-8538-D2DC39EF4FEA"),
            Day = DateTime.Now.AddDays(-2),
            FoodId = new Guid("31801C4C-CBD2-45D6-AF8A-3B21210562D9"),
            IsActive = true,
            MenuId = new Guid("3C8AF117-B57B-461B-9F4E-76C22BA101B4")
        },
        new Menuitem
        {
            Id = new Guid ("2DA1E472-625D-4A5B-8538-D2DC39EF4FEA"),
            Day = DateTime.Now.AddDays(5),
            FoodId = new Guid("31801C4C-CBD2-45D6-AF8A-3B21210562D9"),
            IsActive = false,
            MenuId = new Guid("3C8AF117-B57B-461B-9F4E-76C22BA101B4")
        }
    };

    public static List<KitchenClube.Models.UserMenuitemSelection> UserMenuItemSelections = new List<KitchenClube.Models.UserMenuitemSelection>()
    {
        new KitchenClube.Models.UserMenuitemSelection()
        {
            Id = new Guid("D75AA3B6-B166-4DCF-A1EE-9E84C2BDA0D0"),
            MenuitemId = new Guid("E5D5421F-D750-43FE-89D9-77784D3660DD"),
            UserId = new Guid("2EA3E5EB-CBCB-4A67-B868-61B96F9A9D60"),
            Vote = UserVote.Yes
        },
        new KitchenClube.Models.UserMenuitemSelection()
        {
            Id = new Guid("E75AA3B6-B166-4DCF-A1EE-9E84C2BDA0D0"),
            MenuitemId = new Guid("E5D5421F-D750-43FE-89D9-77784D3660DD"),
            UserId = new Guid("3EA3E5EB-CBCB-4A67-B868-61B96F9A9D60"),
            Vote = UserVote.Yes
        },
        new KitchenClube.Models.UserMenuitemSelection()
        {
            Id = new Guid("A75AA3B5-B166-4DCF-A1EE-9E84C2BDA0D0"),
            MenuitemId = new Guid("1DA1E472-625D-4A5B-8538-D2DC39EF4FEA"),
            UserId = new Guid("3EA3E5EB-CBCB-4A67-B868-61B96F9A9D60"),
            Vote = UserVote.Yes
        },
    };
}
