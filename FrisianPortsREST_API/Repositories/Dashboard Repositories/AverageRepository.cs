using Dapper;

namespace FrisianPortsREST_API.Repositories
{
    public class AverageRepository
    {
        public async Task<double> GetAverageExportWeight(int portId)
        {
            using (var connection = DBConnection.getConnection())
            {
                connection.Open();

                const string query = $@"SELECT AVG(C.WEIGHT_IN_TONNES) FROM CargoTransport CT 
                INNER JOIN Transport T ON CT.CARGO_TRANSPORT_ID = T.CARGO_TRANSPORT_ID
                INNER JOIN Route R ON CT.ROUTE_ID = R.ROUTE_ID
                INNER JOIN Cargo C ON T.TRANSPORT_ID = C.TRANSPORT_ID
                WHERE R.DEPARTURE_PORT_ID = @DepartureId;";

                var avgWeight = await connection.ExecuteScalarAsync<double>(query,
                    new
                    {
                        DepartureId = portId
                    });

                return avgWeight;
            }
        }

        public async Task<double> GetAverageImportWeight(int portId)
        {
            using (var connection = DBConnection.getConnection())
            {
                connection.Open();

                const string query = $@"SELECT AVG(C.WEIGHT_IN_TONNES) FROM CargoTransport CT 
                INNER JOIN Transport T ON CT.CARGO_TRANSPORT_ID = T.CARGO_TRANSPORT_ID
                INNER JOIN Route R ON CT.ROUTE_ID = R.ROUTE_ID
                INNER JOIN Cargo C ON T.TRANSPORT_ID = C.TRANSPORT_ID
                WHERE R.ARRIVAL_PORT_ID = @ArrivalId;";

                var avgWeight = await connection.ExecuteScalarAsync<double>(query,
                    new
                    {
                        ArrivalId = portId
                    });

                return avgWeight;
            }
        }
    }
}
