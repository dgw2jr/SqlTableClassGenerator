using System.Data;
using System.Linq;
using DataAccess;
using Models;

namespace Repositories
{
    public class DatabaseBuilder : IDatabaseBuilder<Database>
    {
        private readonly ISQLConnectionResource _dbResource;

        public DatabaseBuilder(ISQLConnectionResource dbResource)
        {
            _dbResource = dbResource;
        }

        public Database Build(string databaseName)
        {
            var tables = _dbResource.Invoke(conn =>
            {
                conn.ChangeDatabase(databaseName);
                return conn.GetSchema("Tables");
            })
            .AsEnumerable()
            .OrderBy(o => o[2])
            .Select(table =>
                new Table(databaseName, table.Field<string>("table_name"), table.Field<string>("table_schema")))
            .ToList();

            return new Database(databaseName, tables);
        }
    }
}
