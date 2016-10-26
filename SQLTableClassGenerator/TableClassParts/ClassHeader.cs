using System;
using SQLTableClassGenerator.TableElements;

namespace SQLTableClassGenerator.TableClassParts
{
    public class ClassHeader : ITableClassPart
    {
        public string GetString(TableDef table)
        {
            return string.Format("public class {1}{0}{{{0}", Environment.NewLine, table.Name);
        }
    }
}