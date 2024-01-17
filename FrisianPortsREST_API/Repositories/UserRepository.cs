using Dapper;
using FrisianPortsREST_API.DTO;
using FrisianPortsREST_API.Models;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Text;
using ConfigurationManager = System.Configuration.ConfigurationManager;


namespace FrisianPortsREST_API.Repositories
{
    public class UserRepository : IRepository<Users>
    {

        public static MySqlConnection connection = DBConnection.GetConnection();

        public async Task<int> Add(Users itemToAdd)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();

                const string addQuery = @$"INSERT INTO USERS
                            (EMAIL, PASSWORD, FIRSTNAME, SURNAME, PERMISSION_ADD_CARGO)
                            VALUES
                            (@Email, @Password, @FirstName, @SurName, @PermissionAddCargo);
                            SELECT LAST_INSERT_ID()";
                
                int idNewUser = await connection.ExecuteAsync(addQuery,
                   new
                   {
                       Email = itemToAdd.Email,
                       Password = HashPassword(itemToAdd.Password),
                       FirstName = itemToAdd.FirstName,
                       SurName = itemToAdd.SurName,
                       PermissionAddCargo = itemToAdd.PermissionAddCargo
                   });

                return idNewUser;
            }
        }

        public int Delete(int itemToRemove)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
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
        }

        public async Task<List<Users>> Get()
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                const string getQuery = @$"SELECT * FROM USERS;";

                var list = await connection.QueryAsync<Users>(getQuery);
                connection.Close();
                return list.ToList();
            }
        }

        public async Task<Users> GetById(int idOfItem)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                const string getQuery = @$"SELECT * FROM USERS
                                       WHERE USER_ID = @UserId;";

                var user = await connection.QuerySingleOrDefaultAsync<Users>(getQuery,
                    new
                    {
                        UserId = idOfItem
                    });

                return user;
            }
        }

        public async Task<UserDto> GetByIdToDTO(int idOfItem)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                const string getQuery = @$"SELECT * FROM USERS
                                       WHERE USER_ID = @UserId;";

                var user = await connection.QuerySingleOrDefaultAsync<Users>(getQuery,
                    new
                    {
                        UserId = idOfItem
                    });

                return ConvertToUserDTO(user);
            }
        }

        public async Task<int> Update(Users userUpdate)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
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
                       Permission = userUpdate.PermissionAddCargo,
                       UserId = userUpdate.UserId
                   });
             
                return success;
            }
        }

        /// <summary>
        /// Check If email is in use
        /// </summary>
        /// <param name="emailInput">Email provided</param>
        /// <returns>User with corresponding email</returns>
        public async Task<Users> ValidateUser(string emailInput)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                const string getQuery = @$"SELECT * FROM USERS
                                        WHERE EMAIL = @Email;";

                var user = await connection.QuerySingleOrDefaultAsync<Users>(getQuery,
                    new
                    {
                        Email = emailInput
                    });

                return user;
            }
        }

        /// <summary>
        /// Check whether User exists
        /// and whether the combination of email and password is correct
        /// </summary>
        /// <param name="email">email provided by the user</param>
        /// <param name="passwordInput">password provided by the user</param>
        /// <returns>User if exists</returns>
        public UserDto ValidatePassword(string email, string passwordInput) 
        {
            var userInDatabase = ValidateUser(email);

            if (userInDatabase.Result == null)
            {
                return null;
            }
            if (HashPassword(passwordInput) == userInDatabase.Result.Password)
            {
                return ConvertToUserDTO(userInDatabase.Result);
            }
            else 
            {
                return null;
            }
        }

        /// <summary>
        /// Encrypt password using SHA
        /// </summary>
        /// <param name="input">Password input</param>
        /// <returns>Encrypted input</returns>
        public string HashPassword(string input) 
        {
            var key = ConfigurationManager.AppSettings["shaKey"];
            HMACSHA512 HMAC = new HMACSHA512(Encoding.UTF8.GetBytes(key));
            var encodedPassword = HMAC.ComputeHash(Encoding.UTF8.GetBytes(input));

            return Convert.ToBase64String(encodedPassword);
        }

        /// <summary>
        /// Convert User to DTO to hide private information
        /// </summary>
        /// <param name="originalUser">User to be converted</param>
        /// <returns>User without password</returns>
        public UserDto ConvertToUserDTO(Users originalUser) 
        {
            UserDto user = new UserDto();
            user.UserId = originalUser.UserId;
            user.Email = originalUser.Email;
            user.FirstName = originalUser.FirstName;
            user.SurName = originalUser.SurName;
            user.Permission_Add_Cargo = originalUser.PermissionAddCargo;
            return user;
        }
    }
}
