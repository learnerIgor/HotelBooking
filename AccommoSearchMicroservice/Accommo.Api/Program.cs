using Accommo.Persistence;
using Accommo.Application;
using Accommo.Api;
using Accommo.Api.Middlewares;
using Serilog;
using Serilog.Events;
using System.Reflection;
using Accommo.DistributedCache;
using Accommo.Api.gRPC;

try
{
    const string appPrefix = "Accommo";
    const string version = "v1";
    const string appName = "Accommodation search API v1";

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, lc) => lc
#if DEBUG
           .WriteTo.Console()
#endif
           .WriteTo.File($"{builder.Configuration["Logging:LogsFolder"]}/Information-.txt", LogEventLevel.Information,
               rollingInterval: RollingInterval.Day, retainedFileCountLimit: 3, buffered: true)
           .WriteTo.File($"{builder.Configuration["Logging:LogsFolder"]}/Warning-.txt", LogEventLevel.Warning,
               rollingInterval: RollingInterval.Day, retainedFileCountLimit: 14, buffered: true)
           .WriteTo.File($"{builder.Configuration["Logging:LogsFolder"]}/Error-.txt", LogEventLevel.Error,
               rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30, buffered: true));

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services
        .AddSwaggerWidthJwtAuth(Assembly.GetExecutingAssembly(), appName, version, appName)
        .AddCoreApiServices()
        .AddPersistenceServices(builder.Configuration)
        .AddAuthApiServices(builder.Configuration)
        .AddAccommoApplicationServices()
        .AddDistributedCacheServices(builder.Configuration)
        .AddAllCors()
        .AddHttpClient()
        .AddGrpc();

    var app = builder.Build();

    app.RunDbMigrations();

    app.UseExceptionHandlerMiddleware()
            .UseSwagger(c => { c.RouteTemplate = appPrefix + "/swagger/{documentname}/swagger.json"; })
            .UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/" + appPrefix + $"/swagger/{version}/swagger.json", version);
                options.RoutePrefix = appPrefix + "/swagger";
            })
            .UseCors(CorsPolicy.AllowAll);

    app.MapGrpcService<GRPCRoomsService>();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    var appSettingsFile = $"{Directory.GetCurrentDirectory()}/appsettings.json";
    var settingsJson = File.ReadAllText(appSettingsFile);
    var appSettings = System.Text.Json.JsonDocument.Parse(settingsJson);
    var logsPath = appSettings.RootElement.GetProperty("Logging").GetProperty("LogsFolder").GetString();
    var logger = new LoggerConfiguration()
        .WriteTo.File($"{logsPath}/Log-Run-Error-.txt", LogEventLevel.Error, rollingInterval: RollingInterval.Hour,
            retainedFileCountLimit: 30)
        .CreateLogger();
    logger.Fatal(ex.Message, ex);
}

public partial class Program { }
