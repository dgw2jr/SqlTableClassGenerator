using System.Data;

namespace SQLTableClassGenerator
{
    public class TableDefBuilder : ITableDefBuilder
    {
        private readonly IConnectionHandler _connectionHandler;

        private TableClassBuilderOptions _options;

        public TableDefBuilder(IConnectionHandler connectionHandler)
        {
            _connectionHandler = connectionHandler;
        }

        public TableDefBuilder WithOptions(TableClassBuilderOptions options)
        {
            _options = options;
            return this;
        }

        public TableDef Build(string databaseName, string tableName)
        {
            using (var conn = _connectionHandler.GetConnection())
            {
                conn.Open();
                conn.ChangeDatabase(databaseName);

                var columns = conn.GetSchema("Columns", new string[] { databaseName, null, tableName }).AsEnumerable().OrderBy(o => o[3]);

                var columnDefs = columns.Select(c => new ColumnDef(c["column_name"].ToString(), c["data_type"].ToString()));

                TableDef t = new TableDef(tableName, columnDefs);

                return t;
            }
        }
    }
}
