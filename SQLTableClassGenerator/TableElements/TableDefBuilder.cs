namespace SQLTableClassGenerator.TableElements
{
    internal class TableDefBuilder : ITableDefBuilder
    {
        private readonly IColumnDefBuilder _columnDefBuilder;

        public TableDefBuilder(IColumnDefBuilder columnDefBuilder)
        {
            _columnDefBuilder = columnDefBuilder;
        }

        public TableDef Build(string databaseName, string tableName)
        {
            var columnDefs = _columnDefBuilder.Build(databaseName, tableName);

            return new TableDef(tableName, columnDefs);
        }
    }
}
