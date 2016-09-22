using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SQLTableClassGenerator
{
    public class TreeViewPopulator : ITreeViewPopulator
    {
        private readonly IConnectionHandler _connectionHandler;

        public TreeViewPopulator(IConnectionHandler connectionHandler)
        {
            _connectionHandler = connectionHandler;
        }

        private delegate void AddDbNode(TreeView tree, string Name);
        private void Add(TreeView tree, string Name)
        {
            tree.Nodes.Add(Name, Name);
        }

        private delegate void AddTblNode(TreeView tree, string dbName, string tblName);
        private void Add(TreeView tree, string dbName, string tblName)
        {
            tree.Nodes[dbName].Nodes.Add(tblName, tblName);
        }

        public void Populate(TreeView tree)
        {
            var blacklist = new[]
            {
                "master",
                "model",
                "tempdb",
                "msdb"
            };

            using (var conn = _connectionHandler.GetConnection())
            {
                conn.Open();

                var databases = conn.GetSchema("Databases").AsEnumerable().OrderBy(o => o[0]);

                foreach (var dbRow in databases.Where(row => !blacklist.Contains(row[0].ToString())))
                {
                    var dbName = dbRow[0].ToString();
                    try
                    {
                        conn.ChangeDatabase(dbName);
                    }
                    catch (Exception exception)
                    {
                           continue; 
                    }

                    var tables = conn.GetSchema("Tables").AsEnumerable().OrderBy(o => o[2]);

                    // add database node
                    tree.Invoke(new AddDbNode(Add), tree, dbName);

                    // add tables for database nodes
                    foreach (DataRow row in tables)
                    {
                        var tableName = row[2].ToString();
                        tree.Invoke(new AddTblNode(Add), tree, dbName, tableName);
                    }
                }
            }
        }
    }
}
