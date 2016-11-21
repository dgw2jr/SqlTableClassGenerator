using System.Collections.Generic;
using Models;

namespace Repositories
{
    public interface ITableListBuilder
    {
        IEnumerable<Table> Build(string databaseName);
    }
}