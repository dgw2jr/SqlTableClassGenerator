using Autofac;

namespace SQLTableClassGenerator.DataAccess
{
    internal class DataAccessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ConnectionHandler>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
