using System.Windows;
using Autofac;

namespace WPFClient.Modules
{
    internal class UIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MainWindow>().As<Window>();
        }
    }
}
