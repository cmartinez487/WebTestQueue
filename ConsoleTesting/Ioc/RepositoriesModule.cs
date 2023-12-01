using Autofac;
using Microsoft.Extensions.Configuration;
using Persistence.Repositories;


namespace ConsoleTesting.Ioc
{
    internal class RepositoriesModule : Module
    {
        private IConfiguration _configuration;
        public RepositoriesModule(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SqlTestingRepository>().AsSelf().InstancePerDependency();
            base.Load(builder);
        }

    }
}
