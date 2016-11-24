using System;
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

        private Lazy<IEnumerable<Database>> _databases;

        public DatabaseRepository(
            ISQLConnectionResource dbResource,
            ExcludedDatabaseNameCollection excludedDatabaseNames,
            IDatabaseBuilder<Database> databaseBuilder)
        {
            _dbResource = dbResource;
            _excludedDatabaseNames = excludedDatabaseNames;
            _databaseBuilder = databaseBuilder;

            Initialize();
        }

        public IEnumerable<Database> All()
        {
            return _databases.Value;
        }

        private void Initialize()
        {
            _databases = new Lazy<IEnumerable<Database>>(() => 
                _dbResource.Invoke(conn => conn
                    .GetSchema("Databases"))
                    .AsEnumerable()
                    .Select(row => row[0].ToString())
                    .Where(name => !_excludedDatabaseNames.Contains(name))
                    .OrderBy(name => name)
                    .Select(name => _databaseBuilder.Build(name)));
        }
    }
}