using Autofac;
using Autofac.Extensions.DependencyInjection;
using ConsoleTesting.Ioc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using ConsoleTesting.Serilog;

namespace ConsoleTesting
{
    class ConsoleTesting
    {
        public static IContainer Container { get; set; }
        private static IConfiguration Configuration { get; set; }

        internal static async Task Main(string[] args)
        {
            Init();
            var processor = Container.Resolve<TestingProcess>();
            await processor.TestingInit();
        }
        private static void Init()
        {
            Configuration = buildConfiguration();
            Container = BuildContainerBuilder();
            ConfigureLogging();
        }
        private static IConfiguration buildConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables()
            .Build();
        }
        private static IContainer BuildContainerBuilder()
        {
            var services = new ServiceCollection();
            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterModule(new ModulesAdministrator(Configuration));
            return builder.Build();
        }
        private static void ConfigureLogging()
        {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel
            .ControlledBy(new EnvironmentVariableLoggingLevelSwitch(Configuration.GetSection("???")["LoggingLevel"] ?? "Error"))
            .Enrich.With(new ThreadIdEnricher())
            .WriteTo.Console(outputTemplate: "{Timestamp:HH:mm} [{Level}] ({ThreadId}) {Message}{NewLine}{Exception}")
            .CreateLogger();
        }
    }
}