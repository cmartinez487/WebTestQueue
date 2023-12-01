using Autofac;
using Persistence.Client;

namespace WebAppTest.Ioc
{
    internal class ClientsModule : Module
    {
        private IConfiguration _configuration;
        public ClientsModule(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var connection = _configuration.GetValue<string>("sql:connection");
            builder.Register((context, parameters) =>
            {
                return new SqlServerClient(connection);
            })
            .As<SqlServerClient>().SingleInstance();

            base.Load(builder);
        }
    }
}
