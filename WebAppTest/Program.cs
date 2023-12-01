using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;
using WebAppTest.Ioc;

namespace WebAppTest
{ 
    class WebAppTest
    {
        public static IContainer Container { get; set; }
        private static IConfiguration Configuration { get; set; }

        internal static async Task Main(string[] args)
        {
            Init();
            var builder = WebApplication.CreateBuilder(args);
            Webinit(builder);
        }
        private static void Webinit(WebApplicationBuilder builder)
        {
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
            {
                builder.RegisterModule(new ModulesAdministrator(Configuration));
            });
            builder.Services.AddControllersWithViews();
            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
        private static void Init()
        {
            Configuration = buildConfiguration();
            
        }
        private static IConfiguration buildConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables()
            .Build();
        }
    }
}
    
