using Dapper;

namespace FrisianPortsREST_API.Repositories.Dashboard_Repositories
{
    public class TonnageRepository
    {
        /// <summary>
        /// Gets total imported cargo in tonnes.
        /// </summary>
        /// <param name="idOfPort">Id of requested port</param>
        /// <returns>List of cargo contributing to the import of port</returns>
        public async Task<int> GetPortImportTonnage(int idOfPort, int year, int month)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();

                string query = $@"SELECT SUM(C.WEIGHT_IN_TONNES) FROM CargoTransport CT 
                INNER JOIN Transport T ON CT.CARGO_TRANSPORT_ID = T.CARGO_TRANSPORT_ID
                INNER JOIN Route R ON CT.ROUTE_ID = R.ROUTE_ID
                INNER JOIN Cargo C ON T.TRANSPORT_ID = C.TRANSPORT_ID
                WHERE R.ARRIVAL_PORT_ID = @ArrivalId";
                
                QueryBuilder queryBuilder = new QueryBuilder(query);

                if (year != 0)
                {
                    queryBuilder.AddFilter("YEAR(T.DEPARTURE_DATE) = @selectedYear");
                }
                if (month != 0)
                {
                    queryBuilder.AddFilter("MONTH(T.DEPARTURE_DATE) = @selectedMonth");
                }

                var totalWeight = await connection.ExecuteScalarAsync<int>(
                    queryBuilder.Build(),
                    new
                    {
                        ArrivalId = idOfPort,
                        selectedYear = year,
                        selectedMonth = month
                    });
                return totalWeight;
            }
        }

        /// <summary>
        /// Gets total exported cargo in tonnes.
        /// </summary>
        /// <param name="idOfPort">Id of requested port</param>
        /// <returns>List of cargo contributing to the import of port</returns>
        public async Task<int> GetPortExportTonnage(int idOfPort, int year, int month)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();

                string query = $@"SELECT SUM(C.WEIGHT_IN_TONNES) FROM CargoTransport CT 
                INNER JOIN Transport T ON CT.CARGO_TRANSPORT_ID = T.CARGO_TRANSPORT_ID
                INNER JOIN Route R ON CT.ROUTE_ID = R.ROUTE_ID
                INNER JOIN Cargo C ON T.TRANSPORT_ID = C.TRANSPORT_ID
                WHERE R.DEPARTURE_PORT_ID = @DepartureId";

                QueryBuilder queryBuilder = new QueryBuilder(query);

                if (year != 0)
                {
                    queryBuilder.AddFilter("YEAR(T.DEPARTURE_DATE) = @selectedYear");
                }
                if (month != 0)
                {
                    queryBuilder.AddFilter("MONTH(T.DEPARTURE_DATE) = @selectedMonth");
                }

                var totalWeight = await connection.ExecuteScalarAsync<int>(
                    queryBuilder.Build(),
                    new
                    {
                        DepartureId = idOfPort,
                        selectedYear = year,
                        selectedMonth = month
                    });

                return totalWeight;
            }
        }

        /// <summary>
        /// Gets total imported cargo in tonnes.
        /// </summary>
        /// <param name="idOfProvince">Id of requested province</param>
        /// <returns>Total Cargo weight contributing to the import of province</returns>
        public async Task<int> GetProvinceImportTonnage(int idOfProvince, int year, int month)
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

                QueryBuilder queryBuilder = new QueryBuilder(query);

                if (year != 0)
                {
                    queryBuilder.AddFilter("YEAR(T.DEPARTURE_DATE) = @selectedYear");
                }
                if (month != 0)
                {
                    queryBuilder.AddFilter("MONTH(T.DEPARTURE_DATE) = @selectedMonth");
                }

                var totalWeight = await connection.ExecuteScalarAsync<int>(
                    queryBuilder.Build(),
                    new
                    {
                        ProvinceId = idOfProvince,
                        selectedYear = year,
                        selectedMonth = month
                    });
                return totalWeight;
            }
        }

        /// <summary>
        /// Gets total exported cargo in tonnes.
        /// </summary>
        /// <param name="idOfProvince">Id of requested province</param>
        /// <returns>Total Cargo weight contributing to the export of province</returns>
        public async Task<int> GetProvinceExportTonnage(int idOfProvince, int year, int month)
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

                QueryBuilder queryBuilder = new QueryBuilder(query);

                if (year != 0)
                {
                    queryBuilder.AddFilter("YEAR(T.DEPARTURE_DATE) = @selectedYear");
                }
                if (month != 0)
                {
                    queryBuilder.AddFilter("MONTH(T.DEPARTURE_DATE) = @selectedMonth");
                }

                var totalWeight = await connection.ExecuteScalarAsync<int>(
                    queryBuilder.Build(),
                    new
                    {
                        ProvinceId = idOfProvince,
                        selectedYear = year,
                        selectedMonth = month
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
        public async Task<int> GetTonnageInsideProvince(int idOfProvince, int year, int month)
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

                QueryBuilder queryBuilder = new QueryBuilder(query);

                if (year != 0)
                {
                    queryBuilder.AddFilter("YEAR(T.DEPARTURE_DATE) = @selectedYear");
                }
                if (month != 0)
                {
                    queryBuilder.AddFilter("MONTH(T.DEPARTURE_DATE) = @selectedMonth");
                }

                var totalTransport = await connection.ExecuteScalarAsync<int>(
                    queryBuilder.Build(),
                    new
                    {
                        ProvinceId = idOfProvince,
                        selectedYear = year,
                        selectedMonth = month
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
        public async Task<int> GetTonnageFromOutsideProvince(int idOfProvince, int year, int month)
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

                QueryBuilder queryBuilder = new QueryBuilder(query);

                if (year != 0)
                {
                    queryBuilder.AddFilter("YEAR(T.DEPARTURE_DATE) = @selectedYear");
                }
                if (month != 0)
                {
                    queryBuilder.AddFilter("MONTH(T.DEPARTURE_DATE) = @selectedMonth");
                }

                var totalTransport = await connection.ExecuteScalarAsync<int>(
                    queryBuilder.Build(),
                    new
                    {
                        ProvinceId = idOfProvince,
                        selectedYear = year,
                        selectedMonth = month
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
        public async Task<int> GetTonnageToOutsideProvince(int idOfProvince, int year, int month)
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

                QueryBuilder queryBuilder = new QueryBuilder(query);

                if (year != 0)
                {
                    queryBuilder.AddFilter("YEAR(T.DEPARTURE_DATE) = @selectedYear");
                }
                if (month != 0)
                {
                    queryBuilder.AddFilter("MONTH(T.DEPARTURE_DATE) = @selectedMonth");
                }

                var totalTransport = await connection.ExecuteScalarAsync<int>(
                    queryBuilder.Build(),
                    new
                    {
                        ProvinceId = idOfProvince,
                        selectedYear = year,
                        selectedMonth = month
                    });

                return totalTransport;
            }
        }
    }
}
