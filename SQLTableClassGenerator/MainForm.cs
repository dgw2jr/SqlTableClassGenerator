using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace SQLTableClassGenerator
{
    public partial class MainForm : Form
    {
        private readonly IConnectionHandler _connectionHandler;
        private readonly ITableDefBuilder _tableDefBuilder;
        private readonly ITreeViewPopulator _treeViewPopulator;

        public MainForm(IConnectionHandler connectionHandler, ITableDefBuilder tableDefBuilder, ITreeViewPopulator treePopulator)
        {
            InitializeComponent();
            _connectionHandler = connectionHandler;
            _tableDefBuilder = tableDefBuilder;
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
            if (e.Node.Level == 0) return;

            richTextBox1.Text = _tableDefBuilder.Build(e.Node.Parent.Name, e.Node.Name).ToString();
        }
    }
}
