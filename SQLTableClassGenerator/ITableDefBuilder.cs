namespace SQLTableClassGenerator
{
    public interface ITableDefBuilder
    {
        TableDef Build(string databaseName, string tableName);
    }
}