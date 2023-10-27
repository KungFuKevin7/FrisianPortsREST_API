using Dapper;
using FrisianPortsREST_API.Models;
using MySql.Data.MySqlClient;
using Route = FrisianPortsREST_API.Models.Route;

namespace FrisianPortsREST_API.Repositories
{
    public class RouteRepository : IRepository<Route>
    {

        public static MySqlConnection connection = DBConnection.getConnection();

        public async Task<int> Add(Route routeToAdd)
        {
            const string addQuery = @$"INSERT INTO ROUTE
                            (DEPARTURE_PORT_ID, ARRIVAL_PORT_ID)
                            VALUES
                            (@DeparturePortId, @ArrivalPortId);";

            int success = await connection.ExecuteAsync(addQuery,
               new
               {
                   DeparturePortId = routeToAdd.Departure_Port_Id,
                   ArrivalPortId = routeToAdd.Arrival_Port_Id
               });
            connection.Close();
            return success;
        }

        public int Delete(int itemToRemove)
        {
            const string query = @$"DELETE FROM ROUTE
                                    WHERE ROUTE_ID = @RouteId;";

            int success = connection.Execute(query,
                new
                {
                    RouteId = itemToRemove
                });
            connection.Close();
            return success;
        }

        public async Task<List<Route>> Get()
        {
            const string getQuery = @$"SELECT * FROM ROUTE;";

            var list = await connection.QueryAsync<Route>(getQuery);
            connection.Close();
            return list.ToList();
        }

        public async Task<List<Route>> GetById(int idOfItem)
        {
            const string getQuery = @$"SELECT * FROM ROUTE
                                       WHERE ROUTE_ID = @RouteId;";

            var list = await connection.QueryAsync<Route>(getQuery,
                new { 
                    RouteId = idOfItem
                });
            connection.Close();
            return list.ToList();
        }

        public async Task<int> Update(Route itemToUpdate)
        {
            const string updateQuery = @$"UPDATE ROUTE
                                       SET 
                                       DEPARTURE_PORT_ID = @DeparturePortId,
                                       ARRIVAL_PORT_ID = @ArrivalPortId
                                       WHERE ROUTE_ID = @RouteId";

            int success = await connection.ExecuteAsync(updateQuery,
               new
               {
                   DeparturePortId = itemToUpdate.Departure_Port_Id,
                   ArrivalPortId = itemToUpdate.Arrival_Port_Id,
                   RouteId = itemToUpdate.Route_Id
               });
            connection.Close();

            return success;
        }
    }
}
