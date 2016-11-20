using System;
using System.Windows.Forms;
using SQLTableClassGenerator.Interfaces;
using SQLTableClassGenerator.Properties;
using DataAccess;
using System.Threading.Tasks;

namespace SQLTableClassGenerator
{
    public partial class MainForm : Form
    {
        private readonly IConnectionSetter _connectionHandler;
        private readonly IPopulator _treeViewPopulator;
        private readonly ITreeNodeClassGenerator _treeNodeClassGenerator;

        public MainForm(
            IConnectionSetter connectionHandler,
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

            Task.Run(() => _treeViewPopulator.Populate());
        }
        
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Parent == null || e.Node.Level == 0)
                return;

            SetTextBoxText();
        }
        
        private void generateConstructorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetTextBoxText(() => Settings.Default.GenerateConstructor = ((ToolStripMenuItem)sender).Checked);
        }

        private void sealClassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetTextBoxText(() => Settings.Default.IsSealed = ((ToolStripMenuItem)sender).Checked);
        }

        private void privateSettersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetTextBoxText(() => Settings.Default.PrivateSetters = ((ToolStripMenuItem)sender).Checked);
        }

        private void SetTextBoxText(Action action = null)
        {
            richTextBox1.Text = _treeNodeClassGenerator.Generate(treeView1.SelectedNode, action);
        }
    }
}