using Autofac;
using Microsoft.Extensions.Configuration;

namespace WebAppTest.Ioc
{
    public class ModulesAdministrator:Module
    {
        private readonly IConfiguration _configuration;
        public ModulesAdministrator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new ClientsModule(_configuration));
            builder.RegisterModule(new RepositoriesModule(_configuration));
            builder.RegisterModule(new ServicesModule(_configuration));
        }
    }
}
