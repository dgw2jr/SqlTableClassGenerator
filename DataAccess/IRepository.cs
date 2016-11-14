using System.Collections.Generic;

namespace DataAccess
{
    public interface IRepository<T>
    {
        IEnumerable<T> All();
    }
}