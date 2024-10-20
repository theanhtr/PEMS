using Base;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
BaseStartupConfig.ConfigureService(ref builder);

var app = builder.Build();

BaseStartupConfig.ConfigureApp(ref app);

app.Run();