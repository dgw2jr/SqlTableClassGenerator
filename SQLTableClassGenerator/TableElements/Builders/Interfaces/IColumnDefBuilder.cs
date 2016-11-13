using System.Collections.Generic;

namespace SQLTableClassGenerator.TableElements.Builders.Interfaces
{
    public interface IColumnDefBuilder
    {
        IEnumerable<ColumnDef> Build(string databaseName, Table table);
    }
}