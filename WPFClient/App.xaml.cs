using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using Autofac;

namespace WPFClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static IContainer GetContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());

            return builder.Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            using (var scope = GetContainer().BeginLifetimeScope())
            {
                scope.Resolve<Window>().Show();
            }
        }
    }
}
