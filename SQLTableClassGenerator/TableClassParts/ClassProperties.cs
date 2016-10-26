using System;
using System.Text;
using SQLTableClassGenerator.TableElements;

namespace SQLTableClassGenerator.TableClassParts
{
    public class ClassProperties : ITableClassPart
    {
        public string GetString(TableDef table)
        {
            var sb = new StringBuilder();

            foreach (var column in table.Columns)
            {
                sb.Append(string.Format("\t{1}{0}", Environment.NewLine, column.ToString()));
            }

            return sb.ToString();
        }
    }
}