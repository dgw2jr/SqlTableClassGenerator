using System.Collections.Generic;
using System.Linq;
using ClassGeneration.Interfaces;
using DataAccess;
using Models;

namespace ClassGeneration.ModelBuilders
{
    public class ColumnBuilder : IBuilder<Table, IEnumerable<Column>>
    {
        private readonly ISQLConnectionResource _dbResource;

        public ColumnBuilder(ISQLConnectionResource dbResource)
        {
            _dbResource = dbResource;
        }

        public IEnumerable<Column> Build(Table table)
        {
            try
            {
                return _dbResource.Execute(table.DatabaseName, table.EmptyRowCommand, dt => table.GetColumns(dt));
            }
            catch
            {
                return Enumerable.Empty<Column>();
            }
        }
    }
}