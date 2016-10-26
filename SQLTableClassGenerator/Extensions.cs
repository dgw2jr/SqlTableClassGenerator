using System.Collections.Generic;
using System.Linq;

namespace SQLTableClassGenerator
{
    internal static class Extensions
    {
        internal static IEnumerable<T> Switch<TOn, TOff, T>(this IEnumerable<T> list, bool predicate)
        {
            return list.Where(item => item.GetType() != (predicate ? typeof(TOff) : typeof(TOn)));
        }
    }
}
