using System.Collections.Generic;
using System.Data;
using System.Linq;
using DataAccess;
using Models;

namespace Repositories
{
    public class DatabaseRepository : IRepository<Database>
    {
        private readonly ISQLConnectionResource _dbResource;
        private readonly ExcludedDatabaseNameCollection _excludedDatabaseNames;
        private readonly IDatabaseBuilder<Database> _databaseBuilder;

        private IEnumerable<Database> _databases;

        public DatabaseRepository(
            ISQLConnectionResource dbResource,
            ExcludedDatabaseNameCollection excludedDatabaseNames,
            IDatabaseBuilder<Database> databaseBuilder)
        {
            _dbResource = dbResource;
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
            _databases = _dbResource.Invoke(conn => conn
                    .GetSchema("Databases"))
                    .AsEnumerable()
                    .Select(row => row[0].ToString())
                    .Where(name => !_excludedDatabaseNames.Contains(name))
                    .OrderBy(name => name)
                    .Select(name => _databaseBuilder.Build(name));
        }
    }
}