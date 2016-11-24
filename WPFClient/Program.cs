using System;
using System.Reflection;
using System.Windows;
using Autofac;
using ClassGeneration.Modules;

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
            builder.RegisterAssemblyModules(Assembly.GetAssembly(typeof(ClassGenerationModule)));

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