using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;
using Repositories;

namespace SQLTableClassGenerator.Tests
{
    [TestClass]
    public class DatabaseRepositoryTests
    {
        private Mock<ISQLConnectionResource> _dbResource;
        private Mock<IDatabaseBuilder<Database>> _dbBuilder;
        private DatabaseRepository SUT;
        private DatabaseSchemaDataTableBuilder _dataTableBuilder;

        [TestInitialize]
        public void Setup()
        {
            _dbResource = new Mock<ISQLConnectionResource>();
            _dbBuilder = new Mock<IDatabaseBuilder<Database>>();
            _dataTableBuilder = new DatabaseSchemaDataTableBuilder();
            
            SUT = new DatabaseRepository(_dbResource.Object, new ExcludedDatabaseNameCollection(), _dbBuilder.Object);
        }

        [TestMethod]
        public void All_ShouldReturnOrderedFilteredListOfDatabases()
        {
            // Arrange
            var expectedFirst = "A";
            var expectedLast = "B";

            var fakeDataTable = _dataTableBuilder
                .AddRow("master", 1)
                .AddRow(expectedLast, 2)
                .AddRow(expectedFirst, 3)
                .Build();

            _dbBuilder.Setup(m => m.Build(It.IsAny<string>()))
                .Returns<string>(name => new Database(name, null));

            _dbResource.Setup(m => m.Invoke(It.IsAny<Func<SqlConnection, DataTable>>()))
                .Returns(fakeDataTable)
                .Verifiable();

            // Act
            var result = SUT.All();

            // Assert
            Assert.AreEqual(expectedFirst, result.First().Name);
            Assert.AreEqual(expectedLast, result.Last().Name);
            _dbResource.VerifyAll();
        }
    }

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
