using System.Collections.Generic;

namespace Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> All();
    }
}