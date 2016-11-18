using System;
using System.Data.SqlClient;

namespace DataAccess
{
    public interface ISQLConnectionResource
    {
        T Invoke<T>(Func<SqlConnection, T> action);
    }
}
