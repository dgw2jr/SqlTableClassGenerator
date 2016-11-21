using System.Linq;
using System.Collections.Generic;
using System.Data;
using DataAccess;
using Models;

namespace Repositories
{
    public class TableListBuilder : ITableListBuilder
    {
        private readonly ISQLConnectionResource _dbResource;

        public TableListBuilder(ISQLConnectionResource dbResource)
        {
            _dbResource = dbResource;
        }

        public IEnumerable<Table> Build(string databaseName)
        {
            return _dbResource.Invoke(conn =>
            {
                conn.ChangeDatabase(databaseName);
                return conn.GetSchema("Tables");
            })
            .AsEnumerable()
            .OrderBy(o => o[2])
            .Select(table =>
                new Table(databaseName, table.Field<string>("table_name"), table.Field<string>("table_schema")));
        }
    }
}