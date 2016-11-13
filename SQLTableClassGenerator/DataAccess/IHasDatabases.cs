using System.Collections.Generic;
using SQLTableClassGenerator.TableElements;

namespace SQLTableClassGenerator.DataAccess
{
    public interface IHasDatabases
    {
        List<Database> Databases { get; }
    }
}