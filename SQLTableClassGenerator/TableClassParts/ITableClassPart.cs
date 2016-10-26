using SQLTableClassGenerator.TableElements;

namespace SQLTableClassGenerator.TableClassParts
{
    public interface ITableClassPart
    {
        string GetString(TableDef table);
    }
}