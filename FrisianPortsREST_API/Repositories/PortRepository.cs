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

        public async Task<Port> GetById(int portId)
        {
            const string query = @$"SELECT * FROM Port
                                    WHERE PORT_ID = @portId;";
            var port = await connection.QuerySingleAsync<Port>(query,
                new {
                    portId = portId
                });
            connection.Close();
            return port;
        }

        public async Task<int> Add(Port portToAdd)
        {
            const string query = $@"INSERT INTO Port
                                    (PORT_NAME, PORT_LOCATION, LATITUDE, LONGITUDE)
                                    VALUES (@PortName,@PortLocation, @Latitude, @Longitude);";

            int success = await connection.ExecuteAsync(query, 
                new { 
                    PortName = portToAdd.Port_Name,
                    PortLocation = portToAdd.Port_Location,
                    Latitude = portToAdd.Latitude,
                    Longitude = portToAdd.Longitude
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
                                        LONGITUDE = @Longitude
                                    WHERE PORT_ID = @PortId";

            int success = await connection.ExecuteAsync(query,
                new {
                    PortName = portToUpdate.Port_Name,
                    PortLocation = portToUpdate.Port_Location,
                    Latitude = portToUpdate.Latitude,
                    Longitude = portToUpdate.Longitude,
                    PortId = portToUpdate.Port_Id
                });
            connection.Close();
            return success;
        }
    }
}
