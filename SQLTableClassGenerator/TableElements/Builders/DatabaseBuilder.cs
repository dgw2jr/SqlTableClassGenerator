using System.Data;
using System.Data.Common;
using System.Linq;
using SQLTableClassGenerator.TableElements.Builders.Interfaces;

namespace SQLTableClassGenerator.TableElements.Builders
{
    public class DatabaseBuilder : IDatabaseBuilder
    {
        public Database Build(string databaseName, DbConnection connection)
        {
            connection.ChangeDatabase(databaseName);

            var tables = connection
                .GetSchema("Tables")
                .AsEnumerable()
                .OrderBy(o => o[2])
                .Select(table => new Table(table.Field<string>("table_name"), table.Field<string>("table_schema")))
                .ToList();

            return new Database(databaseName, tables);
        }
    }
}
