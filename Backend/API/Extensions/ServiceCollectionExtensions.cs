using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using Topshiriq.Application.Services.Authentication;
using Topshiriq.Application.Services.Users;
using Topshiriq.Domain.Enums;
using Topshiriq.Infrastructure.Authentication;
using Topshiriq.Infrastructure.Contexts;
using Topshiriq.Infrastructure.Repositories.Users;
using Topshiriq.Application.Services.Sciences;
using Topshiriq.Infrastructure.Repositories;
using Topshiriq.Infrastructure.Repositories.Sciences;
using Topshiriq.Infrastructure.Repositories.UserOfScience;
using Topshiriq.Application.Services.UserOfSciences;

namespace Topshiriq.Api.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDbContexts(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlServer");

            services.Configure<JwtOption>(configuration
                .GetSection("JwtSettings"));

            services.AddSwaggerService();

            services.AddDbContextPool<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("Temploy");

                /*options.UseSqlServer(connectionString, sqlServerOptions =>
                {
                    sqlServerOptions.EnableRetryOnFailure();
                });*/
            });

            return services;
        }

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUserOfScienceService, UserOfScienceService>();
            services.AddScoped<IUserService, UserService>();
        services.AddScoped<IScienceService, ScienceService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddHttpContextAccessor();

            return services;
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUserOfScienceRepository, UserOfScienceRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IScienceRepository, ScienceRepository>();
            services.AddTransient<IJwtTokenHandler, JwtTokenHandler>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();

            return services;
        }

        public static IServiceCollection AddAuthentication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("UserPolicy", authorizationPolicyBuilder =>
                {
                    authorizationPolicyBuilder.RequireRole(
                        UserRole.Student.ToString(),
                        UserRole.Teacher.ToString(),
                        UserRole.Admin.ToString());
                });
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]!)),
                    ClockSkew = TimeSpan.Zero
                };
            });

            return services;
        }

        private static void AddSwaggerService(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Task.Api", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description =
                        "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
            });
        }

/*    public static WebApplicationBuilder AddLogging(
        this WebApplicationBuilder builder,
        IConfiguration configuration)
    {
        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .CreateLogger();

        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog(logger);

        return builder;
    }*/
}