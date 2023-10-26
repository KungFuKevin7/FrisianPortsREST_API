using Dapper;
using FrisianPortsREST_API.Models;
using MySql.Data.MySqlClient;

namespace FrisianPortsREST_API.Repositories
{
    public class CargoRepository : IRepository<Cargo>
    {
        public static MySqlConnection connection = DBConnection.getConnection();

        public async Task<int> Add(Cargo cargoToAdd)
        {
            string query = @$"INSERT INTO Cargo 
                             (CARGO_DESCRIPTION, WEIGHT_IN_TONNES, CARGO_TYPE_ID, TRANSPORT_ID)
                             VALUES (@cargoDescription, @weightInTonnes, @cargoTypeId, @transportId)";

            int success = await connection.ExecuteAsync(query,
                new
                {
                    cargoDescription = cargoToAdd.Cargo_Description,
                    weightInTonnes = cargoToAdd.Weight_In_Tonnes,
                    cargoTypeId = cargoToAdd.Cargo_Type_Id,
                    transportId = cargoToAdd.Transport_Id
                });
            connection.Close();
            return success;
        }

        public int Delete(int idOfCargoToRemove)
        {
            const string query = $@"DELETE FROM Cargo
                                    WHERE CARGO_ID = @cargoId";

            int success = connection.Execute(query,
                new
                {
                    cargoId = idOfCargoToRemove
                });
            connection.Close();
            return success;
        }

        public async Task<List<Cargo>> Get()
        {
            const string query = "SELECT * FROM Cargo;";
            var list = await connection.QueryAsync<Cargo>(query);
            connection.Close();
            return list.ToList();
        }

        public async Task<List<Cargo>> GetById(int idOfCargo)
        {
            const string query = @$"SELECT * FROM Cargo
                                    WHERE CARGO_ID = @cargoId";
            var list = await connection.QueryAsync<Cargo>(query,
                new { 
                    cargoId = idOfCargo
                });
            connection.Close();
            return list.ToList();
        }

        public async Task<int> Update(Cargo cargoToUpdate)
        {
            const string updateQuery = @$"UPDATE Cargo
                                         SET 
                                         CARGO_DESCRIPTION = @cargoDescription,
                                         WEIGHT_IN_TONNES = @weightInTonnes,
                                         CARGO_TYPE_ID = @cargoTypeId,
                                         TRANSPORT_ID = @transportId
                                         WHERE CARGO_ID = @cargoId";

            int success = await connection.ExecuteAsync(updateQuery,
                new { 
                    cargoDescription = cargoToUpdate.Cargo_Description,
                    weightInTonnes = cargoToUpdate.Weight_In_Tonnes,
                    cargoTypeId = cargoToUpdate.Cargo_Type_Id,
                    transportId = cargoToUpdate.Transport_Id,
                    cargoId = cargoToUpdate.Cargo_Id
                });
            connection.Close();
            return success;
        }
    }
}
