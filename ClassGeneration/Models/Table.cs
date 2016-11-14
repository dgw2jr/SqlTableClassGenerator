using System.Collections.Generic;

namespace ClassGeneration.Models
{
    public class Table
    {
        public Table(string name, string schema)
        {
            Name = name;
            Schema = schema;
            Columns = new List<Column>();
        }

        public Table(string name, string schema, IEnumerable<Column> columnDefs)
            : this(name, schema)
        {
            Columns = columnDefs;
        }

        public Table(Table table, IEnumerable<Column> columnDefs)
            : this(table.Name, table.Schema, columnDefs)
        {
        }

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