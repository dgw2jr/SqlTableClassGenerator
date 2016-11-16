using System.Collections.Generic;
using System.Data;
using System.Linq;
using DataAccess;
using Models;

namespace Repositories
{
    public class DatabaseRepository : IRepository<Database>
    {
        private readonly IConnectionHandler _connectionHandler;
        private readonly ExcludedDatabaseNameCollection _excludedDatabaseNames;
        private readonly IDatabaseBuilder<Database> _databaseBuilder;

        private IEnumerable<Database> _databases;

        public DatabaseRepository(
            IConnectionHandler connectionHandler,
            ExcludedDatabaseNameCollection excludedDatabaseNames,
            IDatabaseBuilder<Database> databaseBuilder)
        {
            _connectionHandler = connectionHandler;
            _excludedDatabaseNames = excludedDatabaseNames;
            _databaseBuilder = databaseBuilder;
        }

        public IEnumerable<Database> All()
        {
            if (_databases == null)
                Initialize();

            return _databases;
        }

        private void Initialize()
        {
            using (var conn = _connectionHandler.GetConnection())
            {
                conn.Open();

                _databases = conn
                    .GetSchema("Databases")
                    .AsEnumerable()
                    .Where(row => !_excludedDatabaseNames.Contains(row[0].ToString()))
                    .OrderBy(o => o[0])
                    .Select(row => _databaseBuilder.Build(row[0].ToString(), conn))
                    .ToList();
            }
        }
    }
}
