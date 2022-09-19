namespace KitchenClube.Extentions;

public static class VersioningExtention
{
    public static IServiceCollection AddAndConfigureApiVersioning(this IServiceCollection services)
    {
        services.ConfigureOptions<SwaggerConfigureOptions>();

        services.AddApiVersioning(o =>
        {
            o.AssumeDefaultVersionWhenUnspecified = true;
            o.DefaultApiVersion = new ApiVersion(1, 0);
            o.ReportApiVersions = true;
        });
        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }
}
