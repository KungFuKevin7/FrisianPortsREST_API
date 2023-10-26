using MySql.Data.MySqlClient;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace FrisianPortsREST_API
{
    public class DBConnection
    {

        public DBConnection() { }

        public static MySqlConnection getConnection()
        {
            return new MySqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
        }

    }
}
