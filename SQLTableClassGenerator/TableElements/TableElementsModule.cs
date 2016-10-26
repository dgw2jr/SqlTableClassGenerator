using Autofac;

namespace SQLTableClassGenerator.TableElements
{
    internal class TableElementsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TableDefBuilder>().AsImplementedInterfaces();
        }
    }
}
