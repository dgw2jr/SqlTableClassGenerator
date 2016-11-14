using System;
using System.Windows.Forms;
using DataAccess;
using Models;
using SQLTableClassGenerator.Interfaces;

namespace SQLTableClassGenerator
{
    public class TreeViewPopulator : IPopulator
    {
        private readonly IRepository<Database> _databaseRepository;
        private readonly TreeView _tree;

        public TreeViewPopulator(
            IRepository<Database> databaseRepository,
            TreeView tree)
        {
            _databaseRepository = databaseRepository;
            _tree = tree;
        }

        public void Populate()
        {
            foreach (var database in _databaseRepository.All())
            {
                _tree.Invoke(new Action(() => _tree.Nodes.Add(database.Name, database.Name)));
                
                foreach (var table in database.Tables)
                {
                    _tree.Invoke(new Action(() => _tree.Nodes[database.Name].Nodes.Add(table.NameWithSchemaPrefix, table.NameWithSchemaPrefix)));
                }
            }
        }
    }
}