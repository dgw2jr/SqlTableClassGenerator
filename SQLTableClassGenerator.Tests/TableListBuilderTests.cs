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
    public class TableListBuilderTests
    {
        private Mock<ISQLConnectionResource> _dbResource;
        private TableListBuilder SUT;
        private TableSchemaDataTableBuilder _dataTableBuilder;

        [TestInitialize]
        public void Setup()
        {
            _dbResource = new Mock<ISQLConnectionResource>();
            _dataTableBuilder = new TableSchemaDataTableBuilder();
            SUT = new TableListBuilder(_dbResource.Object);
        }

        [TestMethod]
        public void Build_ShouldReturnTablesOrderedByName()
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
            Assert.AreEqual(expectedFirst, result.First().Name);
            Assert.AreEqual(expectedLast, result.Last().Name);
        }
    }
}