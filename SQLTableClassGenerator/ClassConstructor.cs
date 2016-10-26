using System;
using System.Linq;
using System.Text;

namespace SQLTableClassGenerator
{
    public class ClassConstructor : ITableClassPart
    {
        public string GetString(TableDef table)
        {
            var sb = new StringBuilder();

            var paramList = table.Columns.Select(c => string.Format("{0} {1}", c.NETType, c.Field.ToLower()));
            var paramListStr = string.Join(", ", paramList);
            var ctorHead = string.Format("\tpublic {1}({2}){0}\t{{{0}", Environment.NewLine, table.Name, paramListStr);

            sb.Append(ctorHead);

            var propAssignments = table.Columns.Select(c => string.Format("\t\t{0} = {1};{2}", c.Field, c.Field.ToLower(), Environment.NewLine));
            var ctorBody = string.Join("", propAssignments);

            sb.Append(ctorBody);
            sb.Append(string.Format("\t}}{0}{0}", Environment.NewLine));

            return sb.ToString();
        }
    }
}