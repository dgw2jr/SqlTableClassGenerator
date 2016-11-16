using System.Data.Common;

namespace Repositories
{
    public interface IDatabaseBuilder<out TOut>
    {
        TOut Build(string databaseName, DbConnection connection);
    }
}