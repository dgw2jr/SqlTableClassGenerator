using System.Data.Common;

namespace DataAccess
{
    public interface IConnectionHandler
    {
        DbConnection GetConnection();

        void SetConnection();
    }
}