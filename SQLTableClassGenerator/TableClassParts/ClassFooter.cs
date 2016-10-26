using SQLTableClassGenerator.TableElements;

namespace SQLTableClassGenerator.TableClassParts
{
    public class ClassFooter : ITableClassPart
    {
        public string GetString(TableDef table)
        {
            return "}";
        }
    }
}