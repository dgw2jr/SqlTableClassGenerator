using System;
using System.ComponentModel;
using System.Windows.Forms;
using SQLTableClassGenerator.Interfaces;
using ClassGeneration.Properties;
using DataAccess;

namespace SQLTableClassGenerator
{
    public partial class MainForm : Form
    {
        private readonly IConnectionHandler _connectionHandler;
        private readonly IPopulator _treeViewPopulator;
        private readonly ITreeNodeClassGenerator _treeNodeClassGenerator;

        public MainForm(
            IConnectionHandler connectionHandler,
            IPopulator treePopulator,
            ITreeNodeClassGenerator treeNodeClassGenerator,
            TreeView tree)
        {
            treeView1 = tree;
            _connectionHandler = connectionHandler;
            _treeViewPopulator = treePopulator;
            _treeNodeClassGenerator = treeNodeClassGenerator;

            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
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
            if (e.Node.Parent == null || e.Node.Level == 0)
                return;

            richTextBox1.Text = _treeNodeClassGenerator.Generate(e.Node.Name, e.Node.Parent.Name);
        }
        
        private void generateConstructorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = _treeNodeClassGenerator.Generate(treeView1.SelectedNode, () => Settings.Default.GenerateConstructor = ((ToolStripMenuItem)sender).Checked);
        }

        private void sealClassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = _treeNodeClassGenerator.Generate(treeView1.SelectedNode, () => Settings.Default.IsSealed = ((ToolStripMenuItem)sender).Checked);
        }

        private void privateSettersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = _treeNodeClassGenerator.Generate(treeView1.SelectedNode, () => Settings.Default.PrivateSetters = ((ToolStripMenuItem)sender).Checked);
        }
    }
}