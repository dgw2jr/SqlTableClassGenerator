using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SQLTableClassGenerator.DataAccess
{
    public class ConnectionHandler : IConnectionHandler
    {
        private static string defaultServer = ".\\sqlexpress";

        private string server;

        private string GetConnectionString()
        {
            return string.Format("data source={0};integrated security=true;", server);
        }

        public void SetConnection()
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("SQL Server to connect to:", "Connect", defaultServer);
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

        public DbConnection GetConnection()
        {
            return new SqlConnection(GetConnectionString());
        }
    }
}
