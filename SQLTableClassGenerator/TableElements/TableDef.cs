using System.Collections.Generic;

namespace SQLTableClassGenerator.TableElements
{
    public class TableDef
    {
        public TableDef(string name, IEnumerable<ColumnDef> columnDefs)
        {
            Name = name;
            Columns = columnDefs;
        }

        public string Name { get; }

        public IEnumerable<ColumnDef> Columns { get; }
    }
}