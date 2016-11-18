using System.Data.SqlClient;

namespace DataAccess
{
    public interface IConnectionSetter
    {
        void SetConnection();
    }
}