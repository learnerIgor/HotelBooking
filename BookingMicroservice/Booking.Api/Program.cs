using Booking.Persistence;
using Booking.Application;
using Serilog;
using Serilog.Events;
using Booking.Api.Middlewares;
using Booking.Api;
using System.Reflection;
using Booking.ExternalProviders;
using Booking.DistributedCache;
using Booking.Exchanger;

try
{
    const string appPrefix = "Booking";
    const string version = "v1";
    const string appName = "Booking API v1";

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
        .AddSwaggerWithJwtAuth(Assembly.GetExecutingAssembly(), appName, version, appName)
        .AddCoreApiServices()
        .AddExternalProviders()
        .AddAuthApiServices(builder.Configuration)
        .AddPersistenceServices(builder.Configuration)
        .AddAllCors()
        .AddBookingApplicationServices()
        .AddDistributedCacheServices(builder.Configuration)
        .AddHttpClient()
        .AddExchangeProviders();

    var app = builder.Build();

    app.RunDbMigrations();

    app.UseExceptionHandlerMiddleware()
        .UseAuthExceptionHandlerMiddleware()
        .UseSwagger(c => { c.RouteTemplate = appPrefix + "/swagger/{documentname}/swagger.json"; })
        .UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/" + appPrefix + $"/swagger/{version}/swagger.json", version);
            options.RoutePrefix = appPrefix + "/swagger";
        })
        .UseAuthentication()
        .UseAuthorization()
        .UseCors(CorsPolicy.AllowAll);

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
