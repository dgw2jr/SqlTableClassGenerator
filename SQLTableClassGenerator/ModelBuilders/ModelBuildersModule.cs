using Autofac;

namespace SQLTableClassGenerator.ModelBuilders
{
    internal class ModelBuildersModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DatabaseBuilder>().AsImplementedInterfaces();
            builder.RegisterType<TableBuilder>().AsImplementedInterfaces();
            builder.RegisterType<ColumnBuilder>().AsImplementedInterfaces();
        }
    }
}
