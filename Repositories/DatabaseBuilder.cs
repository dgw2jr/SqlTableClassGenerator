using Models;

namespace Repositories
{
    public class DatabaseBuilder : IDatabaseBuilder<Database>
    {
        private readonly ITableListBuilder _tableListBuilder;

        public DatabaseBuilder(ITableListBuilder tableListBuilder)
        {
            _tableListBuilder = tableListBuilder;
        }

        public Database Build(string databaseName)
        {
            return new Database(databaseName, _tableListBuilder.Build(databaseName));
        }
    }
}