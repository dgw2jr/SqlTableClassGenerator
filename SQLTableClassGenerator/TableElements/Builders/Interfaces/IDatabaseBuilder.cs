using System.Data.Common;
using SQLTableClassGenerator.DataAccess;

namespace SQLTableClassGenerator.TableElements.Builders.Interfaces
{
    public interface IDatabaseBuilder
    {
        Database Build(string databaseName, DbConnection connection);
    }
}