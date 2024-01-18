using Dapper;
using FrisianPortsREST_API.DTO;

namespace FrisianPortsREST_API.Repositories
{
    public class CargoTransportRouteRepository
    {
        public async Task<int> Add(CargoTransportRouteDTO cargoTransportRoute) 
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();

                int alreadyExist = checkRouteExists(cargoTransportRoute);
                string addQuery = $@"INSERT INTO ROUTE (DEPARTURE_PORT_ID, ARRIVAL_PORT_ID) 
                                    VALUES (@DeparturePortId,@ArrivalPortId);

                                    INSERT INTO cargotransport
                                    (FREQUENCY, DATE_STARTED, ADDED_BY_ID, ROUTE_ID)
                                    VALUES
                                    (@Frequency,@DateStarted,@AddedById,(SELECT LAST_INSERT_ID()));";

                if (alreadyExist != 0) 
                {
                    addQuery = $@"INSERT INTO cargotransport
                    (FREQUENCY, DATE_STARTED, ADDED_BY_ID, ROUTE_ID)
                    VALUES
                    (@Frequency,@DateStarted,@AddedById,@RouteId);";
                }

                int idOfCreated = await connection.ExecuteScalarAsync<int>(addQuery,
                   new
                   {
                       DeparturePortId = cargoTransportRoute.Departure_Port_Id,
                       ArrivalPortId = cargoTransportRoute.Arrival_Port_Id,
                       Frequency = cargoTransportRoute.Frequency,
                       DateStarted = cargoTransportRoute.Date_Started,
                       AddedById = cargoTransportRoute.Added_By_Id,
                       RouteId = alreadyExist
                   });
                connection.Close();
                return idOfCreated;
            }        
        }

        public int checkRouteExists(CargoTransportRouteDTO cargoTransportRoute) 
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();

                const string addQuery = @$"SELECT ROUTE_ID FROM ROUTE";

                QueryBuilder queryBuilder = new QueryBuilder(addQuery);
                queryBuilder.AddFilter("DEPARTURE_PORT_ID = @DeparturePortId");
                queryBuilder.AddFilter("ARRIVAL_PORT_ID = @ArrivalPortId");

                int idOfCreated = connection.ExecuteScalar<int>(queryBuilder.Build(),
                   new
                   {
                       DeparturePortId = cargoTransportRoute.Departure_Port_Id,
                       ArrivalPortId = cargoTransportRoute.Arrival_Port_Id,
                   });
                connection.Close();
                return idOfCreated;
            }
        }
    }
}
