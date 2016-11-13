namespace SQLTableClassGenerator.TableElements.Builders.Interfaces
{
    public interface ITableDefBuilder
    {
        TableDef Build(string databaseName, Table table);
    }
}