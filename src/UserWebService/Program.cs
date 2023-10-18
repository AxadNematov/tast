using System.Configuration;
using System.Reflection;
using GeneralApplication.Extensions;
using GeneralDomain.Configs;
using GeneralDomain.Extensions;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using UserApplication;
using UserInfrastructure;
using ConfigurationManager = Microsoft.Extensions.Configuration.ConfigurationManager;

public class Programm
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        RegisterAppConfig(builder.Configuration);
        builder.Services.ApplicationBuild();
        builder.Services.InfrastructureBuild();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.OperationFilter<SwaggerSkipProperty>();
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
          Enter 'Bearer' [space] and then your token in the text input below.
          \r\n\r\nExample: 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
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
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });
        });
        
        builder.Services.AddHttpContextAccessor();

        var app = builder.Build();
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        if (app.Services.GetService<IHttpContextAccessor>() != null)
            HttpContextHelper.Accessor = app.Services.GetRequiredService<IHttpContextAccessor>();

        app.Run();
    }

    private static void RegisterAppConfig(ConfigurationManager configuration)
    {
        Configs.DatabaseConnection = configuration["ConnectionStrings:PostgresConnectionString"];
        Configs.FilePath = configuration["ConnectionStrings:FilePath"];
    }
}