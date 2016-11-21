using System.Data;

namespace SQLTableClassGenerator.Tests
{
    internal class TableSchemaDataTableBuilder
    {
        private readonly DataTable _dataTable;

        public TableSchemaDataTableBuilder()
        {
            _dataTable = new DataTable();
            _dataTable.Columns.Add(new DataColumn("table_catalog", typeof(string)));
            _dataTable.Columns.Add(new DataColumn("table_schema", typeof(string)));
            _dataTable.Columns.Add(new DataColumn("table_name", typeof(string)));
            _dataTable.Columns.Add(new DataColumn("table_type", typeof(string)));
        }

        public DataTable Build()
        {
            return _dataTable;
        }

        public TableSchemaDataTableBuilder AddRow(string catalog, string schema, string name, string type)
        {
            _dataTable.Rows.Add(catalog, schema, name, type);
            return this;
        }
    }
}