using System.Collections.Generic;

namespace ClassGeneration.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> All();
    }
}