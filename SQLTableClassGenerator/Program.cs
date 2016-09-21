using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            builder.RegisterType<ConnectionHandler>().AsImplementedInterfaces();
            builder.RegisterType<MainForm>().As<Form>();

            return builder.Build();
        }
    }
}
