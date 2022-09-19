using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace KitchenClube.Extentions;

public static class SwaggerExtention
{
    public static WebApplication AddSwagger(this WebApplication app)
    {
        app.UseSwagger(o =>
        {
            o.PreSerializeFilters.Add((swagger, req) =>
            {
                swagger.Servers = new List<OpenApiServer>()
                {
                    new OpenApiServer(){Url = $"https://{req.Host}"}
                };
            });
        });
        app.UseSwaggerUI(o =>
        {
            var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
            foreach (var desc in provider.ApiVersionDescriptions)
            {
                o.SwaggerEndpoint($"../swagger/{desc.GroupName}/swagger.json", desc.ApiVersion.ToString());
                o.DefaultModelExpandDepth(-1);
                o.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
            }
        });

        return app;
    }
}
