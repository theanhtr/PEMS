using Base;
using Predict.BL;
using Predict.DL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
BaseStartupConfig.ConfigureService(ref builder);

// add injection
builder.Services.AddScoped<IPredictService, PredictService>();
builder.Services.AddScoped<IPredictRepository, PredictRepository>();

var app = builder.Build();

BaseStartupConfig.ConfigureApp(ref app);

app.Run();