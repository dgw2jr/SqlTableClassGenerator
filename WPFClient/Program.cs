using System;
using System.Reflection;
using System.Windows;
using Autofac;

namespace WPFClient
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            var container = GetContainer();

            RunApplication(container);
        }

        static IContainer GetContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());

            return builder.Build();
        }

        private static void RunApplication(IContainer container)
        {
            var app = new App();
            var mainWindow = container.Resolve<Window>();
            app.Run(mainWindow);
        }
    }
}