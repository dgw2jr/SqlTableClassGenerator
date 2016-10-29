using System.Collections.Generic;

namespace SQLTableClassGenerator.TableElements
{
    public interface IColumnDefBuilder
    {
        IEnumerable<ColumnDef> Build(string databaseName, string tableName);
    }
}