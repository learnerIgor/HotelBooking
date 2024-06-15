using Accommo.Domain.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using Accommo.Api.Services;
using Accommo.Application.Abstractions.Service;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json.Serialization;
using Accommo.Api.JsonSerializer;

namespace Accommo.Api
{
    /// <summary>
    /// Dependency Injection accommodation search microservice 
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Add authorization services
        /// </summary>
        public static IServiceCollection AddAuthApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthorization()
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
                    };
                });

            var authBuilder = services.AddAuthorizationBuilder();
            authBuilder
                .AddPolicy(AuthorizationPoliciesEnum.AdminGreetings.ToString(), policy =>
                    policy.RequireRole(ApplicationUserRolesEnum.Admin.ToString()));

            return services.AddTransient<ICurrentUserService, CurrentUserService>();
        }

        /// <summary>
        /// Add Swagger with Jwt authentication
        /// </summary>
        public static IServiceCollection AddSwaggerWidthJwtAuth(
            this IServiceCollection services,
            Assembly apiAssembly,
            string appName,
            string version,
            string description)
        {
            return services.AddEndpointsApiExplorer()
                .AddSwaggerGen(options =>
                {
                    options.SwaggerDoc(version, new OpenApiInfo
                    {
                        Version = version,
                        Title = appName,
                        Description = description
                    });

                    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = """
                                  JWT Authorization header using the Bearer scheme. \r\n\r\n
                                                        Enter 'Bearer' [space] and then your token in the text input below.
                                                        \r\n\r\nExample: 'Bearer 12345abcdef'
                                  """,
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    });

                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                    });

                    // using System.Reflection;
                    var xmlFilename = $"{apiAssembly.GetName().Name}.xml";
                    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
                });
        }

        /// <summary>
        /// Add core services
        /// </summary>
        public static IServiceCollection AddCoreApiServices(this IServiceCollection services)
        {
            return services
                .AddHttpContextAccessor()
                .Configure<JsonOptions>(options =>
                {
                    options.SerializerOptions.Converters.Add(new TrimmingConverter());
                    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                })
                .AddResponseCompression(options => { options.EnableForHttps = true; });
        }

        /// <summary>
        /// Add all cors
        /// </summary>
        public static IServiceCollection AddAllCors(this IServiceCollection services)
        {
            return services
                .AddCors(options =>
                {
                    options.AddPolicy(CorsPolicy.AllowAll, policy =>
                    {
                        policy.AllowAnyHeader();
                        policy.AllowAnyMethod();
                        policy.AllowAnyOrigin();
                        policy.WithExposedHeaders("*");
                    });
                });
        }
    }
}
