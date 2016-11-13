using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using SQLTableClassGenerator.TableElements;
using SQLTableClassGenerator.TableElements.Builders.Interfaces;

namespace SQLTableClassGenerator.DataAccess
{
    public class ConnectionHandler : IConnectionHandler, IHasDatabases
    {
        private readonly ExcludedDatabaseNameCollection _excludedDatabaseNames;
        private readonly IDatabaseBuilder _databaseBuilder;

        public ConnectionHandler(
            ExcludedDatabaseNameCollection excludedDatabaseNames,
            IDatabaseBuilder databaseBuilder)
        {
            _excludedDatabaseNames = excludedDatabaseNames;
            _databaseBuilder = databaseBuilder;
        }

        private static string defaultServer = ".\\sqlexpress";

        private string server;

        private List<Database> _databases;

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

        public List<Database> Databases
        {
            get
            {
                if(_databases != null)
                {
                    return _databases;
                }

                using (var conn = GetConnection())
                {
                    conn.Open();

                    _databases = conn
                        .GetSchema("Databases")
                        .AsEnumerable()
                        .Where(row => !_excludedDatabaseNames.Contains(row[0].ToString()))
                        .OrderBy(o => o[0])
                        .Select(row => _databaseBuilder.Build(row[0].ToString(), conn))
                        .ToList();
                }

                return _databases;
            }
        }
    }
}
