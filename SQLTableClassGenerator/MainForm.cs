using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using ClassGeneration.Interfaces;
using SQLTableClassGenerator.Interfaces;
using ClassGeneration.Properties;
using DataAccess;
using Models;

namespace SQLTableClassGenerator
{
    public partial class MainForm : Form
    {
        private readonly IConnectionHandler _connectionHandler;
        private readonly IRepository<Database> _databaseRepository;
        private readonly IBuilder<Table, Table> _tableBuilder;
        private readonly IClassBuilder<Table> _classBuilder;
        private readonly IPopulator _treeViewPopulator;

        public MainForm(
            IConnectionHandler connectionHandler,
            IRepository<Database> databaseRepository,
            IBuilder<Table, Table> tableBuilder,
            IClassBuilder<Table> classBuilder,
            IPopulator treePopulator,
            TreeView tree)
        {
            treeView1 = tree;
            _connectionHandler = connectionHandler;
            _databaseRepository = databaseRepository;
            _tableBuilder = tableBuilder;
            _classBuilder = classBuilder;
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

            var table = _databaseRepository
                .All()
                .FirstOrDefault(db => db.Name == parentName)
                .Tables
                .FirstOrDefault(tbl => tbl.Name == name.Split('.')[1]);

            var tableDef = _tableBuilder.Build(table);

            richTextBox1.Text = _classBuilder.Build(tableDef, Settings.Default);
        }

        private void GenerateClass(TreeNode node)
        {
            if (node == null)
                return;

            GenerateClass(node.Level, node.Name, node.Parent.Name);
        }
        
        private void generateConstructorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenerateClassWithActions(() => Settings.Default.GenerateConstructor = ((ToolStripMenuItem)sender).Checked);
        }

        private void sealClassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenerateClassWithActions(() => Settings.Default.IsSealed = ((ToolStripMenuItem)sender).Checked);
        }

        private void privateSettersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenerateClassWithActions(() => Settings.Default.PrivateSetters = ((ToolStripMenuItem)sender).Checked);
        }

        private void GenerateClassWithActions(Action preAction = null, Action postAction = null)
        {
            preAction?.Invoke();
            GenerateClass(treeView1.SelectedNode);
            postAction?.Invoke();
        }
    }
}
