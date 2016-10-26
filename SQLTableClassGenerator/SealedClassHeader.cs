using System;

namespace SQLTableClassGenerator
{
    public class SealedClassHeader : ITableClassPart
    {
        public string GetString(TableDef table)
        {
            return string.Format("public sealed class {1}{0}{{{0}", Environment.NewLine, table.Name);
        }
    }
}