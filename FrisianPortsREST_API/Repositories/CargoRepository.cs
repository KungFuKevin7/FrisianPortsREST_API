using Dapper;
using FrisianPortsREST_API.Models;
using MySql.Data.MySqlClient;

namespace FrisianPortsREST_API.Repositories
{
    public class CargoRepository : IRepository<Cargo>
    {
        ///<inheritdoc/>
        public async Task<int> Add(Cargo cargoToAdd)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();

                string query = @$"INSERT INTO Cargo 
                             (CARGO_DESCRIPTION, WEIGHT_IN_TONNES, CARGO_TYPE_ID, TRANSPORT_ID)
                             VALUES (@cargoDescription, @weightInTonnes, @cargoTypeId, @transportId);
                             SELECT LAST_INSERT_ID();";

                int createdId = await connection.ExecuteScalarAsync<int>(query,
                    new
                    {
                        cargoDescription = cargoToAdd.CargoDescription,
                        weightInTonnes = cargoToAdd.WeightInTonnes,
                        cargoTypeId = cargoToAdd.CargoTypeId,
                        transportId = cargoToAdd.TransportId
                    });
                connection.Close(); 
                return createdId;
            }
        }

        public int Delete(int idOfCargoToRemove)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                const string query = $@"DELETE FROM Cargo
                                    WHERE CARGO_ID = @cargoId";

                int success = connection.Execute(query,
                    new
                    {
                        cargoId = idOfCargoToRemove
                    });
                return success;
            }
        }

        public async Task<List<Cargo>> Get()
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();

                const string query = "SELECT * FROM Cargo;";
                var list = await connection.QueryAsync<Cargo>(query);
                connection.Close();
                return list.ToList();
            }
        }

        public async Task<Cargo> GetById(int idOfCargo)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                const string query = @$"SELECT * FROM Cargo
                                    WHERE CARGO_ID = @cargoId";
                var cargo = await connection.QuerySingleOrDefaultAsync<Cargo>(query,
                    new
                    {
                        cargoId = idOfCargo
                    });
                connection.Close();
                return cargo;
            }
        }

        public async Task<int> Update(Cargo cargoToUpdate)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                const string updateQuery = @$"UPDATE Cargo
                                         SET 
                                         CARGO_DESCRIPTION = @cargoDescription,
                                         WEIGHT_IN_TONNES = @weightInTonnes,
                                         CARGO_TYPE_ID = @cargoTypeId,
                                         TRANSPORT_ID = @transportId
                                         WHERE CARGO_ID = @cargoId";

                int success = await connection.ExecuteAsync(updateQuery,
                    new
                    {
                        cargoDescription = cargoToUpdate.CargoDescription,
                        weightInTonnes = cargoToUpdate.WeightInTonnes,
                        cargoTypeId = cargoToUpdate.CargoTypeId,
                        transportId = cargoToUpdate.TransportId,
                        cargoId = cargoToUpdate.CargoId
                    });
                connection.Close();
                return success;
            }
        }



    }
}
