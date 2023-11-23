using MySql.Data.MySqlClient;
using System.Data;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace FrisianPortsREST_API
{
    public class DBConnection
    {

        public DBConnection() { }

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
        }

        public static MySqlConnection GetConnection(string connectionString)
        {
            return new MySqlConnection(connectionString);
        }

    }
}
