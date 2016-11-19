using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using DataAccess.Properties;

namespace DataAccess
{
    public class ConnectionHandler : IConnectionSetter, ISQLConnectionResource
    {
        public void SetConnection()
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("SQL Server to connect to:", "Connect", Settings.Default.Server);
            if (input.Length == 0) Environment.Exit(1);

            using (var conn = new SqlConnection(string.Format("data source={0};integrated security=true;", input)))
            {
                try
                {
                    conn.Open();
                    Settings.Default.Server = input;
                }
                catch
                {
                    MessageBox.Show(string.Format("Could not connect to {0}", input));
                    SetConnection();
                }
            }
        }
        
        public T Invoke<T>(Func<SqlConnection, T> action)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                return action(connection);
            }
        }

        public T Execute<T>(string databaseName, string commandText, Func<DataTable, T> selector)
        {
            return Invoke(conn =>
            {
                conn.ChangeDatabase(databaseName);

                var cmd = conn.CreateCommand();
                cmd.CommandText = commandText;

                var dt = new DataTable();

                new SqlDataAdapter(cmd).Fill(dt);

                return selector(dt);
            });
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(GetConnectionString());
        }

        private string GetConnectionString()
        {
            return string.Format("data source={0};integrated security=true;", Settings.Default.Server);
        }
    }
}
