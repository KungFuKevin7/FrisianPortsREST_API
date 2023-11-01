using Dapper;
using FrisianPortsREST_API.Models;
using MySql.Data.MySqlClient;

namespace FrisianPortsREST_API.Repositories
{
    public class UserRepository : IRepository<Users>
    {

        public static MySqlConnection connection = DBConnection.getConnection();

        public async Task<int> Add(Users itemToAdd)
        {
            const string addQuery = @$"INSERT INTO USERS
                            (EMAIL, PASSWORD, FIRSTNAME, SURNAME, PERMISSION_ADD_CARGO)
                            VALUES
                            (@Email, @Password, @FirstName, @SurName, @PermissionAddCargo);
                            SELECT LAST_INSERT_ID()";

            int idNewUser = await connection.ExecuteAsync(addQuery,
               new
               {
                   Email = itemToAdd.Email,
                   Password = itemToAdd.Password,
                   FirstName = itemToAdd.FirstName,
                   SurName = itemToAdd.SurName,
                   PermissionAddCargo = itemToAdd.Permission_Add_Cargo
               });

            connection.Close();
            return idNewUser;
        }

        public int Delete(int itemToRemove)
        {
            const string query = @$"DELETE FROM USERS
                                    WHERE USER_ID = @UserId;";

            int success = connection.Execute(query,
                new
                {
                    UserId = itemToRemove
                });
            connection.Close();
            return success;
        }

        public async Task<List<Users>> Get()
        {
            const string getQuery = @$"SELECT * FROM USERS;";

            var list = await connection.QueryAsync<Users>(getQuery);
            connection.Close();
            return list.ToList();
        }

        public async Task<Users> GetById(int idOfItem)
        {
            const string getQuery = @$"SELECT * FROM USERS
                                       WHERE USER_ID = @UserId;";

            var user = await connection.QuerySingleAsync<Users>(getQuery, 
                new {
                    UserId = idOfItem
                });

            connection.Close();
            return user;
        }

        public async Task<int> Update(Users userUpdate)
        {
            const string updateQuery = @$"UPDATE USERS
                                       SET 
                                       EMAIL = @Email,
                                       PASSWORD = @Password,
                                       FIRSTNAME = @FirstName,
                                       SURNAME = @SurName,
                                       PERMISSION_ADD_CARGO = @Permission
                                       WHERE USER_ID = @UserId;";

            int success = await connection.ExecuteAsync(updateQuery,
               new
               {
                   Email = userUpdate.Email,
                   Password = userUpdate.Password,
                   FirstName = userUpdate.FirstName,
                   SurName = userUpdate.SurName,
                   Permission = userUpdate.Permission_Add_Cargo,
                   UserId = userUpdate.User_Id
               });

            connection.Close();

            return success;
        }
    }
}
