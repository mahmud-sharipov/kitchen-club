using Serilog;
using Serilog.Events;

namespace KitchenClube.Extentions;

public static class LoggerExtension
{
    public static WebApplicationBuilder AddLogger(this WebApplicationBuilder builder)
    {
        const string logTemplate = @"{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] 
                                    [{SourceContext:l}] {Message:lj}{NewLine}{Exception}";
        var logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.File("logs/info/log-.txt", LogEventLevel.Information, logTemplate, rollingInterval: RollingInterval.Day)
            .WriteTo.File("logs/warn/log-.txt", LogEventLevel.Warning, logTemplate, rollingInterval: RollingInterval.Day)
            .WriteTo.File("logs/err/log-.txt", LogEventLevel.Error, logTemplate, rollingInterval: RollingInterval.Day)
            .WriteTo.File("logs/fat/log-.txt", LogEventLevel.Fatal, logTemplate, rollingInterval: RollingInterval.Day)
            .WriteTo.MySQL(builder.Configuration.GetSection("ConnectionStrings:mysql").Value)
        .CreateLogger();

        builder.Logging.AddSerilog(logger);

        return builder;
    }
}
