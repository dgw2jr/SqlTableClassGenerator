using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repositories;

namespace SQLTableClassGenerator.Tests
{
    [TestClass]
    public class DatabaseBuilderTests
    {
        private Mock<ISQLConnectionResource> _dbResource;
        private DatabaseBuilder SUT;
        private TableSchemaDataTableBuilder _dataTableBuilder;

        [TestInitialize]
        public void Setup()
        {
            _dbResource = new Mock<ISQLConnectionResource>();
            _dataTableBuilder = new TableSchemaDataTableBuilder();
            SUT = new DatabaseBuilder(_dbResource.Object);
        }

        [TestMethod]
        public void Build_ShouldReturnDatabaseWithTablesOrderedByName()
        {
            // Arrange
            var dbName = "Test";
            var expectedFirst = "A";
            var expectedLast = "B";

            var fakeDataTable = _dataTableBuilder
                .AddRow("test", "test", expectedLast, "test")
                .AddRow("test", "test", expectedFirst, "test")
                .Build();

            _dbResource.Setup(m => m.Invoke(It.IsAny<Func<SqlConnection, DataTable>>()))
                .Returns(fakeDataTable)
                .Verifiable();

            // Act
            var result = SUT.Build(dbName);

            // Assert
            Assert.AreEqual(dbName, result.Name);
            Assert.AreEqual(expectedFirst, result.Tables.First().Name);
            Assert.AreEqual(expectedLast, result.Tables.Last().Name);
        }
    }

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
