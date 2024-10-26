using Base;
using Weather.BL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
BaseStartupConfig.ConfigureService(ref builder);

// add injection
builder.Services.AddScoped<IWeatherService, WeatherService>();
builder.Services.AddScoped<ILocationService, LocationService>();

var app = builder.Build();

BaseStartupConfig.ConfigureApp(ref app);

app.Run();