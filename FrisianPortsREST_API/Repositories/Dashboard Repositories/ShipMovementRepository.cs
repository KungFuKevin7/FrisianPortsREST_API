using Dapper;

namespace FrisianPortsREST_API.Repositories
{
    public class ShipMovementRepository
    {
        /// <summary>
        /// Gets List of total imported cargo.
        /// </summary>
        /// <param name="idOfProvince">Id of requested province</param>
        /// <returns>Shipmovements contributing to the import of province</returns>
        public async Task<int> GetImportOfProvince(int idOfProvince, int period)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                string query = $@"SELECT COUNT(*) FROM TRANSPORT T
                                INNER JOIN CARGOTRANSPORT CT ON T.CARGO_TRANSPORT_ID = CT.CARGO_TRANSPORT_ID
                                INNER JOIN ROUTE R ON CT.ROUTE_ID = R.ROUTE_ID
                                INNER JOIN PORT P ON R.ARRIVAL_PORT_ID = P.PORT_ID
                                WHERE P.PROVINCE_ID = @ProvinceId";
                if (period != 0)
                {
                    query += " AND YEAR(T.DEPARTURE_DATE) = @selectedPeriod;";
                }

                var totalShipMovements = await connection.ExecuteScalarAsync<int>(query,
                    new
                    {
                        ProvinceId = idOfProvince,
                        selectedPeriod = period
                    });

                return totalShipMovements;
            }
        }

        /// <summary>
        /// Gets List of total exported cargo.
        /// </summary>
        /// <param name="idOfProvince">Id of requested province</param>
        /// <returns>Shipmovements contributing to the export of province</returns>
        public async Task<int> GetExportOfProvince(int idOfProvince, int period)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                string query = $@"SELECT COUNT(*) FROM TRANSPORT T
                                INNER JOIN CARGOTRANSPORT CT ON T.CARGO_TRANSPORT_ID = CT.CARGO_TRANSPORT_ID
                                INNER JOIN ROUTE R ON CT.ROUTE_ID = R.ROUTE_ID
                                INNER JOIN PORT P ON R.DEPARTURE_PORT_ID = P.PORT_ID
                                INNER JOIN PROVINCE PR ON P.PROVINCE_ID = PR.PROVINCE_ID
                                WHERE PR.PROVINCE_ID = @ProvinceId";

                if (period != 0)
                {
                    query += " AND YEAR(T.DEPARTURE_DATE) = @selectedPeriod;";
                }

                var totalShipMovements = await connection.ExecuteScalarAsync<int>(query,
                    new
                    {
                        ProvinceId = idOfProvince,
                        selectedPeriod = period
                    });

                return totalShipMovements;
            }
        }

        /// <summary>
        /// Gets List of total imported cargo.
        /// </summary>
        /// <param name="idOfPort">Id of requested port</param>
        /// <returns>List of cargo contributing to the import of port</returns>
        public async Task<int> GetImportOfPort(int idOfPort, int period)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                string query = $@"SELECT COUNT(*) FROM TRANSPORT T
                                    INNER JOIN CARGOTRANSPORT CT ON T.CARGO_TRANSPORT_ID = CT.CARGO_TRANSPORT_ID
                                    INNER JOIN ROUTE r on CT.ROUTE_ID = r.ROUTE_ID
                                    WHERE r.ARRIVAL_PORT_ID = @ArrivalId";
                if (period != 0)
                {
                    query += " AND YEAR(T.DEPARTURE_DATE) = @selectedPeriod";
                }

                var port = await connection.ExecuteScalarAsync<int>(query,
                    new
                    {
                        ArrivalId = idOfPort,
                        selectedPeriod = period
                    });

                return port;
            }
        }

        /// <summary>
        /// Gets List of total exported cargo.
        /// </summary>
        /// <param name="idOfPort">Id of requested port</param>
        /// <returns>List of cargo contributing to the export of port</returns>
        public async Task<int> GetExportOfPort(int idOfPort, int period)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                string query = $@"SELECT COUNT(*) FROM TRANSPORT T
                                        INNER JOIN CARGOTRANSPORT CT ON T.CARGO_TRANSPORT_ID = CT.CARGO_TRANSPORT_ID
                                        INNER JOIN ROUTE r on CT.ROUTE_ID = r.ROUTE_ID
                                        WHERE r.DEPARTURE_PORT_ID = @DepartureId";

                if (period != 0)
                {
                    query += " AND YEAR(T.DEPARTURE_DATE) = @selectedPeriod";
                }

                var port = await connection.ExecuteScalarAsync<int>(query,
                    new
                    {
                        DepartureId = idOfPort,
                        selectedPeriod = period
                    });

                return port;
            }
        }


        public async Task<int> GetTransportsInProvince(int provinceId, int period)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                string query = $@"SELECT COUNT(*) FROM TRANSPORT T
                                INNER JOIN CARGOTRANSPORT CT ON T.CARGO_TRANSPORT_ID = CT.CARGO_TRANSPORT_ID
                                INNER JOIN ROUTE R ON CT.ROUTE_ID = R.ROUTE_ID
                                WHERE (R.DEPARTURE_PORT_ID IN (SELECT PORT_ID FROM port WHERE PROVINCE_ID = @ProvinceId))
                                AND (R.ARRIVAL_PORT_ID IN (SELECT PORT_ID FROM port WHERE PROVINCE_ID = @ProvinceId))";

                if (period != 0)
                {
                    query += " AND YEAR(T.DEPARTURE_DATE) = @selectedPeriod;";
                }

                var totalTransport = await connection.ExecuteScalarAsync<int>(query,
                    new
                    {
                        ProvinceId = provinceId,
                        selectedPeriod = period
                    });

                return totalTransport;
            }
        }

        public async Task<int> GetTransportsImportFromOutside(int provinceId, int period)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                string query = $@"SELECT COUNT(*) FROM TRANSPORT T
                                INNER JOIN CARGOTRANSPORT CT ON T.CARGO_TRANSPORT_ID = CT.CARGO_TRANSPORT_ID
                                INNER JOIN ROUTE R ON CT.ROUTE_ID = R.ROUTE_ID
                                WHERE (R.DEPARTURE_PORT_ID NOT IN (SELECT PORT_ID FROM port WHERE PROVINCE_ID = @ProvinceId))
                                AND (R.ARRIVAL_PORT_ID IN (SELECT PORT_ID FROM port WHERE PROVINCE_ID = @ProvinceId))";

                if (period != 0)
                {
                    query += " AND YEAR(T.DEPARTURE_DATE) = @selectedPeriod;";
                }

                var totalTransport = await connection.ExecuteScalarAsync<int>(query,
                    new
                    {
                        ProvinceId = provinceId,
                        selectedPeriod = period
                    });

                return totalTransport;
            }
        }

        public async Task<int> GetTransportsToOutside(int provinceId, int period)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                string query = $@"SELECT COUNT(*) FROM TRANSPORT T
                                INNER JOIN CARGOTRANSPORT CT ON T.CARGO_TRANSPORT_ID = CT.CARGO_TRANSPORT_ID
                                INNER JOIN ROUTE R ON CT.ROUTE_ID = R.ROUTE_ID
                                WHERE (R.DEPARTURE_PORT_ID IN (SELECT PORT_ID FROM port WHERE PROVINCE_ID = @ProvinceId))
                                AND (R.ARRIVAL_PORT_ID NOT IN (SELECT PORT_ID FROM port WHERE PROVINCE_ID = @ProvinceId))";

                if (period != 0)
                {
                    query += " AND YEAR(T.DEPARTURE_DATE) = @selectedPeriod;";
                }

                var totalTransport = await connection.ExecuteScalarAsync<int>(query,
                    new
                    {
                        ProvinceId = provinceId,
                        selectedPeriod = period
                    });

                return totalTransport;
            }
        }

    }
}
