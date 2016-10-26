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

                    if (!Settings.Default.GenerateConstructor)
                    {
                        all = all.Where(part => part.GetType() != typeof(ClassConstructor));
                    }

                    all = all.Where(part => part.GetType() != (Settings.Default.IsSealed ? typeof(ClassHeader) : typeof(SealedClassHeader)));

                    return all;
                };
            });

            builder.RegisterType<TreeViewPopulator>().AsImplementedInterfaces();
            builder.RegisterType<TableDefBuilder>().AsImplementedInterfaces();
            builder.RegisterType<ConnectionHandler>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<MainForm>().As<Form>();

            return builder.Build();
        }
    }
}
