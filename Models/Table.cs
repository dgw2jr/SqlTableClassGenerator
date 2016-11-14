using System.Collections.Generic;

namespace Models
{
    public class Table
    {
        public Table(string databaseName, string name, string schema)
        {
            DatabaseName = databaseName;
            Name = name;
            Schema = schema;
            Columns = new List<Column>();
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
    }
}