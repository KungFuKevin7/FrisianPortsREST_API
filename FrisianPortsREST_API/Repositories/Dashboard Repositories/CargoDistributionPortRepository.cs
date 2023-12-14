using Dapper;
using FrisianPortsREST_API.DTO;

namespace FrisianPortsREST_API.Repositories
{
    public class CargoDistributionPortRepository
    {
        public async Task<List<TransportedCargoDTO>> GetExport(int idOfPort, int period)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();

                string query =
                    @$"SELECT CS.CARGO_TYPE_NAME, SUM(C.WEIGHT_IN_TONNES) AS TRANSPORTED_WEIGHT
                    FROM CARGOTRANSPORT CT
                    INNER JOIN TRANSPORT T ON CT.CARGO_TRANSPORT_ID = T.CARGO_TRANSPORT_ID
                    INNER JOIN ROUTE R ON CT.ROUTE_ID = R.ROUTE_ID
                    INNER JOIN CARGO C ON C.TRANSPORT_ID = T.TRANSPORT_ID
                    INNER JOIN CARGOTYPE CS ON C.CARGO_TYPE_ID = CS.CARGO_TYPE_ID
                    WHERE R.DEPARTURE_PORT_ID = @DeparturePort";

                if (period != 0)
                {
                    query += " AND YEAR(T.DEPARTURE_DATE) = @Period ";
                }
                query += " GROUP BY C.CARGO_TYPE_ID";
                
                var cargo = await connection.
                    QueryAsync<TransportedCargoDTO>(query,
                    new
                    {
                        DeparturePort = idOfPort,
                        Period = period
                    });

                connection.Close();
                return cargo.ToList();
            }
        }

        public async Task<List<TransportedCargoDTO>> GetImport(int idOfPort, int period)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();

                string query =
                    @$"SELECT CS.CARGO_TYPE_NAME, SUM(C.WEIGHT_IN_TONNES) AS TRANSPORTED_WEIGHT
                    FROM CARGOTRANSPORT CT
                    INNER JOIN TRANSPORT T ON CT.CARGO_TRANSPORT_ID = T.CARGO_TRANSPORT_ID
                    INNER JOIN ROUTE R ON CT.ROUTE_ID = R.ROUTE_ID
                    INNER JOIN CARGO C ON C.TRANSPORT_ID = T.TRANSPORT_ID
                    INNER JOIN CARGOTYPE CS ON C.CARGO_TYPE_ID = CS.CARGO_TYPE_ID
                    WHERE R.ARRIVAL_PORT_ID = @ArrivalPort";

                if (period != 0)
                {
                    query += " AND YEAR(T.DEPARTURE_DATE) = @Period";
                }
                query += " GROUP BY C.CARGO_TYPE_ID;";

                var cargo = await connection.
                    QueryAsync<TransportedCargoDTO>(query,
                    new
                    {
                        ArrivalPort = idOfPort,
                        Period = period
                    });

                connection.Close();
                return cargo.ToList();
            }
        }

        public async Task<List<TransportedCargoDTO>> GetDistributionByCargoTransport(int cargoTransportId)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();

                const string query =
                    @$"SELECT (SELECT CARGO_TYPE_NAME FROM cargotype WHERE c.CARGO_TYPE_ID = cargotype.CARGO_TYPE_ID) AS CARGO_TYPE_NAME,
                        CARGO_DESCRIPTION,
                        C.WEIGHT_IN_TONNES AS TRANSPORTED_WEIGHT
                        FROM cargotransport CT
                        INNER JOIN TRANSPORT T
                        ON CT.CARGO_TRANSPORT_ID = T.CARGO_TRANSPORT_ID
                        INNER JOIN cargo c on T.TRANSPORT_ID = c.TRANSPORT_ID
                        WHERE T.CARGO_TRANSPORT_ID = @cargoTransportId;";

                var cargo = await connection.
                    QueryAsync<TransportedCargoDTO>(query,
                    new
                    {
                        cargoTransportId = cargoTransportId
                    });

                connection.Close();
                return cargo.ToList();
            }
        }

        public async Task<List<TransportedCargoDTO>> GetDistributionByCargoType(int cargoTransportId) 
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();

                const string query = $@"SELECT CS.CARGO_TYPE_NAME, SUM(C.WEIGHT_IN_TONNES) AS TRANSPORTED_WEIGHT
                                        FROM CARGOTRANSPORT CT
                                        INNER JOIN TRANSPORT T ON CT.CARGO_TRANSPORT_ID = T.CARGO_TRANSPORT_ID
                                        INNER JOIN CARGO C ON C.TRANSPORT_ID = T.TRANSPORT_ID
                                        INNER JOIN CARGOTYPE CS ON C.CARGO_TYPE_ID = CS.CARGO_TYPE_ID
                                        WHERE CT.CARGO_TRANSPORT_ID = @cargoTransportId
                                        GROUP BY C.CARGO_TYPE_ID;";

                var cargo = await connection.QueryAsync<TransportedCargoDTO>(
                    query, new
                    {
                        cargoTransportId = cargoTransportId,
                    });

                return cargo.ToList();
            }
        }

    }
}
