using System;
using System.Collections.Generic;
using System.Text;
using SQLTableClassGenerator.TableElements;

namespace SQLTableClassGenerator.TableClassParts
{
    public class TableClassBuilder : ITableClassBuilder
    {
        private readonly Func<IEnumerable<ITableClassPart>> _tableClassParts;

        public TableClassBuilder(Func<IEnumerable<ITableClassPart>> tableClassParts)
        {
            _tableClassParts = tableClassParts;
        }

        public string Build(TableDef table)
        {
            var sb = new StringBuilder();

            foreach (var part in _tableClassParts())
            {
                sb.Append(part.GetString(table));
            }

            return sb.ToString();
        }
    }
}
