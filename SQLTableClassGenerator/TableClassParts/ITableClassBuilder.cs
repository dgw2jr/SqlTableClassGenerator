using SQLTableClassGenerator.TableElements;

namespace SQLTableClassGenerator.TableClassParts
{
    public interface ITableClassBuilder
    {
        string Build(TableDef table);
    }
}