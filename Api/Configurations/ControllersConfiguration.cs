using System;
using Microsoft.OpenApi.Models;

namespace Api.Configurations;

public static class ControllersConfiguration
{
    public static IServiceCollection AddAndConfigureControllers(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddDocumentation();
        services.AddMvc();

        return services;
    }
    private static IServiceCollection AddDocumentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "POCPACS",
                    Description = "PocPacs API Swagger surface",
                    Contact = new OpenApiContact { Name = "John Doe", Email = "contato@contato.com.br", Url = new Uri("https://www.google.com.br") },
                    License = new OpenApiLicense { Name = "Proprietary", Url = new Uri("https://www.google.com.br") }
                });

                c.CustomSchemaIds(x => x.FullName);

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                   Description = "Authorization: Bearer {token}",
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

        return services;
    }
}
