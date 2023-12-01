using Application.Services;
using Autofac;
using Microsoft.Extensions.Configuration;

namespace WebAppTest.Ioc
{
    public class ServicesModule : Module
    {
        private IConfiguration _configuration;
        public ServicesModule(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TestingSqlService>().AsSelf().InstancePerDependency();
            builder.RegisterType<TestingQueueService>().AsSelf().InstancePerDependency();
            base.Load(builder);
        }
    }
}
