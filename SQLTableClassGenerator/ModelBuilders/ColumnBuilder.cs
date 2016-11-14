using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using ClassGeneration.Interfaces;
using ClassGeneration.Models;
using SQLTableClassGenerator.Interfaces;

namespace SQLTableClassGenerator.ModelBuilders
{
    internal class ColumnBuilder : IBuilder<Table, IEnumerable<Column>>
    {
        private readonly IConnectionHandler _connectionHandler;

        public ColumnBuilder(IConnectionHandler connectionHandler)
        {
            _connectionHandler = connectionHandler;
        }

        public IEnumerable<Column> Build(string databaseName, Table table)
        {
            using (var conn = _connectionHandler.GetConnection())
            {
                conn.Open();
                conn.ChangeDatabase(databaseName);

                var cmd = conn.CreateCommand() as SqlCommand;
                cmd.CommandText = $"select top 0 * from {table.Schema}.{table.Name}";
                var dt = new DataTable();
                var da = new SqlDataAdapter(cmd);

                try
                {
                    da.Fill(dt);
                }
                catch
                {
                    return new List<Column>();
                }

                var columnDefs = dt.Columns
                    .Cast<DataColumn>()
                    .OrderBy(c => c.ColumnName)
                    .Select(c => new Column(c.ColumnName, c.DataType.UnderlyingSystemType));

                return columnDefs;
            }
        }
    }
}
