using System.Collections.Generic;
using ClassGeneration.Interfaces;
using ClassGeneration.Models;

namespace SQLTableClassGenerator.ModelBuilders
{
    internal class TableBuilder : IBuilder<Table, Table>
    {
        private readonly IBuilder<Table, IEnumerable<Column>> _columnBuilder;

        public TableBuilder(IBuilder<Table, IEnumerable<Column>> columnBuilder)
        {
            _columnBuilder = columnBuilder;
        }

        public Table Build(string databaseName, Table table)
        {
            var columns = _columnBuilder.Build(databaseName, table);

            return new Table(table, columns);
        }
    }
}
