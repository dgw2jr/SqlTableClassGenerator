using System.Data.Common;

namespace SQLTableClassGenerator
{
    public interface IConnectionHandler
    {
        DbConnection GetConnection();

        void SetConnection();
    }
}