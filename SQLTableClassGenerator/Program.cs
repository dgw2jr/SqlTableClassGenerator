using System;
using System.Windows.Forms;
using Autofac;

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

            builder.RegisterType<TreeViewPopulator>().AsImplementedInterfaces();
            builder.RegisterType<TableDefBuilder>().AsImplementedInterfaces();
            builder.RegisterType<ConnectionHandler>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<MainForm>().As<Form>();

            return builder.Build();
        }
    }
}
