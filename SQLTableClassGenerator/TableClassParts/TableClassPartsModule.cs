using System;
using System.Collections.Generic;
using Autofac;
using SQLTableClassGenerator.Properties;

namespace SQLTableClassGenerator.TableClassParts
{
    internal class TableClassPartsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TableClassBuilder>().AsImplementedInterfaces();

            // Order of the parts matters, but it shouldn't. Need a design that doesn't rely on registration order.
            builder.RegisterType<ClassHeader>().AsImplementedInterfaces();
            builder.RegisterType<SealedClassHeader>().AsImplementedInterfaces();
            builder.RegisterType<ClassConstructor>().AsImplementedInterfaces();
            builder.RegisterType<ClassProperties>().AsImplementedInterfaces();
            builder.RegisterType<ClassFooter>().AsImplementedInterfaces();

            builder.Register<Func<IEnumerable<ITableClassPart>>>(c =>
            {
                var ctx = c.Resolve<IComponentContext>();

                return () =>
                {
                    var all = ctx.Resolve<IEnumerable<ITableClassPart>>();

                    all = all.Switch<ClassConstructor, NullTableClassPart, ITableClassPart>(Settings.Default.GenerateConstructor);
                    all = all.Switch<SealedClassHeader, ClassHeader, ITableClassPart>(Settings.Default.IsSealed);

                    return all;
                };
            });
        }
    }
}
