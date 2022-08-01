using KitchenClube.Data;

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


    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
{
    if (app.Environment.IsDevelopment()) {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    //app.UseAuthorization();

    app.MapControllers();

    using var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope();
    using var context = serviceScope.ServiceProvider.GetService<KitchenClubContext>();
    context.Database.Migrate();
}

app.Run();
