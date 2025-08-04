using System;
using Domain.UnitOfWork;
using Infra.ConnectionContext;
using Infra.UnitOfWork;
using Shared.Core.Configuration;

namespace Api.Configurations;

public static class BuilderExtensions
{
    public static void AddConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        Configuration.Database.ConnectionString =
            Environment.GetEnvironmentVariable("PACS_CONNECTION_STRING") ?? string.Empty;

        Configuration.Database.Provider =
            Environment.GetEnvironmentVariable("PACS_CONNECTION_PROVIDER") ?? string.Empty;
    }

    public static void AddDatabase(this IServiceCollection service)
    {
        service.AddScoped<IConnectionProvider, ConnectionProvider>();
        service.AddTransient<IUnitOfWork, UnitOfWork>();
        //if (Configuration.Database.Provider.ToLower() == "oracle")
        //{
        //    builder.Services.AddScoped<OracleConnectionContext, OracleConnectionContext>();
        //}
        //else
        //{
        //    builder.Services.AddScoped<PostgresConnectionContext, PostgresConnectionContext>();
        //}
    }
}
