using Autofac;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;

namespace ClassGeneration.Modules
{
    public class ClassGenerationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => SyntaxGenerator.GetGenerator(new AdhocWorkspace(), LanguageNames.CSharp))
                .As<SyntaxGenerator>()
                .SingleInstance();

            builder.RegisterGeneric(typeof(RoslynClassBuilder<>)).AsImplementedInterfaces();
            builder.RegisterType<ClassDeclarationBuilder>().AsImplementedInterfaces();
        }
    }
}