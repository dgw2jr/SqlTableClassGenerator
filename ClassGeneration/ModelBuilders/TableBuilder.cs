using System.Collections.Generic;
using ClassGeneration.Interfaces;
using Models;

namespace ClassGeneration.ModelBuilders
{
    public class TableBuilder : IBuilder<Table, Table>
    {
        private readonly IBuilder<Table, IEnumerable<Column>> _columnBuilder;

        public TableBuilder(IBuilder<Table, IEnumerable<Column>> columnBuilder)
        {
            _columnBuilder = columnBuilder;
        }

        public Table Build(Table table)
        {
            var columns = _columnBuilder.Build(table);

            return new Table(table, columns);
        }
    }
}
