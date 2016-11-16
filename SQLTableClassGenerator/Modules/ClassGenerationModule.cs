using Autofac;
using ClassGeneration;

namespace SQLTableClassGenerator.Modules
{
    internal class ClassGenerationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(RoslynClassBuilder<>)).AsImplementedInterfaces();
            builder.RegisterGeneric(typeof(ClassDeclarationBuilder<>)).AsImplementedInterfaces();
            builder.RegisterType<ColumnPropertiesAssignmentBlockBuilder>().AsImplementedInterfaces();
            builder.RegisterType<ColumnParameterSyntaxBuilder>().AsImplementedInterfaces();
            builder.RegisterType<ColumnPropertyBuilder>().AsImplementedInterfaces();
            builder.RegisterType<ConstructorBuilder>().AsImplementedInterfaces();
            builder.RegisterType<PropertiesBuilder>().AsImplementedInterfaces();
        }
    }
}
