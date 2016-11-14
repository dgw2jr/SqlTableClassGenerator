using System.Windows.Forms;
using Autofac;

namespace SQLTableClassGenerator
{
    internal class UIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TreeView>().AsSelf().SingleInstance();
            builder.RegisterType<TreeViewPopulator>().AsImplementedInterfaces();
            builder.RegisterType<MainForm>().As<Form>();
        }
    }
}
