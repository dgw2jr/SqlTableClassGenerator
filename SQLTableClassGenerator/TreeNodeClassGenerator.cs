using System;
using System.Linq;
using System.Windows.Forms;
using ClassGeneration.Interfaces;
using ClassGeneration.Properties;
using DataAccess;
using Models;
using SQLTableClassGenerator.Interfaces;

namespace SQLTableClassGenerator
{
    public class TreeNodeClassGenerator : ITreeNodeClassGenerator
    {
        private readonly IBuilder<Table, string> _classStringBuilder;
        private readonly IRepository<Database> _databaseRepository;
        private readonly IBuilder<Table, Table> _tableBuilder;

        public TreeNodeClassGenerator(
            IRepository<Database> databaseRepository,
            IBuilder<Table, Table> tableBuilder,
            IBuilder<Table, string> classStringBuilder)
        {
            _databaseRepository = databaseRepository;
            _tableBuilder = tableBuilder;
            _classStringBuilder = classStringBuilder;
        }

        public string Generate(TreeNode node, Action preAction = null)
        {
            preAction?.Invoke();
            return Generate(node);
        }

        private string Generate(TreeNode node)
        {
            if (node == null || node.Level == 0)
                return string.Empty;

            return Generate(node.Name, node.Parent.Name);
        }

        public string Generate(string name, string parentName)
        {
            var table = _databaseRepository
                .All()
                .FirstOrDefault(db => db.Name == parentName)
                .Tables
                .FirstOrDefault(tbl => tbl.Name == name.Split('.')[1]);

            var tableDef = _tableBuilder.Build(table);

            return _classStringBuilder.Build(tableDef, Settings.Default);
        }
    }
}
