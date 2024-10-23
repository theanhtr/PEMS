using Base;
using Report.BL;
using Report.DL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
BaseStartupConfig.ConfigureService(ref builder);

// add injection
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();

var app = builder.Build();

BaseStartupConfig.ConfigureApp(ref app);

app.Run();