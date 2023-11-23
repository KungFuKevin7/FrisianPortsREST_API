using Dapper;
using FrisianPortsREST_API.Models;
using MySql.Data.MySqlClient;

namespace FrisianPortsREST_API.Repositories
{
    public class CargoTransportRepository : IRepository<CargoTransport>
    {
        public async Task<int> Add(CargoTransport cargoTransportToAdd)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                const string addQuery = @$"INSERT INTO CargoTransport
                                       (FREQUENCY, DATE_STARTED, ADDED_BY_ID, ROUTE_ID)
                                       VALUES
                                       (@Frequency, @DateStarted, @AddedById, @RouteId);
                                       SELECT LAST_INSERT_ID();";

                int idOfCreated = await connection.ExecuteScalarAsync<int>(addQuery,
                   new
                   {
                       Frequency = cargoTransportToAdd.Frequency,
                       DateStarted = cargoTransportToAdd.Date_Started,
                       AddedById = cargoTransportToAdd.Added_By_Id,
                       RouteId = cargoTransportToAdd.Route_Id
                   });
                connection.Close();
                return idOfCreated;
            }
        }

        public int Delete(int idOfCargoTransportToRemove)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                const string Query = @$"DELETE FROM CargoTransport
                                       WHERE CARGO_TRANSPORT_ID = @CargoTransportId";

                int success = connection.Execute(Query,
                    new
                    {
                        CargoTransportId = idOfCargoTransportToRemove
                    });
                return success;
            }
        }

        public async Task<List<CargoTransport>> Get()
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                const string query = @$"SELECT * FROM CargoTransport";

                var list = await connection.QueryAsync<CargoTransport>(query);
                return list.ToList();
            }
        }

        public async Task<CargoTransport> GetById(int idOfCargoTransport)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                const string query = @$"SELECT * FROM CargoTransport
                                    WHERE CARGO_TRANSPORT_ID = @cargoTransportId";
                var cargoTransport = await connection.QuerySingleOrDefaultAsync<CargoTransport>(query,
                    new
                    {
                        cargoTransportId = idOfCargoTransport
                    });
                return cargoTransport;
            }
        }

        public async Task<int> Update(CargoTransport cargoTransportUpdate)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                const string updateQuery = @$"UPDATE CargoTransport
                                       SET 
                                       FREQUENCY = @Frequency, 
                                       DATE_STARTED = @DateStarted, 
                                       ADDED_BY_ID = @AddedById, 
                                       ROUTE_ID = @RouteId) 
                                       WHERE CARGO_TRANSPORT_ID = @CargoTransportId ";

                int success = await connection.ExecuteAsync(updateQuery,
                   new
                   {
                       Frequency = cargoTransportUpdate.Frequency,
                       DateStarted = cargoTransportUpdate.Date_Started,
                       AddedById = cargoTransportUpdate.Added_By_Id,
                       RouteId = cargoTransportUpdate.Route_Id
                   });

                return success;
            }
        }

     
    }
}
