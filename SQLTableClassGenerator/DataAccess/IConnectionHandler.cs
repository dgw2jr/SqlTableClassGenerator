using System.Data.Common;

namespace SQLTableClassGenerator.DataAccess
{
    public interface IConnectionHandler
    {
        DbConnection GetConnection();

        void SetConnection();
    }
}