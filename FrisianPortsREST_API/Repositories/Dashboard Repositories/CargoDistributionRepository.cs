using Dapper;
using FrisianPortsREST_API.DTO;

namespace FrisianPortsREST_API.Repositories
{
    public class CargoDistributionRepository
    {
        public async Task<List<TransportedCargoDTO>> GetExport(int idOfPort)
        {
            using (var connection = DBConnection.getConnection())
            {
                connection.Open();

                const string query =
                    @$"SELECT CS.CARGO_TYPE_NAME, SUM(C.WEIGHT_IN_TONNES) AS TRANSPORTED_WEIGHT
                    FROM CARGOTRANSPORT CT
                    INNER JOIN TRANSPORT T ON CT.CARGO_TRANSPORT_ID = T.CARGO_TRANSPORT_ID
                    INNER JOIN ROUTE R ON CT.ROUTE_ID = R.ROUTE_ID
                    INNER JOIN CARGO C ON C.TRANSPORT_ID = T.TRANSPORT_ID
                    INNER JOIN CARGOTYPE CS ON C.CARGO_TYPE_ID = CS.CARGO_TYPE_ID
                    WHERE R.DEPARTURE_PORT_ID = @DeparturePort
                    GROUP BY C.CARGO_TYPE_ID;";

                var cargo = await connection.
                    QueryAsync<TransportedCargoDTO>(query,
                    new
                    {
                        DeparturePort = idOfPort
                    });

                connection.Close();
                return cargo.ToList();
            }
        }

        public async Task<List<TransportedCargoDTO>> GetImport(int idOfPort)
        {
            using (var connection = DBConnection.getConnection())
            {
                connection.Open();

                const string query =
                    @$"SELECT CS.CARGO_TYPE_NAME, SUM(C.WEIGHT_IN_TONNES) AS TRANSPORTED_WEIGHT
                    FROM CARGOTRANSPORT CT
                    INNER JOIN TRANSPORT T ON CT.CARGO_TRANSPORT_ID = T.CARGO_TRANSPORT_ID
                    INNER JOIN ROUTE R ON CT.ROUTE_ID = R.ROUTE_ID
                    INNER JOIN CARGO C ON C.TRANSPORT_ID = T.TRANSPORT_ID
                    INNER JOIN CARGOTYPE CS ON C.CARGO_TYPE_ID = CS.CARGO_TYPE_ID
                    WHERE R.ARRIVAL_PORT_ID = @ArrivalPort
                    GROUP BY C.CARGO_TYPE_ID;";

                var cargo = await connection.
                    QueryAsync<TransportedCargoDTO>(query,
                    new
                    {
                        ArrivalPort = idOfPort
                    });

                connection.Close();
                return cargo.ToList();
            }
        }

    }
}
