﻿using Dapper;
using FrisianPortsREST_API.Models;

namespace FrisianPortsREST_API.Repositories
{
    public class TotalRepository
    {

        /// <summary>
        /// Gets List of total imported cargo.
        /// </summary>
        /// <param name="idOfPort">Id of requested port</param>
        /// <returns>List of cargo contributing to the import of port</returns>
        public async Task<int> GetImportShips(int idOfPort)
        {
            using (var connection = DBConnection.getConnection())
            {
                connection.Open();
                const string query = $@"SELECT COUNT(*) FROM TRANSPORT T
                                    INNER JOIN CARGOTRANSPORT CT ON T.CARGO_TRANSPORT_ID = CT.CARGO_TRANSPORT_ID
                                    INNER JOIN ROUTE r on CT.ROUTE_ID = r.ROUTE_ID
                                    WHERE r.ARRIVAL_PORT_ID = @ArrivalId";

                var port = await connection.ExecuteScalarAsync<int>(query,
                    new
                    {
                        ArrivalId = idOfPort
                    });

                return port;
            }
        }

        /// <summary>
        /// Gets List of total exported cargo.
        /// </summary>
        /// <param name="idOfPort">Id of requested port</param>
        /// <returns>List of cargo contributing to the export of port</returns>
        public async Task<int> GetExportShips(int idOfPort)
        {
            using (var connection = DBConnection.getConnection())
            {
                connection.Open();
                const string query = $@"SELECT COUNT(*) FROM TRANSPORT T
                                        INNER JOIN CARGOTRANSPORT CT ON T.CARGO_TRANSPORT_ID = CT.CARGO_TRANSPORT_ID
                                        INNER JOIN ROUTE r on CT.ROUTE_ID = r.ROUTE_ID
                                        WHERE r.DEPARTURE_PORT_ID = @DepartureId";
                var port = await connection.ExecuteScalarAsync<int>(query,
                    new
                    {
                        DepartureId = idOfPort
                    });

                return port;
            }
        }


        /// <summary>
        /// Gets total imported cargo in tonnes.
        /// </summary>
        /// <param name="idOfPort">Id of requested port</param>
        /// <returns>List of cargo contributing to the import of port</returns>
        public async Task<int> GetTotalImportWeight(int idOfPort)
        {
            using (var connection = DBConnection.getConnection())
            {
                connection.Open();

                const string query = $@"SELECT SUM(C.WEIGHT_IN_TONNES) FROM CargoTransport CT 
                INNER JOIN Transport T ON CT.CARGO_TRANSPORT_ID = T.CARGO_TRANSPORT_ID
                INNER JOIN Route R ON CT.ROUTE_ID = R.ROUTE_ID
                INNER JOIN Cargo C ON T.TRANSPORT_ID = C.TRANSPORT_ID
                WHERE R.ARRIVAL_PORT_ID = @ArrivalId;";

                var totalWeight = await connection.ExecuteScalarAsync<int>(query,
                    new
                    {
                        ArrivalId = idOfPort
                    });
                return totalWeight;
            }
        }

        /// <summary>
        /// Gets total exported cargo in tonnes.
        /// </summary>
        /// <param name="idOfPort">Id of requested port</param>
        /// <returns>List of cargo contributing to the import of port</returns>
        public async Task<int> GetTotalExportWeight(int idOfPort)
        {
            using (var connection = DBConnection.getConnection())
            {
                connection.Open();

                const string query = $@"SELECT SUM(C.WEIGHT_IN_TONNES) FROM CargoTransport CT 
                INNER JOIN Transport T ON CT.CARGO_TRANSPORT_ID = T.CARGO_TRANSPORT_ID
                INNER JOIN Route R ON CT.ROUTE_ID = R.ROUTE_ID
                INNER JOIN Cargo C ON T.TRANSPORT_ID = C.TRANSPORT_ID
                WHERE R.DEPARTURE_PORT_ID = @DepartureId;";
                var totalWeight = await connection.ExecuteScalarAsync<int>(query,
                    new
                    {
                        DepartureId = idOfPort
                    });

                return totalWeight;
            }
        }
    }
}