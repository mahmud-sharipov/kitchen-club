using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
{
    //builder.Services.AddScoped<KitchenClubContext, KitchenClubSqlServerContext>();
    //builder.Services.AddDbContext<KitchenClubSqlServerContext>(options =>
    //{
    //    options.UseSqlServer(builder.Configuration.GetConnectionString("sqlserver"));
    //    options.UseLazyLoadingProxies();
    //});

    builder.Services.AddScoped<KitchenClubContext, KitchenClubMySqlContext>();
    builder.Services.AddDbContext<KitchenClubMySqlContext>(options =>
    {
        options.UseMySql(builder.Configuration.GetConnectionString("mysql"), ServerVersion.Parse("8.0.0-mysql"));
        options.UseLazyLoadingProxies();
    });

    builder.Services.AddScoped<IFoodService, FoodService>();
    builder.Services.AddScoped<IMenuService, MenuService>();
    builder.Services.AddScoped<IMenuItemService, MenuItemService>();
    builder.Services.AddScoped<IUserMenuItemSelectionService, UserMenuItemSelectionService>();
    builder.Services.AddScoped<IUserService, UserService>();

    builder.Services.AddScoped<IRoleService, RoleService>();
    builder.Services.AddScoped<IAuthService, AuthService>();

    builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

    builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    builder.Services.AddHttpContextAccessor();

    builder.Services.AddControllers().AddFluentValidation(x =>
    {
        x.ImplicitlyValidateChildProperties = true;
        x.ImplicitlyValidateRootCollectionElements = true;
        x.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    });


    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            Description = "Description",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        });

        options.OperationFilter<SecurityRequirementsOperationFilter>();
    });

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true
            };
        });
    builder.Services.AddAuthorization(opts =>
    {
        opts.AddPolicy("Admin", policy =>
        {
            policy.RequireClaim(ClaimTypes.Role, "Admin");
        });

        opts.AddPolicy("User", policy =>
        {
            policy.RequireClaim(ClaimTypes.Role, "User");
        });

        //TODO:Remove
        opts.AddPolicy("All", policy =>
        {
            policy.RequireClaim(ClaimTypes.Role, "Admin", "User");
        });
    });
}



var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    using var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope();
    using var context = serviceScope.ServiceProvider.GetService<KitchenClubContext>();
    context.Database.Migrate();
}

app.Run();
