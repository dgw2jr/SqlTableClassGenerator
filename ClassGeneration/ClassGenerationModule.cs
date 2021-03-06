﻿using System;
using Autofac;
using Autofac.Builder;
using ClassGeneration.Interfaces;
using ClassGeneration.Properties;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;
using Models;

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
            builder.RegisterGeneric(typeof(ClassDeclarationBuilder<>)).AsImplementedInterfaces();
            builder.RegisterType<ColumnPropertiesAssignmentBlockBuilder>().AsImplementedInterfaces();
            builder.RegisterType<ColumnParameterSyntaxBuilder>().AsImplementedInterfaces();
            builder.RegisterType<ColumnPropertyBuilder>().AsImplementedInterfaces();
            builder.RegisterType<PropertiesBuilder>().AsImplementedInterfaces();

            builder.Register<ConstructorBuilder, NullClassMembersBuilder<Table>, IClassMembersBuilder<Table>>(() => Settings.Default.GenerateConstructor);
            builder.Register<PrivateModifier, NullModifier, IPropertySetterAccessibilityModifier>(() => Settings.Default.PrivateSetters);
            builder.Register<SealedClassDeclarationModifier, NullClassDeclarationModifier, IClassDeclarationModifier>(() => Settings.Default.IsSealed);
            builder.Register<PropertyGetter, PropertyGetterAndSetter, IPropertyAccessorDeclarations>(() => Settings.Default.Immutable);
        }
    }

    public static class BuilderExtensions
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