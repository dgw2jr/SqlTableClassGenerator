namespace SQLTableClassGenerator
{
    public interface ITableClassBuilder
    {
        string Build(TableDef table);
    }
}