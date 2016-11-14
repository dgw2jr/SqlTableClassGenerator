using Autofac;

namespace SQLTableClassGenerator.DataAccess
{
    internal class DataAccessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ExcludedDatabaseNameCollection>()
                .AsSelf()
                .SingleInstance();

            builder.RegisterType<ConnectionHandler>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<DatabaseRepository>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
