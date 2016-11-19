using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Models
{
    public class Table : IHasName
    {
        public Table(string databaseName, string name, string schema)
        {
            DatabaseName = databaseName;
            Name = name;
            Schema = schema;
            Columns = Enumerable.Empty<Column>();
        }

        public Table(string databaseName, string name, string schema, IEnumerable<Column> columnDefs)
            : this(databaseName, name, schema)
        {
            Columns = columnDefs;
        }

        public Table(Table table, IEnumerable<Column> columnDefs)
            : this(table.DatabaseName, table.Name, table.Schema, columnDefs)
        {
        }

        public string DatabaseName { get; }

        public string Name { get; }

        public string Schema { get; }

        public IEnumerable<Column> Columns { get; }

        public string NameWithSchemaPrefix
        {
            get
            {
                return $"{Schema}.{Name}";
            }
        }

        public string EmptyRowCommand
        {
            get
            {
                return $"select top 0 * from {Schema}.{Name}";
            }
        }

        public IEnumerable<Column> GetColumns(DataTable table)
        {
            return table.Columns
                .Cast<DataColumn>()
                .OrderBy(c => c.ColumnName)
                .Select(c => new Column(c.ColumnName, c.DataType.UnderlyingSystemType));
        }
    }
}