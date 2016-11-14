using System.Data.Common;

namespace SQLTableClassGenerator.Interfaces
{
    public interface IConnectionHandler
    {
        DbConnection GetConnection();

        void SetConnection();
    }
}