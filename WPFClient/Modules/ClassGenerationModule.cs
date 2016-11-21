using System;
using Autofac;
using Autofac.Builder;
using ClassGeneration;
using ClassGeneration.Interfaces;
using WPFClient.Properties;
using Models;

namespace WPFClient.Modules
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
            builder.RegisterType<PropertiesBuilder>().AsImplementedInterfaces();
            
            builder.Register(c => Settings.Default).As<Settings>();

            builder.Register<ConstructorBuilder, NullTableSyntaxNodesBuilder, ISyntaxNodesBuilder<Table>>(() => Settings.Default.GenerateConstructor);
            builder.Register<PrivateModifier, NullModifier, IPropertySetterAccessibilityModifier>(() => Settings.Default.PrivateSetters);
            builder.Register<SealedClassDeclarationModifier, NullClassDeclarationModifier, IClassDeclarationModifier>(() => Settings.Default.IsSealed);
            builder.Register<PropertyGetter, PropertyGetterAndSetter, IPropertyAccessorDeclarations>(() => Settings.Default.Immutable);
        }
    }

    internal static class BuilderExtensions
    {
        internal static IRegistrationBuilder<TOut, SimpleActivatorData, SingleRegistrationStyle> Register<TOn, TOff, TOut>(this ContainerBuilder builder, Func<bool> predicate)
            where TOn : TOut
            where TOff : TOut
        {
            builder.RegisterType<TOn>().AsSelf();
            builder.RegisterType<TOff>().AsSelf();

            return builder.Register(c =>
            {
                TOut impl = c.Resolve<TOff>();

                if (predicate())
                    impl = c.Resolve<TOn>();

                return impl;
            }).As<TOut>();
        }
    }
}