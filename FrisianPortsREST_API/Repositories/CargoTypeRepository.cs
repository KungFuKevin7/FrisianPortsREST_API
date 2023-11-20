using Dapper;
using FrisianPortsREST_API.Models;
using MySql.Data.MySqlClient;

namespace FrisianPortsREST_API.Repositories
{
    public class CargoTypeRepository : IRepository<CargoType>
    {

        public async Task<int> Add(CargoType cargoTypeToAdd)
        {
            using (var connection = DBConnection.getConnection())
            {
                connection.Open();
                const string addQuery = @$"INSERT INTO CARGOTYPE
                            (CARGO_TYPE_NAME)
                            VALUES
                            (@CargoTypeName);
                            SELECT LAST_INSERT_ID();";

                int idNewItem = await connection.ExecuteScalarAsync<int>(addQuery,
                   new
                   {
                       CargoTypeName = cargoTypeToAdd.Cargo_Type_Name
                   });

                return idNewItem;
            }
        }

        public int Delete(int idOfCargoType)
        {
            using (var connection = DBConnection.getConnection())
            {
                connection.Open();
                const string query = @$"DELETE FROM CARGOTYPE
                                       WHERE CARGO_TYPE_ID = @CargoTypeId";

                int success = connection.Execute(query,
                    new
                    {
                        CargoTypeId = idOfCargoType
                    });
                connection.Close();
                return success;
            }
        }

        public async Task<List<CargoType>> Get()
        {
            using (var connection = DBConnection.getConnection())
            {
                connection.Open();
                const string getQuery = @$"SELECT * FROM CARGOTYPE";

                var list = await connection.QueryAsync<CargoType>(getQuery);

                return list.ToList();
            }
        }

        public async Task<CargoType> GetById(int idOfItem)
        {
            using (var connection = DBConnection.getConnection())
            {
                connection.Open();
                const string query = @$"SELECT * FROM CARGOTYPE
                                    WHERE CARGO_TYPE_ID = @CargoTypeId";

                var cargoType = await connection.QuerySingleOrDefaultAsync<CargoType>(query,
                    new
                    {
                        CargoTypeId = idOfItem
                    });

                return cargoType;
            }
        }

        public async Task<int> Update(CargoType itemToUpdate)
        {
            using (var connection = DBConnection.getConnection())
            {
                connection.Open();
                const string updateQuery = @$"UPDATE CARGOTYPE
                                       SET 
                                       CARGO_TYPE_NAME = @CargoTypeName
                                       WHERE CARGO_TYPE_ID = @CargoTypeId";

                int success = await connection.ExecuteAsync(updateQuery,
                   new
                   {
                       CargoTypeName = itemToUpdate.Cargo_Type_Name,
                       CargoTypeId = itemToUpdate.Cargo_Type_Id,
                   });
                connection.Close();

                return success;
            }
        }
    }
}
