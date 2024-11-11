using Base.Controller;
using Base.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Base.DL;
using Base.BL;

namespace Base
{
    public static class BaseStartupConfig
    {
        public static void ConfigureService(ref WebApplicationBuilder builder)
        {
            var configuration = builder.Configuration;
            // Add services to the container.
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
                options.IdleTimeout = TimeSpan.FromMinutes(15);
            });

            // thêm Localization cho đa ngôn ngữ
            builder.Services.AddLocalization();

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
                    ValidIssuer = configuration.GetSection("Jwt:Issuer").Get<string>(),
                    ValidAudience = configuration.GetSection("Jwt:Audience").Get<string>(),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                };
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var dbConnectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
            builder.Services.AddScoped<IUnitOfWork>(option => new UnitOfWork(dbConnectionString));

            builder.Services.AddSingleton<ITokenService, TokenService>();
        }
        public static void ConfigureApp(ref WebApplication app)
        {
            var configuration = (IConfiguration)(app.Services.GetService(typeof(IConfiguration)));

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            //app.UseHttpsRedirection();

            // add basePath
            Console.WriteLine("Start application with base path: " + configuration?.GetValue<string>("APPLICATION_BASE_PATH"));
            if (configuration?.GetValue<string>("APPLICATION_BASE_PATH") != null)
            {
                app.UsePathBase(configuration?.GetValue<string>("APPLICATION_BASE_PATH"));
            }

            app.UseRouting();

            app.UseCors(x =>
            {
                string allowedOrigins = configuration["AppSettings:AllowedOrigins"];
                Console.WriteLine("Start application with allowedOrigins: " + allowedOrigins);
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

            //app.UseSession();

            app.UseMiddleware<LocalizationMiddleware>();
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseEndpoints(x => x.MapControllers());
        }
    }
}
