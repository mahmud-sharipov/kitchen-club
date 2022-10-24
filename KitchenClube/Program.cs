using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

namespace KitchenClube;

public class Program
{
    public static void Main(string[] args)
    {
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

            builder.AddLogger();

            builder.Services.AddIdentity<User, Role>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<KitchenClubContext>();

            builder.Services.AddingServices();

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

            builder.Services.AddControllers().AddFluentValidation(x =>
            {
                x.ImplicitlyValidateChildProperties = true;
                x.ImplicitlyValidateRootCollectionElements = true;
                x.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            })
                .AddOData(opt => opt.Select().Filter().Count().OrderBy().Expand());            

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
    }
}


