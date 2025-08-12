
using Api.Configurations;
using FellowOakDicom;
using FellowOakDicom.Imaging.NativeCodec;
using Shared.Core.Configuration;

namespace Api;

public class Startup
{
    public IConfiguration _configuration { get; private set; }

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddConfiguration(_configuration);
        services.AddAndConfigureControllers();
        services.AddDatabase();
        services.AddDependencies();

        // fo-dicom
        new DicomSetupBuilder()
            .RegisterServices(s => s.AddFellowOakDicom().AddTranscoderManager<NativeTranscoderManager>())
            .SkipValidation()
            .Build();

        services.AddFellowOakDicom();

        var healthChecksBuilder = services.AddHealthChecks();

        healthChecksBuilder.AddNpgSql(Configuration.Database.ConnectionString);
        //switch (Configuration.Database.Provider)
        //{
        //    case "postgres":
        //        healthChecksBuilder.AddNpgSql(
        //            connectionString: Configuration.Database.ConnectionString
        //        );
        //        break;
        //    default:
        //        healthChecksBuilder.AddOracle(
        //            connectionString: Configuration.Database.ConnectionString
        //        );
        //        break;
        //}

    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) //, IDbConnection connection
    {
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCors(options => options
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // fo-dicom
        //DicomSetupBuilder.UseServiceProvider(app.Services);

        app.UseHealthChecks("/api/health-check");
    }
}
