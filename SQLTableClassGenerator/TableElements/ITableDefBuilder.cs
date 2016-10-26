namespace SQLTableClassGenerator.TableElements
{
    public interface ITableDefBuilder
    {
        TableDef Build(string databaseName, string tableName);
    }
}