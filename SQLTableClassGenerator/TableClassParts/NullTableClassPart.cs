using SQLTableClassGenerator.TableElements;

namespace SQLTableClassGenerator.TableClassParts
{
    public class NullTableClassPart : ITableClassPart
    {
        public string GetString(TableDef table)
        {
            return string.Empty;
        }
    }
}