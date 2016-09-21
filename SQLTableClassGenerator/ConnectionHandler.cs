using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLTableClassGenerator
{
    public class ConnectionHandler : IConnectionHandler
    {
        public static string DefaultServer = ".\\sqlexpress";

        public string Server { get; set; }

        public string GetConnectionString()
        {
            return string.Format("data source={0};integrated security=true;", Server);
        }

        public void SetConnection()
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("SQL Server to connect to:", "Connect", DefaultServer);
            if (input.Length == 0) Environment.Exit(1);

            using (var conn = new SqlConnection(string.Format("data source={0};integrated security=true;", input)))
            {
                try
                {
                    conn.Open();
                    Server = input;
                }
                catch
                {
                    MessageBox.Show(string.Format("Could not connect to {0}", input));
                    SetConnection();
                }
            }
        }
    }
}
