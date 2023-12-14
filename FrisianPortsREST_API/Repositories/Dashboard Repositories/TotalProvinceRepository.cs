using Dapper;
using FrisianPortsREST_API.Models;

namespace FrisianPortsREST_API.Repositories
{
    public class TotalProvinceRepository
    {
        /// <summary>
        /// Gets List of total imported cargo.
        /// </summary>
        /// <param name="idOfProvince">Id of requested province</param>
        /// <returns>Shipmovements contributing to the import of province</returns>
        public async Task<int> GetImportShips(int idOfProvince, int period)
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
        public async Task<int> GetExportShips(int idOfProvince, int period)
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
        /// Gets total imported cargo in tonnes.
        /// </summary>
        /// <param name="idOfProvince">Id of requested province</param>
        /// <returns>Total Cargo weight contributing to the import of province</returns>
        public async Task<int> GetTotalImportWeight(int idOfProvince, int period)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();

                string query = $@"SELECT SUM(C.WEIGHT_IN_TONNES) FROM CARGO C
                                INNER JOIN TRANSPORT T ON C.TRANSPORT_ID = T.TRANSPORT_ID
                                INNER JOIN CARGOTRANSPORT CT ON CT.CARGO_TRANSPORT_ID = T.CARGO_TRANSPORT_ID
                                INNER JOIN ROUTE R ON CT.ROUTE_ID = R.ROUTE_ID
                                INNER JOIN PORT P ON R.ARRIVAL_PORT_ID = P.PORT_ID
                                WHERE P.PROVINCE_ID = @ProvinceId";

                if (period != 0)
                {
                    query += " AND YEAR(T.DEPARTURE_DATE) = @selectedPeriod";
                }

                var totalWeight = await connection.ExecuteScalarAsync<int>(query,
                    new
                    {
                        ProvinceId = idOfProvince,
                        selectedPeriod = period
                    });
                return totalWeight;
            }
        }

        /// <summary>
        /// Gets total exported cargo in tonnes.
        /// </summary>
        /// <param name="idOfProvince">Id of requested province</param>
        /// <returns>Total Cargo weight contributing to the export of province</returns>
        public async Task<int> GetTotalExportWeight(int idOfProvince, int period)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();

                string query = $@"SELECT SUM(C.WEIGHT_IN_TONNES) FROM CARGO C
                                INNER JOIN TRANSPORT T ON C.TRANSPORT_ID = T.TRANSPORT_ID
                                INNER JOIN CARGOTRANSPORT CT ON CT.CARGO_TRANSPORT_ID = T.CARGO_TRANSPORT_ID
                                INNER JOIN ROUTE R ON CT.ROUTE_ID = R.ROUTE_ID
                                INNER JOIN PORT P ON R.DEPARTURE_PORT_ID = P.PORT_ID
                                WHERE P.PROVINCE_ID = @ProvinceId";

                if (period != 0)
                {
                    query += " AND YEAR(T.DEPARTURE_DATE) = @selectedPeriod";
                }

                var totalWeight = await connection.ExecuteScalarAsync<int>(query,
                    new
                    {
                        ProvinceId = idOfProvince,
                        selectedPeriod = period
                    });

                return totalWeight;
            }
        }

        /// <summary>
        /// Get Total Tonnage transported Within the requested province
        /// </summary>
        /// <param name="idOfProvince">Id Of requested Province</param>
        /// <param name="period">Period to Filter by</param>
        /// <returns>Total Tonnage transported inside the province</returns>
        public async Task<int> GetTonnageTransportInProvince(int idOfProvince, int period)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                string query = $@"SELECT SUM(WEIGHT_IN_TONNES) FROM TRANSPORT T
                                INNER JOIN CARGO C ON T.TRANSPORT_ID = C.TRANSPORT_ID
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
                        ProvinceId = idOfProvince,
                        selectedPeriod = period
                    });

                return totalTransport;
            }
        }

        /// <summary>
        /// Get Total Tonnage originating from outside province
        /// </summary>
        /// <param name="idOfProvince">Id Of requested Province</param>
        /// <param name="period">Period to Filter by</param>
        /// <returns>Total Tonnage originating from outside province</returns>
        public async Task<int> GetImportFromOutsideProvince(int idOfProvince, int period)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                string query = $@"SELECT SUM(WEIGHT_IN_TONNES) FROM TRANSPORT T
                                INNER JOIN CARGO C ON T.TRANSPORT_ID = C.TRANSPORT_ID
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
                        ProvinceId = idOfProvince,
                        selectedPeriod = period
                    });

                return totalTransport;
            }
        }

        /// <summary>
        /// Get Total Tonnage transported to outside of the province
        /// </summary>
        /// <param name="idOfProvince">Id Of requested Province</param>
        /// <param name="period">Period to Filter by</param>
        /// <returns>Total Tonnage transported to outside of the province</returns>
        public async Task<int> ExportToOutsideProvince(int idOfProvince, int period)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                string query = $@"SELECT SUM(WEIGHT_IN_TONNES) FROM TRANSPORT T
                                INNER JOIN CARGO C ON T.TRANSPORT_ID = C.TRANSPORT_ID
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
                        ProvinceId = idOfProvince,
                        selectedPeriod = period
                    });

                return totalTransport;
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
