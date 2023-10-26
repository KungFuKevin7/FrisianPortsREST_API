using Dapper;
using FrisianPortsREST_API.Models;
using MySql.Data.MySqlClient;

namespace FrisianPortsREST_API.Repositories
{
    public class PortRepository : IRepository<Port>
    {

        public static MySqlConnection connection = DBConnection.getConnection();
        
        public async Task<List<Port>> Get() 
        {
            const string query = "SELECT * FROM Port;";
            var list = await connection.QueryAsync<Port>(query);
            connection.Close();
            return list.ToList();
        }

        public async Task<List<Port>> GetById(int portId)
        {
            const string query = @$"SELECT * FROM Port
                                    WHERE PORT_ID = @portId;";
            var list = await connection.QueryAsync<Port>(query,
                new {
                    portId = portId
                });
            connection.Close();
            return list.ToList();
        }

        public async Task<int> Add(Port portToAdd)
        {
            const string query = $@"INSERT INTO Port
                                    (PORT_NAME, PORT_LOCATION, LATITUDE, LONGTITUDE)
                                    VALUES (@PortName,@PortLocation, @Latitude, @Longtitude);";

            int success = await connection.ExecuteAsync(query, 
                new { 
                    PortName = portToAdd.Port_Name,
                    PortLocation = portToAdd.Port_Location,
                    Latitude = portToAdd.Latitude,
                    Longtitude = portToAdd.Longtitude
                });
            connection.Close();
            return success;
        }

        public int Delete(int idOfPort)
        {
            const string query = $@"DELETE FROM Port
                                    WHERE PORT_ID = @PortId";

            int success = connection.Execute(query,
                new
                {
                    PortId = idOfPort
                });
            connection.Close();
            return success;
        }

        public async Task<int> Update(Port portToUpdate)
        {
            const string query = $@"UPDATE Port
                                    SET PORT_NAME = @PortName,
                                        PORT_LOCATION = @PortLocation,
                                        LATITUDE = @Latitude,
                                        LONGTITUDE = @Longtitude
                                    WHERE PORT_ID = @PortId";

            int success = await connection.ExecuteAsync(query,
                new {
                    PortName = portToUpdate.Port_Name,
                    PortLocation = portToUpdate.Port_Location,
                    Latitude = portToUpdate.Latitude,
                    Longtitude = portToUpdate.Longtitude,
                    PortId = portToUpdate.Port_Id
                });
            connection.Close();
            return success;
        }
    }
}
