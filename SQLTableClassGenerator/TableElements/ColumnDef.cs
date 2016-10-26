using System.Collections.Generic;

namespace SQLTableClassGenerator.TableElements
{
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

        public ColumnDef(string columnName, string columnType)
        {
            SQLType = columnType;
            Field = columnName;
        }
        

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

        public string SQLType
        {
            get;
        }

        public string Field
        {
            get;
        }
        
        public override string ToString()
        {
            return string.Format("public {0} {1} {{ get; set; }}", this.NETType, this.Field);
        }
    }
}