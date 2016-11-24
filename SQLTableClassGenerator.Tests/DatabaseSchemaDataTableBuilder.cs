using System;
using System.Data;

namespace SQLTableClassGenerator.Tests
{
    internal class DatabaseSchemaDataTableBuilder
    {
        private readonly DataTable _dataTable;

        public DatabaseSchemaDataTableBuilder()
        {
            _dataTable = new DataTable();
            _dataTable.Columns.Add(new DataColumn("database_name", typeof(string)));
            _dataTable.Columns.Add(new DataColumn("Dbid", typeof(short)));
            _dataTable.Columns.Add(new DataColumn("create_date", typeof(DateTime)));
        }

        public DataTable Build()
        {
            return _dataTable;
        }

        public DatabaseSchemaDataTableBuilder AddRow(string databaseName, int databaseId)
        {
            _dataTable.Rows.Add(databaseName, databaseId, DateTime.Now);
            return this;
        }
    }
}