using Dapper;
using FrisianPortsREST_API.Models;
using MySql.Data.MySqlClient;
using Route = FrisianPortsREST_API.Models.Route;

namespace FrisianPortsREST_API.Repositories
{
    public class RouteRepository : IRepository<Route>
    {

        public async Task<int> Add(Route routeToAdd)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                const string addQuery = @$"INSERT INTO ROUTE
                            (DEPARTURE_PORT_ID, ARRIVAL_PORT_ID)
                            VALUES
                            (@DeparturePortId, @ArrivalPortId);
                            SELECT LAST_INSERT_ID();";

                int newId = await connection.ExecuteScalarAsync<int>(addQuery,
                   new
                   {
                       DeparturePortId = routeToAdd.DeparturePortId,
                       ArrivalPortId = routeToAdd.ArrivalPortId
                   });

                return newId;
            }
        }

        public int Delete(int itemToRemove)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                const string query = @$"DELETE FROM ROUTE
                                    WHERE ROUTE_ID = @RouteId;";

                int success = connection.Execute(query,
                    new
                    {
                        RouteId = itemToRemove
                    });
                return success;
            }
        }

        public async Task<List<Route>> Get()
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                const string getQuery = @$"SELECT * FROM ROUTE;";

                var list = await connection.QueryAsync<Route>(getQuery);

                return list.ToList();
            }
        }

        public async Task<Route> CheckCombinationExists(int? departPort, int? arrivalPort)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                const string getQuery = @$"SELECT * FROM ROUTE
                                           WHERE DEPARTURE_PORT_ID = @DepartPort
                                           AND ARRIVAL_PORT_ID = @ArrivalPort;";

                var exists = await connection.QuerySingleOrDefaultAsync<Route>(getQuery, new { 
                    DepartPort = departPort,
                    ArrivalPort = arrivalPort
                });

                return exists;
            }
        }

        public async Task<Route> GetById(int idOfItem)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                const string getQuery = @$"SELECT * FROM ROUTE
                                       WHERE ROUTE_ID = @RouteId;";

                var route = await connection.QuerySingleOrDefaultAsync<Route>(getQuery,
                    new
                    {
                        RouteId = idOfItem
                    });

                return route;
            }
        }

        public async Task<int> Update(Route itemToUpdate)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                const string updateQuery = @$"UPDATE ROUTE
                                       SET 
                                       DEPARTURE_PORT_ID = @DeparturePortId,
                                       ARRIVAL_PORT_ID = @ArrivalPortId
                                       WHERE ROUTE_ID = @RouteId";

                int success = await connection.ExecuteAsync(updateQuery,
                   new
                   {
                       DeparturePortId = itemToUpdate.DeparturePortId,
                       ArrivalPortId = itemToUpdate.ArrivalPortId,
                       RouteId = itemToUpdate.RouteId
                   });

                return success;
            }
        }
    }
}
