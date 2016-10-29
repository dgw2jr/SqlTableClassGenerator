using System;

namespace SQLTableClassGenerator.TableElements
{
    public class ColumnDef
    {
        public ColumnDef(string columnName, Type columnType)
        {
            Type = columnType;
            Field = columnName;
        }

        public Type Type
        {
            get;
        }

        public string Field
        {
            get;
        }
        
        public override string ToString()
        {
            return string.Format("public {0} {1} {{ get; set; }}", this.Type.Name, this.Field);
        }
    }
}