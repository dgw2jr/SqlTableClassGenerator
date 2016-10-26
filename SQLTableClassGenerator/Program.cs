using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Autofac;
using SQLTableClassGenerator.Properties;

namespace SQLTableClassGenerator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var scope = GetContainer().BeginLifetimeScope())
            {
                Application.Run(scope.Resolve<Form>());
            }
        }

        static IContainer GetContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<TableClassBuilder>().AsImplementedInterfaces();
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

            builder.RegisterType<TreeViewPopulator>().AsImplementedInterfaces();
            builder.RegisterType<TableDefBuilder>().AsImplementedInterfaces();
            builder.RegisterType<ConnectionHandler>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<MainForm>().As<Form>();

            return builder.Build();
        }

        private static IEnumerable<T> Switch<TOn, TOff, T>(this IEnumerable<T> list, bool predicate)
        {
            return list.Where(item => item.GetType() != (predicate ? typeof(TOff) : typeof(TOn)));
        }
    }
}
