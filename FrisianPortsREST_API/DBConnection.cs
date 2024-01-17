using MySql.Data.MySqlClient;
using System.Data;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace FrisianPortsREST_API
{
    public class DBConnection
    {

        public DBConnection() { }

        /// <summary>
        /// Establish database connection using connectionstring 
        /// found in config files
        /// </summary>
        /// <returns>Mysqlconnection to execute queries with</returns>
        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
        }

        /// <summary>
        /// Establish database connection using connectionstring parameter
        /// </summary>
        /// <param name="connectionString">
        /// connectionstring to establish connection
        /// </param>
        /// <returns>Mysqlconnection to execute queries with</returns>
        public static MySqlConnection GetConnection(string connectionString)
        {
            return new MySqlConnection(connectionString);
        }

    }
}
