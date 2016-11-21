using Autofac;
using ClassGeneration.ModelBuilders;
using Repositories;

namespace SQLTableClassGenerator.Modules
{
    internal class ModelBuildersModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DatabaseBuilder>().AsImplementedInterfaces();
            builder.RegisterType<TableListBuilder>().AsImplementedInterfaces();
            builder.RegisterType<TableBuilder>().AsImplementedInterfaces();
            builder.RegisterType<ColumnBuilder>().AsImplementedInterfaces();
        }
    }
}
