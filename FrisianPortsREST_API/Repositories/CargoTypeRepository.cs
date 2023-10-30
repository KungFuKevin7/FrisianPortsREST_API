using Dapper;
using FrisianPortsREST_API.Models;
using MySql.Data.MySqlClient;

namespace FrisianPortsREST_API.Repositories
{
    public class CargoTypeRepository : IRepository<CargoType>
    {

        public static MySqlConnection connection = DBConnection.getConnection();

        public async Task<int> Add(CargoType cargoTypeToAdd)
        {
            const string addQuery = @$"INSERT INTO CARGO_TYPE
                            (CARGO_TYPE_ID, CARGO_TYPE_NAME)
                            VALUES
                            (@CargoTypeId, @CargoTypeName)";

            int success = await connection.ExecuteAsync(addQuery,
               new
               {
                   CargoTypeId = cargoTypeToAdd.Cargo_Type_Id,
                   CargoTypeName = cargoTypeToAdd.Cargo_Type_Name
               });
            connection.Close();
            return success;
        }

        public int Delete(int idOfCargoType)
        {
            const string query = @$"DELETE FROM CARGO_TYPE
                                       WHERE CARGO_TYPE_ID = @CargoTypeId";

            int success = connection.Execute(query,
                new
                {
                    CargoTypeId = idOfCargoType
                });
            connection.Close();
            return success;
        }

        public async Task<List<CargoType>> Get()
        {
            const string getQuery = @$"SELECT * FROM CARGO_TYPE";

            var list = await connection.QueryAsync<CargoType>(getQuery);
            connection.Close();
            return list.ToList();
        }

        public async Task<CargoType> GetById(int idOfItem)
        {
            const string query = @$"SELECT * FROM CARGO_TYPE
                                    WHERE CARGO_TYPE_ID = @CargoTypeId";

            var cargoType = await connection.QuerySingleAsync<CargoType>(query,
                new {
                    CargoTypeId = idOfItem
                });
            connection.Close();
            return cargoType;
        }

        public async Task<int> Update(CargoType itemToUpdate)
        {
            const string updateQuery = @$"UPDATE CARGO_TYPE
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
