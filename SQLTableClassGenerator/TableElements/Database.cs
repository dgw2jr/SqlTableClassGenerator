using System.Collections.Generic;

namespace SQLTableClassGenerator.TableElements
{
    public class Database
    {
        public Database(string name, IEnumerable<Table> tables)
        {
            Name = name;
            Tables = tables;
        }

        public string Name { get; }

        public IEnumerable<Table> Tables { get; }
    }

    public class Table
    {
        public Table(string name, string schema)
        {
            Name = name;
            Schema = schema;
        }

        public string Name { get; }

        public string Schema { get; }
    }
}
