using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SQLTableClassGenerator
{
    public partial class Form1 : Form
    {
        private static string server = ".\\sqlexpress";

        public Form1()
        {
            InitializeComponent();
        }

        public string GetConnectionString()
        {
            return string.Format("data source={0};integrated security=true;", server);
        }

        public void SetConnection()
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("SQL Server to connect to:", "Connect", server);
            if (input.Length == 0) Environment.Exit(1);

            using (var conn = new SqlConnection(string.Format("data source={0};integrated security=true;", input)))
            {
                try
                {
                    conn.Open();
                    server = input;
                }
                catch
                {
                    MessageBox.Show(string.Format("Could not connect to {0}", input));
                    SetConnection();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetConnection();

            BackgroundWorker bgw = new BackgroundWorker();
            bgw.DoWork += new DoWorkEventHandler(bgw_DoWork);
            bgw.RunWorkerAsync();
        }

        private void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            FillTree();
        }

        private delegate void AddDbNode(string Name);
        private void Add(string Name)
        {
            treeView1.Nodes.Add(Name, Name);
        }

        private delegate void AddTblNode(string dbName, string tblName);
        private void Add(string dbName, string tblName)
        {
            treeView1.Nodes[dbName].Nodes.Add(tblName, tblName);
        }

        private void FillTree()
        {
            var skipDb = new []
            {
                "master",
                "model",
                "tempdb",
                "msdb"
            };

            using (var conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();

                var databases = conn.GetSchema("Databases").AsEnumerable().OrderBy(o => o[0]);

                foreach (var dbRow in databases.Where(row => !skipDb.Contains(row[0].ToString())))
                {
                    var dbName = dbRow[0].ToString();
                    conn.ChangeDatabase(dbName);
                    var tables = conn.GetSchema("Tables").AsEnumerable().OrderBy(o => o[2]);
                    
                    // add database node
                    treeView1.Invoke(new AddDbNode(Add), dbName);

                    // add tables for database nodes
                    foreach (DataRow row in tables)
                    {
                        var tableName = row[2].ToString();
                        treeView1.Invoke(new AddTblNode(Add), dbName, tableName);
                    }
                }
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level == 0) return;
           
            using (var conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();
                conn.ChangeDatabase(e.Node.Parent.Name);
                var columns = conn.GetSchema("Columns", new string[] { e.Node.Parent.Name, null, e.Node.Name }).AsEnumerable().OrderBy(o => o[3]);
                
                IEnumerable<ColumnDef> columnDefs = columns.Select(c => new ColumnDef() { Field = c["column_name"].ToString(), SQLType = c["data_type"].ToString() });

                TableDef t = new TableDef()
                {
                    Name = e.Node.Name,
                    Columns = columnDefs
                };

                richTextBox1.Text = t.ToString();
            }
        }
    }
}
