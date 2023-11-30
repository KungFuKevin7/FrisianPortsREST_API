using Dapper;
using FrisianPortsREST_API.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace FrisianPortsREST_API.Repositories
{
    public class PortRepository : IRepository<Port>
    {

        public async Task<List<Port>> Get() 
        {
            using (var connection = DBConnection.GetConnection())
            {
                const string query = "SELECT * FROM Port;";
                var list = await connection.QueryAsync<Port>(query);
                connection.Close();
                return list.ToList();
            }
        }

        public async Task<Port> GetById(int portId)
        {
            using (var connection = DBConnection.GetConnection())
            {
                const string query = @$"SELECT * FROM Port
                                        WHERE PORT_ID = @portId;";
                var port = await connection.QuerySingleOrDefaultAsync<Port>(query,
                    new
                    {
                        portId = portId
                    });
                connection.Close();
                return port;
            }
        }

        public async Task<int> Add(Port portToAdd)
        {
            using (var connection = DBConnection.GetConnection())
            {
                const string query = $@"INSERT INTO Port
                                    (PORT_NAME, PORT_LOCATION, LATITUDE, LONGITUDE)
                                    VALUES (@PortName,@PortLocation, @Latitude, @Longitude);
                                    SELECT LAST_INSERT_ID();";

                int idOfCreatedItem = await connection.ExecuteScalarAsync<int>(query,
                    new
                    {
                        PortName = portToAdd.Port_Name,
                        PortLocation = portToAdd.Port_Location,
                        Latitude = portToAdd.Latitude,
                        Longitude = portToAdd.Longitude
                    });
                connection.Close();
                return idOfCreatedItem;
            }
        }

        public int Delete(int idOfPort)
        {
            using (var connection = DBConnection.GetConnection())
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
        }

        public async Task<int> Update(Port portToUpdate)
        {
            using (var connection = DBConnection.GetConnection())
            {
                const string query = $@"UPDATE Port
                                    SET PORT_NAME = @PortName,
                                        PORT_LOCATION = @PortLocation,
                                        LATITUDE = @Latitude,
                                        LONGITUDE = @Longitude
                                    WHERE PORT_ID = @PortId";

                int success = await connection.ExecuteAsync(query,
                    new
                    {
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

        public async Task<List<Port>> GetPorts(string searchQuery)
        {
            using (var connection = DBConnection.GetConnection())
            {
                string dbQuery = $@"SELECT * FROM Port
                              WHERE Port_Name LIKE @Query
                              OR    Port_Location LIKE @Query";
                
                searchQuery = $"%{searchQuery}%";

                var portResults = await connection.QueryAsync<Port>(dbQuery,
                    new
                    {
                        Query = searchQuery
                    });

                return portResults.ToList();
            }
        }
        public async Task<List<Port>> GetPortsWithFilters(string searchQuery, string[] filters)
        {
            using (var connection = DBConnection.GetConnection())
            {
                string dbQuery = $@"SELECT Port.* FROM Port
                                 INNER JOIN Province Pro
                                 ON Port.PROVINCE_ID = Pro.PROVINCE_ID
                                 WHERE (Port.PORT_LOCATION LIKE @Query
                                 OR    Port.PORT_NAME LIKE @Query)
                                 AND Pro.PROVINCE_NAME IN @Filter";

                searchQuery = $"%{searchQuery}%";

                var portResult = await connection.QueryAsync<Port>(dbQuery,
                new
                {
                    Query = searchQuery,
                    Filter = filters
                });

                return portResult.ToList();
            }
        }


        public async Task<Port> GetPortByLocation(string location)
        {
            using (var connection = DBConnection.GetConnection())
            {
                string dbQuery = $@"SELECT * FROM Port
                                    WHERE Port_Location = @Query";

                var portResults = await connection.QuerySingleAsync<Port>(dbQuery,
                    new
                    {
                        Query = location
                    });

                return portResults;
            }
        }

        /// <summary>
        /// Gets List of total import cargo.
        /// </summary>
        /// <param name="idOfPort">Id of requested port</param>
        /// <returns>List of cargoTransports contributing to import</returns>
        public async Task<List<CargoTransport>> GetImportShipsByPortId(int idOfPort)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();

                const string query = $@"SELECT CT.* FROM CargoTransport CT
                                    INNER JOIN Transport T ON CT.CARGO_TRANSPORT_ID = T.CARGO_TRANSPORT_ID
                                    INNER JOIN Route R ON CT.ROUTE_ID = R.ROUTE_ID
                                    WHERE R.DEPARTURE_PORT_ID = @DeparturePortId;";
                var cargoTransports = await connection.QueryAsync<CargoTransport>(query,
                    new
                    {
                        DeparturePortId = idOfPort
                    });

                return cargoTransports.ToList();
            }
        }

        /// <summary>
        /// Gets List of total export cargo.
        /// </summary>
        /// <param name="idOfPort">Id of requested port</param>
        /// <returns>List of cargoTransports contributing to export</returns>
        public async Task<List<CargoTransport>> GetExportShipsByPortId(int idOfPort)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();

                const string query = $@"SELECT CT.* FROM CargoTransport CT
                                    INNER JOIN Transport T ON CT.CARGO_TRANSPORT_ID = T.CARGO_TRANSPORT_ID
                                    INNER JOIN Route R ON CT.ROUTE_ID = R.ROUTE_ID
                                    WHERE R.ARRIVAL_PORT_ID = @ArrivalPortId;";
                var cargoTransports = await connection.QueryAsync<CargoTransport>(query,
                    new
                    {
                        ArrivalPortId = idOfPort
                    });
                //connection.Close();

                return cargoTransports.ToList();
            }
        }

    }
}
