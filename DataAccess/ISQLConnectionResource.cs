using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public interface ISQLConnectionResource
    {
        T Invoke<T>(Func<SqlConnection, T> action);

        T Execute<T>(string databaseName, string commandText, Func<DataTable, T> selector);
    }
}
