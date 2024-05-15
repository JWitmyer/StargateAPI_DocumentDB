using Azure.Data.Tables;

using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StargateAPI_BL;
using StargateAPI_DAL;




[assembly: FunctionsStartup(typeof(StargateAPI.Startup))]
namespace StargateAPI
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {

            //Will leave this blank for now could connect it to app insights or some other logging mechanism Logger spec not needed atm
            
            var config = BuildConfiguration(builder.GetContext().ApplicationRootPath);

            builder.Services.AddAppConfiguration(config);

            // Inject Mediatrs 
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<CreateAstronautCommand>();
            });

            //TODO refine this injection class
            builder.Services.AddScoped<IRepository<Astronaut>, TableClientRepository<Astronaut>>();
            builder.Services.AddScoped<IRepository<AstronautDuty>, TableClientRepository<AstronautDuty>>();

            var tableClientConnectionString = config["Stargate:TableConnStr"];

            builder.Services.AddScoped(_ => new TableServiceClient(tableClientConnectionString));

            ////This is not ideal. Was having issues with the config DI 
            //builder.Services.AddSingleton(new WordlyerOptions
            //{
            //    DictonaryKey = config["WordlyerOptions:DictonaryKey"]
            //});

        }


        private IConfiguration BuildConfiguration(string applicationRootPath)
        {
            var config =
                new ConfigurationBuilder()
                    .SetBasePath(applicationRootPath)
                    .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)                    
                    .AddEnvironmentVariables()
                    .Build();

            return config;
        }
    }

    internal static class ConfigurationServiceCollectionExtensions
    {
        public static IServiceCollection AddAppConfiguration(this IServiceCollection services, IConfiguration config)
        {
            
            return services;
        }
    }
}
