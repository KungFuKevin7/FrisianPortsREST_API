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
                                        SELECT MAX(TRANSPORT_ID) FROM Transport";

                int idOfCreatedTransport = await connection.ExecuteScalarAsync<int>(addQuery,
                   new
                   {
                       CargoTransportId = cargoTransportToAdd.CargoTransportId,
                       DepartureDate = cargoTransportToAdd.DepartureDate,
                       CargoDescription = cargoTransportToAdd.CargoDescription,
                       WeightInTonnes = cargoTransportToAdd.WeightInTonnes,
                       CargoTypeId = cargoTransportToAdd.CargoTypeId
                   });
                connection.Close();
                return idOfCreatedTransport;
            }
        }
        public async Task<int> Add(CargoTransportDTO[] cargoToAdd)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();

                string query = @$"INSERT INTO Cargo 
                             (CARGO_DESCRIPTION, WEIGHT_IN_TONNES, CARGO_TYPE_ID, TRANSPORT_ID)
                             VALUES (@cargoDescription, @weightInTonnes, @cargoTypeId, @transportId);";


                int newlyCreatedTransportId = await Add(cargoToAdd[0]);

                int addedCargo = 0;
                for (int i = 1; i < cargoToAdd.Length; i++)
                {
                    await connection.ExecuteScalarAsync<int>(query,
                        new
                        {
                            cargoDescription = cargoToAdd[i].CargoDescription,
                            weightInTonnes = cargoToAdd[i].WeightInTonnes,
                            cargoTypeId = cargoToAdd[i].CargoTypeId,
                            transportId = newlyCreatedTransportId
                        });
                    addedCargo++;
                }
                connection.Close();
                return addedCargo;
            }
        }
    }
}
