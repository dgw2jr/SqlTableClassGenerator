using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using SQLTableClassGenerator.DataAccess;
using SQLTableClassGenerator.Interfaces;
using SQLTableClassGenerator.Properties;
using SQLTableClassGenerator.TableClassParts.Interfaces;
using SQLTableClassGenerator.TableElements.Builders.Interfaces;

namespace SQLTableClassGenerator.UI
{
    public partial class MainForm : Form
    {
        private readonly IConnectionHandler _connectionHandler;
        private readonly IHasDatabases _databasesContainer;
        private readonly ITableDefBuilder _tableDefBuilder;
        private readonly ITableClassBuilder _tableClassBuilder;
        private readonly IPopulator _treeViewPopulator;

        public MainForm(
            IConnectionHandler connectionHandler,
            IHasDatabases databasesContainer,
            ITableDefBuilder tableDefBuilder,
            ITableClassBuilder tableClassBuilder,
            IPopulator treePopulator,
            TreeView tree)
        {
            treeView1 = tree;
            _connectionHandler = connectionHandler;
            _databasesContainer = databasesContainer;
            _tableDefBuilder = tableDefBuilder;
            _tableClassBuilder = tableClassBuilder;
            _treeViewPopulator = treePopulator;

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _connectionHandler.SetConnection();

            BackgroundWorker bgw = new BackgroundWorker();
            bgw.DoWork += new DoWorkEventHandler(bgw_DoWork);
            bgw.RunWorkerAsync();
        }

        private void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            _treeViewPopulator.Populate();
        }
        
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Parent == null)
                return;

            GenerateClass(e.Node.Level, e.Node.Name, e.Node.Parent.Name);
        }
        
        private void GenerateClass(int nodeLevel, string name, string parentName)
        {
            if (nodeLevel == 0) return;

            var table = _databasesContainer
                .Databases
                .FirstOrDefault(db => db.Name == parentName)
                .Tables
                .FirstOrDefault(tbl => tbl.Name == name.Split('.')[1]);

            var tableDef = _tableDefBuilder.Build(parentName, table);

            richTextBox1.Text = _tableClassBuilder.Build(tableDef);
        }

        private void GenerateClass(TreeView treeView)
        {
            if (treeView.SelectedNode == null)
                return;

            GenerateClass(treeView.SelectedNode.Level, treeView.SelectedNode.Name, treeView.SelectedNode.Parent.Name);
        }
        
        private void generateConstructorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Execute(() => Settings.Default.GenerateConstructor = ((ToolStripMenuItem)sender).Checked);
        }

        private void sealClassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Execute(() => Settings.Default.IsSealed = ((ToolStripMenuItem)sender).Checked);
        }

        private void privateSettersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Execute(() => Settings.Default.PrivateSetters = ((ToolStripMenuItem)sender).Checked);
        }

        private void Execute(Action action)
        {
            action();
            GenerateClass(treeView1);
        }
    }
}
