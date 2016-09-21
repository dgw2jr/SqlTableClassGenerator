using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;

namespace SQLTableClassGenerator
{
    public class TableDef
    {
        public string Name;

        public IEnumerable<ColumnDef> Columns;

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

    public class ColumnDef
    {
        private Dictionary<string, string> TypeMap = new Dictionary<string, string>() {
		    { "char", "string" },
		    { "tinyint", "int" },
		    { "varchar", "string" },
            { "nvarchar", "string" },
		    { "money", "decimal" },
		    { "datetime", "DateTime" },
		    { "int", "int" },
		    { "smallint", "int" },
		    { "bit", "bool" },
            { "numeric", "decimal" },
            { "bigint", "long"},
            { "timestamp", "byte[]" },
            { "float", "float" },
            { "decimal", "decimal" },
            { "real", "float" },
            { "smalldatetime", "DateTime" },
            { "image", "byte[]" },
            { "uniqueidentifier", "guid" },
            { "nchar", "string" },
            { "text", "string" },
            { "geography", "DBGeography" }
	    };
        
        
        public string SQLType;
        public string NETType
        {
            get
            {
                try
                {
                    return this.TypeMap[this.SQLType];
                }
                catch (KeyNotFoundException)
                {
                    return "object";
                }
            }
        }

        public string Field;

        public override string ToString()
        {
            return string.Format("public {0} {1} {{ get; set; }}", this.NETType, this.Field);
        }

    }
}
