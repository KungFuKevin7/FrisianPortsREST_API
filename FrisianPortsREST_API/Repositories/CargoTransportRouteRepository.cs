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
                const string addQuery = @$"INSERT INTO ROUTE
                                            (DEPARTURE_PORT_ID, ARRIVAL_PORT_ID)
                                            VALUES
                                            (@DeparturePortId,@ArrivalPortId);

                                            INSERT INTO cargotransport
                                            (FREQUENCY, DATE_STARTED, ADDED_BY_ID, ROUTE_ID)
                                            VALUES
                                            (@Frequency,@DateStarted,@AddedById,(SELECT LAST_INSERT_ID()));";

                int idOfCreated = await connection.ExecuteScalarAsync<int>(addQuery,
                   new
                   {
                       DeparturePortId = cargoTransportRoute.Departure_Port_Id,
                       ArrivalPortId = cargoTransportRoute.Arrival_Port_Id,
                       Frequency = cargoTransportRoute.Frequency,
                       DateStarted = cargoTransportRoute.Date_Started,
                       AddedById = cargoTransportRoute.Added_By_Id,
                   });
                connection.Close();
                return idOfCreated;
            }        
        }
    }
}
