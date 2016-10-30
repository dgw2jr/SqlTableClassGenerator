using Autofac;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;

namespace SQLTableClassGenerator.TableClassParts
{
    internal class TableClassPartsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => SyntaxGenerator.GetGenerator(c.Resolve<AdhocWorkspace>(), LanguageNames.CSharp)).AsSelf();

            builder.RegisterType<RoslynTableClassBuilder>().AsImplementedInterfaces();
            builder.RegisterType<ConstructorParameterBuilder>().AsSelf();

            builder.RegisterType<ClassBuilder>().AsSelf();
            builder.RegisterType<ConstructorBuilder>().AsImplementedInterfaces();
            builder.RegisterType<PropertiesBuilder>().AsImplementedInterfaces();
        }
    }
}
