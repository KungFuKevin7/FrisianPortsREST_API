using Dapper;
using FrisianPortsREST_API.DTO;
using FrisianPortsREST_API.Models;

namespace FrisianPortsREST_API.Repositories
{
    public class CargoTransportDTORepository
    {
        public async Task<int> Add(CargoTransportDTO cargoTransportToAdd)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                const string addQuery = @$"INSERT INTO TRANSPORT (CARGO_TRANSPORT_ID, DEPARTURE_DATE)
                                        VALUES (@CargoTransportId, @DepartureDate);

                                        INSERT INTO CARGO (CARGO_DESCRIPTION, WEIGHT_IN_TONNES, CARGO_TYPE_ID, TRANSPORT_ID)
                                        VALUES (@CargoDescription, @WeightInTonnes, @CargoTypeId, (SELECT LAST_INSERT_ID()));
                                        SELECT LAST_INSERT_ID()";

                int idOfCreated = await connection.ExecuteScalarAsync<int>(addQuery,
                   new
                   {
                       CargoTransportId = cargoTransportToAdd.CargoTransportId,
                       DepartureDate = cargoTransportToAdd.DepartureDate,
                       CargoDescription = cargoTransportToAdd.CargoDescription,
                       WeightInTonnes = cargoTransportToAdd.WeightInTonnes,
                       CargoTypeId = cargoTransportToAdd.CargoTypeId
                   });
                connection.Close();
                return idOfCreated;
            }
        }
    }
}
