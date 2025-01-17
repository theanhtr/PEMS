﻿using Microsoft.AspNetCore.Mvc;
using System.Net;
using PEMS.Infrastructure;
using PEMS.Domain;
using PEMS.Application;
using PEMS;
using System.Globalization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = actionContext =>
        {
            var modelState = actionContext.ModelState;
            var errors = new Dictionary<string, string>();

            foreach (var entry in modelState)
            {
                var key = entry.Key;
                var valueErrors = entry.Value.Errors.Select(error => error.ErrorMessage);
                var errorMessage = string.Join(", ", valueErrors);

                errors[key] = errorMessage;
            }

            return new BadRequestObjectResult(new BaseException
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                DevMessage = Resource.Validate_User_Input_Error,
                UserMessage = Resource.Validate_User_Input_Error,
                TraceId = "",
                MoreInfo = "",
                Data = errors,
            });
        };
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.Converters.Add(new LocalTimeZoneConverter());
    });

builder.Services.AddDistributedMemoryCache();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSession(options =>
{
    //thời gian chờ trước khi bị hủy bỏ
    options.IdleTimeout = TimeSpan.FromMinutes(AppConst.ExpiredTime);
});

var dbConnectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
var redisConnectionString = builder.Configuration.GetConnectionString("RedisConnection");

// Thêm Hangfire để xử lý background job
//builder.Services.AddHangfire(config => config
//    .UseSimpleAssemblyNameTypeSerializer()
//    .UseRecommendedSerializerSettings()
//    .UseStorage(
//        new MySqlStorage(
//            dbConnectionString,
//                new MySqlStorageOptions
//                {
//                    QueuePollInterval = TimeSpan.FromSeconds(10), // kiểm tra hàng đợi thực thi
//                    JobExpirationCheckInterval = TimeSpan.FromHours(1), // kiểm tra công việc hết hạn
//                    CountersAggregateInterval = TimeSpan.FromMinutes(5), // cập nahạt thống kê
//                    PrepareSchemaIfNecessary = true, // tự động chuẩn bị db
//                    DashboardJobListLimit = 25000, // giới hạn số lượng công việc
//                    TransactionTimeout = TimeSpan.FromMinutes(1), // thời gian tối đa để thực hiện
//                    TablesPrefix = "Hangfire", //phân biệt bảng hangfire trong database
//                }
//            )
//        )
//    );

//builder.Services.AddHangfireServer();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<ICacheService>(option => new CacheService(redisConnectionString));

// thêm Localization cho đa ngôn ngữ
builder.Services.AddLocalization();
var localizationOptions = new RequestLocalizationOptions();

var supportedCultures = new[]
{
    new CultureInfo("en"),
    new CultureInfo("vi"),
};


// thêm xác thực bằng jwt
var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>(),
        ValidAudience = builder.Configuration.GetSection("Jwt:Audience").Get<string>(),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});


localizationOptions.SupportedCultures = supportedCultures;
localizationOptions.SupportedUICultures = supportedCultures;
localizationOptions.SetDefaultCulture("en");
localizationOptions.ApplyCurrentCultureToResponseHeaders = true;

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUnitOfWork>(option => new UnitOfWork(dbConnectionString));

builder.Services.AddTransient<IScheduleService, ScheduleService>();

builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmployeeValidate, EmployeeValidate>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IExcelService<EmployeeExcelDto, EmployeeLayoutDto>, EmployeeExcelService>();

builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IDepartmentValidate, DepartmentValidate>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

builder.Services.AddScoped<IEmployeeLayoutService, EmployeeLayoutService>();
builder.Services.AddScoped<IEmployeeLayoutValidate, EmployeeLayoutValidate>();
builder.Services.AddScoped<IEmployeeLayoutRepository, EmployeeLayoutRepository>();

builder.Services.AddScoped<IPredictService, PredictService>();
builder.Services.AddScoped<IPredictValidate, PredictValidate>();
builder.Services.AddScoped<IPredictRepository, PredictRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserValidate, UserValidate>();


builder.Services.AddScoped<IExcelWorker<EmployeeDto, EmployeeExcelDto, EmployeeLayoutDto>, EmployeeExcelWorker>();

builder.Services.AddScoped<IFileService, FileService>();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddSingleton<ITokenService, TokenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseRouting();

var configuration = (IConfiguration)(app.Services.GetService(typeof(IConfiguration)));
app.UseCors(x =>
{
    string allowedOrigins = configuration["AppSettings:AllowedOrigins"];
    if (string.IsNullOrWhiteSpace(allowedOrigins))
    {
        x.SetIsOriginAllowed(origin => true);
    }
    else
    {
        x.SetIsOriginAllowedToAllowWildcardSubdomains();
        x.WithOrigins(allowedOrigins.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries).ToList().ToArray());
    }
    x.AllowAnyHeader()
     .AllowCredentials()
     .AllowAnyMethod()
     .SetPreflightMaxAge(TimeSpan.FromMinutes(10))
     .WithExposedHeaders("Content-Disposition");
});

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

//app.UseHangfireDashboard();
//app.MapHangfireDashboard();

//var rootPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location.Substring(0, Assembly.GetEntryAssembly().Location.IndexOf("bin\\")));

//var clientFolderStore = Path.Combine(rootPath, AppConst.ClientFolderStoreName);

//RecurringJob.AddOrUpdate<IScheduleService>(scheduleService => scheduleService.ClearFiles(clientFolderStore), AppConst.CronJobTime);

// thêm localization
app.UseRequestLocalization(localizationOptions);
app.UseMiddleware<LocalizationMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();

app.UseEndpoints(x => x.MapControllers());
app.Run();