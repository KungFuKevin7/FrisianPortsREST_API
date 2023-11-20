using Dapper;
using FrisianPortsREST_API.Models;
using MySql.Data.MySqlClient;

namespace FrisianPortsREST_API.Repositories
{
    public class TransportRepository : IRepository<Transport>
    {

        public async Task<int> Add(Transport transportToAdd)
        {
            using (var connection = DBConnection.getConnection())
            {
                connection.Open();
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

                return idOfItem;
            }
        }

        public int Delete(int idToRemove)
        {
            using (var connection = DBConnection.getConnection())
            {
                connection.Open();
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
        }

        public async Task<List<Transport>> Get()
        {
            using (var connection = DBConnection.getConnection())
            {
                connection.Open();
                const string getQuery = @$"SELECT * FROM TRANSPORT;";

                var list = await connection.QueryAsync<Transport>(getQuery);

                return list.ToList();
            }
        }

        public async Task<Transport> GetById(int idOfItem)
        {
            using (var connection = DBConnection.getConnection())
            {
                connection.Open();
                const string getQuery = @$"SELECT * FROM TRANSPORT
                                       WHERE TRANSPORT_ID = @TransportId;";

                var transport = await connection.QuerySingleOrDefaultAsync<Transport>(getQuery,
                    new
                    {
                        TransportId = idOfItem
                    });

                return transport;
            }

        }

        public async Task<int> Update(Transport itemToUpdate)
        {
            using (var connection = DBConnection.getConnection())
            {
                connection.Open();
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
      
                return success;
            }
        }

        public async Task<int> GetCountInCargoTransport(int idOfItem)
        {
            using (var connection = DBConnection.getConnection())
            {
                connection.Open();
                const string getQuery = @$"SELECT COUNT(*) FROM cargotransport CT
                                            INNER JOIN transport t on CT.CARGO_TRANSPORT_ID = t.CARGO_TRANSPORT_ID
                                            WHERE CT.CARGO_TRANSPORT_ID = @cargoTransportId;";

                var transports = await connection.ExecuteScalarAsync<int>(getQuery,
                    new
                    {
                        cargoTransportId = idOfItem
                    });

                return transports;
            }

        }

    }
}
