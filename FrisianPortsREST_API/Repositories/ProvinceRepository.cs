using Dapper;
using FrisianPortsREST_API.Models;

namespace FrisianPortsREST_API.Repositories
{
    public class ProvinceRepository : IRepository<Province>
    {
        public async Task<int> Add(Province newProvince)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                const string addQuery = @$"INSERT INTO Province
                                       (PROVINCE_NAME)
                                       VALUES
                                       (@ProvinceName);
                                       SELECT LAST_INSERT_ID();";

                int idOfCreated = await connection.ExecuteScalarAsync<int>(addQuery,
                   new
                   {
                       ProvinceName = newProvince.Province_Name
                   });
                connection.Close();
                return idOfCreated;
            }
        }

        public int Delete(int provinceId)
        {
            using (var connection = DBConnection.GetConnection()) 
            {
                connection.Open();
                const string query = $@"DELETE FROM Province
                                        WHERE PROVINCE_ID = @ProvinceId";

                int success = connection.Execute(query,
                   new
                   {
                       ProvinceId = provinceId
                   });
                return success;
            }
        }

        public async Task<List<Province>> Get()
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                const string query = @$"SELECT * FROM Province";

                var list = await connection.QueryAsync<Province>(query);
                return list.ToList();
            }
        }

        public async Task<Province> GetById(int provinceId)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                const string query = @$"SELECT * FROM Province
                                    WHERE PROVINCE_ID = @ProvinceId";
                var province = await connection.QuerySingleOrDefaultAsync<Province>(query,
                    new
                    {
                        ProvinceId = provinceId
                    });
                return province;
            }
        }

        public async Task<int> Update(Province updatedProvince)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                const string updateQuery = @$"UPDATE PROVINCE
                                       SET 
                                       PROVINCE_NAME = @ProvinceName
                                       WHERE PROVINCE_ID = @ProvinceId ";

                int success = await connection.ExecuteAsync(updateQuery,
                   new
                   {
                       ProvinceName = updatedProvince.Province_Name,
                       ProvinceId = updatedProvince.Province_Id,
                   });

                return success;
            }
        }
    }
}
