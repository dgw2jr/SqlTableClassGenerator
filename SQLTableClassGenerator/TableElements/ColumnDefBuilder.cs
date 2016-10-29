using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using SQLTableClassGenerator.DataAccess;
using Microsoft.SqlServer.Types;

namespace SQLTableClassGenerator.TableElements
{
    internal class ColumnDefBuilder : IColumnDefBuilder
    {
        private readonly IConnectionHandler _connectionHandler;

        public ColumnDefBuilder(IConnectionHandler connectionHandler)
        {
            _connectionHandler = connectionHandler;
        }

        public IEnumerable<ColumnDef> Build(string databaseName, string tableName)
        {
            using (var conn = _connectionHandler.GetConnection())
            {
                conn.Open();
                conn.ChangeDatabase(databaseName);

                var cmd = conn.CreateCommand() as SqlCommand;
                cmd.CommandText = $"select top 0 * from {tableName}";
                var dt = new DataTable();
                var da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                var columns = dt.Columns.Cast<DataColumn>().OrderBy(c => c.ColumnName);

                var columnDefs = columns.Select(c => new ColumnDef(c.ColumnName, c.DataType.UnderlyingSystemType));

                return columnDefs;
            }
        }
    }
}
