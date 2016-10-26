using System;

namespace SQLTableClassGenerator
{
    public class ClassHeader : ITableClassPart
    {
        public string GetString(TableDef table)
        {
            return string.Format("public class {1}{0}{{{0}", Environment.NewLine, table.Name);
        }
    }
}