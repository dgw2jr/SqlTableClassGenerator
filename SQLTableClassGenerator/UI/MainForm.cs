using System;
using System.ComponentModel;
using System.Windows.Forms;
using SQLTableClassGenerator.DataAccess;
using SQLTableClassGenerator.Properties;
using SQLTableClassGenerator.TableClassParts;
using SQLTableClassGenerator.TableElements;

namespace SQLTableClassGenerator.UI
{
    public partial class MainForm : Form
    {
        private readonly IConnectionHandler _connectionHandler;
        private readonly ITableDefBuilder _tableDefBuilder;
        private readonly ITableClassBuilder _tableClassBuilder;
        private readonly ITreeViewPopulator _treeViewPopulator;

        public MainForm(
            IConnectionHandler connectionHandler, 
            ITableDefBuilder tableDefBuilder,
            ITableClassBuilder tableClassBuilder,
            ITreeViewPopulator treePopulator)
        {
            InitializeComponent();
            _connectionHandler = connectionHandler;
            _tableDefBuilder = tableDefBuilder;
            _tableClassBuilder = tableClassBuilder;
            _treeViewPopulator = treePopulator;
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
            _treeViewPopulator.Populate(treeView1);
        }
        
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            GenerateClass(e.Node.Level, e.Node.Name, e.Node.Parent.Name);
        }

        private void checkBox_generateCtor_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.GenerateConstructor = ((CheckBox)sender).Checked;

            if (treeView1.SelectedNode == null)
                return;

            GenerateClass(treeView1.SelectedNode.Level, treeView1.SelectedNode.Name, treeView1.SelectedNode.Parent.Name);
        }

        private void GenerateClass(int nodeLevel, string name, string parentName)
        {
            if (nodeLevel == 0) return;

            var tableDef = _tableDefBuilder.Build(parentName, name);

            richTextBox1.Text = _tableClassBuilder.Build(tableDef);
        }

        private void checkBox_isSealed_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.IsSealed = ((CheckBox)sender).Checked;

            if (treeView1.SelectedNode == null)
                return;

            GenerateClass(treeView1.SelectedNode.Level, treeView1.SelectedNode.Name, treeView1.SelectedNode.Parent.Name);
        }
    }
}
