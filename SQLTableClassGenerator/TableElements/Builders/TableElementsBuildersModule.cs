using Autofac;

namespace SQLTableClassGenerator.TableElements.Builders
{
    internal class TableElementsBuildersModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DatabaseBuilder>().AsImplementedInterfaces();
            builder.RegisterType<TableDefBuilder>().AsImplementedInterfaces();
            builder.RegisterType<ColumnDefBuilder>().AsImplementedInterfaces();
        }
    }
}
