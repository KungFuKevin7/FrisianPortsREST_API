using Dapper;
using FrisianPortsREST_API.Models;
using MySql.Data.MySqlClient;

namespace FrisianPortsREST_API.Repositories
{
    public class TransportRepository : IRepository<Transport>
    {

        public static MySqlConnection connection = DBConnection.getConnection();

        public async Task<int> Add(Transport transportToAdd)
        {
            const string addQuery = @$"INSERT INTO TRANSPORT
                            (CARGO_TRANSPORT_ID, DEPARTURE_DATE)
                            VALUES
                            (@CargoTransportId, @DepartureDate);
                            SELECT LAST_INSERT_ID();";

            int idOfItem = await connection.ExecuteAsync(addQuery,
               new
               {
                   CargoTransportId = transportToAdd.Cargo_Transport_Id,
                   DepartureDate = transportToAdd.Departure_Date
               });
            connection.Close();
            return idOfItem;
        }

        public int Delete(int idToRemove)
        {
            const string query = @$"DELETE FROM TRANSPORT
                                    WHERE TRANSPORT_ID = @TransportId;";

            int success = connection.Execute(query,
                new
                {
                    TransportId = idToRemove
                });
            connection.Close();
            return success;
        }

        public async Task<List<Transport>> Get()
        {
            const string getQuery = @$"SELECT * FROM TRANSPORT;";

            var list = await connection.QueryAsync<Transport>(getQuery);
            connection.Close();
            return list.ToList();
        }

        public async Task<Transport> GetById(int idOfItem)
        {
            const string getQuery = @$"SELECT * FROM TRANSPORT
                                       WHERE TRANSPORT_ID = @TransportId;";

           // try
            //{
                var transport = await connection.QuerySingleAsync<Transport>(getQuery,
                    new { 
                        TransportId = idOfItem
                    }); 
                connection.Close();
                return transport;
            //}
            //catch (Exception)
            //{
            //    return;
            //}


        }

        public async Task<int> Update(Transport itemToUpdate)
        {
            const string updateQuery = @$"UPDATE TRANSPORT
                                       SET 
                                       CARGO_TRANSPORT_ID = @CargoTransportId,
                                       DEPARTURE_DATE = @DepartureDate
                                       WHERE TRANSPORT_ID = @TransportId;";

            int success = await connection.ExecuteAsync(updateQuery,
               new
               {
                   CargoTransportId = itemToUpdate.Cargo_Transport_Id,
                   DepartureDate = itemToUpdate.Departure_Date,
                   TransportId = itemToUpdate.Transport_Id
               });
            connection.Close();

            return success;
        }
    }
}
