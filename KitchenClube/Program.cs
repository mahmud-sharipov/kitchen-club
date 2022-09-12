using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
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

    builder.Services.AddIdentity<User, Role>(options => {
        options.SignIn.RequireConfirmedAccount = true;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireDigit = false;
        options.Password.RequireUppercase = false;
    }).AddEntityFrameworkStores<KitchenClubContext>();

    builder.Services.AddScoped<KitchenClube.Services.V1.IFoodService, KitchenClube.Services.V1.FoodService>();
    builder.Services.AddScoped<KitchenClube.Services.V1.IMenuService, KitchenClube.Services.V1.MenuService>();
    builder.Services
        .AddScoped<KitchenClube.Services.V1.IMenuItemService, KitchenClube.Services.V1.MenuItemService>();
    builder.Services
        .AddScoped<KitchenClube.Services.V1.IUserMenuItemSelectionService, KitchenClube.Services.V1.UserMenuItemSelectionService>();
    builder.Services.AddScoped<KitchenClube.Services.V1.IUserService, KitchenClube.Services.V1.UserService>();
    builder.Services.AddScoped<KitchenClube.Services.V1.IAuthService, KitchenClube.Services.V1.AuthService>();
    builder.Services.AddScoped<KitchenClube.Services.V1.IRoleService, KitchenClube.Services.V1.RoleService>();


    builder.Services.AddScoped<KitchenClube.Services.V2.IFoodService, KitchenClube.Services.V2.FoodService>();
    builder.Services.AddScoped<KitchenClube.Services.V2.IMenuService, KitchenClube.Services.V2.MenuService>();
    builder.Services
        .AddScoped<KitchenClube.Services.V2.IMenuItemService, KitchenClube.Services.V2.MenuItemService>();
    builder.Services
        .AddScoped<KitchenClube.Services.V2.IUserMenuItemSelectionService, KitchenClube.Services.V2.UserMenuItemSelectionService>();
    builder.Services.AddScoped<KitchenClube.Services.V2.IUserService, KitchenClube.Services.V2.UserService>();
    builder.Services.AddScoped<KitchenClube.Services.V2.IAuthService, KitchenClube.Services.V2.AuthService>();
    builder.Services.AddScoped<KitchenClube.Services.V2.IRoleService, KitchenClube.Services.V2.RoleService>();
    builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    //builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigureOptions>();

    builder.Services.AddHttpContextAccessor();
    builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
    
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

    builder.Services.AddAndConfigureApiVersioning();
    builder.Services.AddEndpointsApiExplorer();

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
    builder.Services.AddAuthorization();
}

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

{
    if (app.Environment.IsDevelopment())
    {
        app.AddSwagger();
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
