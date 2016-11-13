using SQLTableClassGenerator.TableElements;

namespace SQLTableClassGenerator.TableClassParts.Interfaces
{
    public interface ITableClassBuilder
    {
        string Build(TableDef table);
    }
}