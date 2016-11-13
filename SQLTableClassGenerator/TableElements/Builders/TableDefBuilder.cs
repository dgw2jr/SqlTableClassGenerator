using SQLTableClassGenerator.TableElements.Builders.Interfaces;

namespace SQLTableClassGenerator.TableElements.Builders
{
    internal class TableDefBuilder : ITableDefBuilder
    {
        private readonly IColumnDefBuilder _columnDefBuilder;

        public TableDefBuilder(IColumnDefBuilder columnDefBuilder)
        {
            _columnDefBuilder = columnDefBuilder;
        }

        public TableDef Build(string databaseName, Table table)
        {
            var columnDefs = _columnDefBuilder.Build(databaseName, table);

            return new TableDef(table.Name, columnDefs);
        }
    }
}
