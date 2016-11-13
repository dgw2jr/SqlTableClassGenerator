using System;
using System.Windows.Forms;
using SQLTableClassGenerator.DataAccess;
using SQLTableClassGenerator.Interfaces;

namespace SQLTableClassGenerator.UI
{
    public class TreeViewPopulator : IPopulator
    {
        private readonly IHasDatabases _databasesContainer;
        private readonly TreeView _tree;

        public TreeViewPopulator(
            IHasDatabases databasesContainer,
            TreeView tree)
        {
            _databasesContainer = databasesContainer;
            _tree = tree;
        }

        private TreeNode Add(string Name)
        {
            return _tree.Nodes.Add(Name, Name);
        }

        private TreeNode Add(string dbName, string tblName)
        {
            return _tree.Nodes[dbName].Nodes.Add(tblName, tblName);
        }

        public void Populate()
        {
            foreach (var database in _databasesContainer.Databases)
            {
                _tree.Invoke(new Action(() => Add(database.Name)));
                
                foreach (var table in database.Tables)
                {
                    _tree.Invoke(new Action(() => Add(database.Name, $"{table.Schema}.{table.Name}")));
                }
            }
        }
    }
}
