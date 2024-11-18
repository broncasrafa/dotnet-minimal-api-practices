using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Api.Data.Context;
using Api.Data.Repository.Implementation;
using Api.Data.Repository.Interface;
using Api.Services.Implementation;
using Api.Services.Interface;
using Api.Middlewares;
using Api.Validations;
using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;


namespace Api.DependencyInjection;

public static class ServiceCollectionsRegistration
{
    public static void AddSwaggerOpenAPI(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Minimal Api Practices",
                Version = "v1",
                Description = "Demonstração dos recursos disponíveis na api",
                Contact = new OpenApiContact
                {
                    Name = "Rafael Francisco",
                    Email = "rsfrancisco.applications@gmail.com",
                    Url = new Uri("https://github.com/broncasrafa")
                }
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Description = "Informe seu token bearer para acessar os recursos da API da seguinte forma: Bearer {your token here}",
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer",
                        },
                        Scheme = "Bearer",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    }, new List<string>()
                },
            });

            //Set the comments path for the Swagger JSON and UI.
            string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        });
    }
    public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(c =>
        {
            c.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            c.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"])),
                ValidIssuer = configuration["JWTSettings:Issuer"],
                ValidateIssuer = true,
                ValidateAudience = false
            };

            options.Events = new JwtBearerEvents
            {
                OnChallenge = async context =>
                {
                    context.HandleResponse();
                    await AuthErrorHandler.HandleAuthError(context.HttpContext, StatusCodes.Status401Unauthorized);
                },
                OnForbidden = async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    context.Response.ContentType = "application/json";
                    await AuthErrorHandler.HandleAuthError(context.HttpContext, StatusCodes.Status403Forbidden);
                }
            };
        });
    }

    public static void AddControllerAndJsonConfigurations(this IServiceCollection services)
    {
        services
            .AddControllers()
            .AddNewtonsoftJson(o => // Microsoft.AspNetCore.Mvc.NewtonsoftJson
            {
                o.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                o.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
                o.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });
    }

    public static void AddFluentValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CouponCreateValidation>();
    }




    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(configuration.GetConnectionString("DatabaseConnection"),
                    new MySqlServerVersion(new Version(8, 0, 23)),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
                          .EnableSensitiveDataLogging()
                          .EnableDetailedErrors());

        return services;
    }
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        //services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddTransient<ICouponRepository, CouponRepository>();
        services.AddTransient<IAuthRepository, AuthRepository>();


        return services;
    }
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICouponService, CouponService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        return services;
    }
}
