using System;
using System.Collections.Generic;
using System.Text;

namespace SQLTableClassGenerator
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

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(string.Format("public class {1}{0}{{{0}", Environment.NewLine, this.Name));

            foreach (var column in this.Columns)
            {
                sb.Append(string.Format("\t{1}{0}", Environment.NewLine, column.ToString()));
            }

            sb.Append("}");

            return sb.ToString();
        }
    }
}