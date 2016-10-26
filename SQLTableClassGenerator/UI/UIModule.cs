using System.Windows.Forms;
using Autofac;

namespace SQLTableClassGenerator.UI
{
    internal class UIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TreeViewPopulator>().AsImplementedInterfaces();
            builder.RegisterType<MainForm>().As<Form>();
        }
    }
}
