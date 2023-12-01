using Autofac;
using Microsoft.Extensions.Configuration;
using Persistence.Client;

namespace ConsoleTesting.Ioc
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
            var connection = "Data Source=DESKTOP-VLU1LE8\\SQLEXPRESS; Initial Catalog=Testing; Persist Security Info=True; User ID=sa; Password=sa1234; Connection Timeout=35";

            builder.Register((context, parameters) =>
            {
                return new SqlServerClient(connection);
            })
            .As<SqlServerClient>().SingleInstance();

            base.Load(builder);
        }
    }
}
