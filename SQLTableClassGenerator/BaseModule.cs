using Autofac;
using ClassGeneration;

namespace SQLTableClassGenerator
{
    internal class BaseModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(RoslynClassBuilder<>)).AsImplementedInterfaces();
            builder.RegisterType<BlockStatementListGenerator>().AsImplementedInterfaces();
            builder.RegisterType<ColumnParameterSyntaxBuilder>().AsImplementedInterfaces();
            builder.RegisterType<ColumnPropertyGenerator>().AsImplementedInterfaces();
            builder.RegisterType<ConstructorBuilder>().AsImplementedInterfaces();
            builder.RegisterType<PropertiesBuilder>().AsImplementedInterfaces();
            builder.RegisterType<TableSyntaxNodeBuilder>().AsImplementedInterfaces();
        }
    }
}
