using System.Data.Common;

namespace ClassGeneration.Interfaces
{
    public interface IDatabaseBuilder<out TOut>
    {
        TOut Build(string databaseName, DbConnection connection);
    }
}