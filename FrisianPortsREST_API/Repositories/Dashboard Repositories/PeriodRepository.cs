using Dapper;
using FrisianPortsREST_API.DTO;

namespace FrisianPortsREST_API.Repositories
{
    public class PeriodRepository
    {

        public async Task<List<YearlyTransportDTO>> GetYearlyImport(int portId)
        {
            using (var connection = DBConnection.getConnection())
            {
                connection.Open();

                const string query = $@"SELECT 
                                        YEAR(T.DEPARTURE_DATE) Year, 
                                        ""IMPORT"" AS Cargo_Type_Name, 
                                        SUM(WEIGHT_IN_TONNES) AS Transported_Weight
                                        FROM CARGOTRANSPORT CT
                                        INNER JOIN TRANSPORT T ON CT.CARGO_TRANSPORT_ID = T.CARGO_TRANSPORT_ID
                                        INNER JOIN ROUTE R ON CT.ROUTE_ID = R.ROUTE_ID
                                        INNER JOIN CARGO C ON C.TRANSPORT_ID = T.TRANSPORT_ID
                                        WHERE R.ARRIVAL_PORT_ID = @Arrival_Id
                                        GROUP BY Year
                                        ORDER BY YEAR;";

                var yearTransport = await connection.QueryAsync
                    <YearlyTransportDTO, TransportedCargoDTO, YearlyTransportDTO>
                            (
                                query,
                                (YearlyTransport, TransportedCargo) =>
                                {
                                    YearlyTransport.Transported = TransportedCargo;
                                    return YearlyTransport;
                                },
                                new { Arrival_Id = portId },
                                splitOn: "Cargo_Type_Name"
                            );

                return yearTransport.ToList();
            }
        }

        public async Task<List<YearlyTransportDTO>> GetYearlyExport(int portId)
        {
            using (var connection = DBConnection.getConnection())
            {
                connection.Open();

                const string query = $@"SELECT 
                                        YEAR(T.DEPARTURE_DATE) Year, 
                                        ""EXPORT"" AS Cargo_Type_Name, 
                                        SUM(WEIGHT_IN_TONNES) AS Transported_Weight
                                        FROM CARGOTRANSPORT CT
                                        INNER JOIN TRANSPORT T ON CT.CARGO_TRANSPORT_ID = T.CARGO_TRANSPORT_ID
                                        INNER JOIN ROUTE R ON CT.ROUTE_ID = R.ROUTE_ID
                                        INNER JOIN CARGO C ON C.TRANSPORT_ID = T.TRANSPORT_ID
                                        WHERE R.DEPARTURE_PORT_ID = @Departure_Id
                                        GROUP BY Year
                                        ORDER BY YEAR;";

                var yearTransport = await connection.QueryAsync<YearlyTransportDTO, TransportedCargoDTO, YearlyTransportDTO>
                    (
                        query,
                        (YearlyTransport, TransportedCargo) =>
                        {
                            YearlyTransport.Transported = TransportedCargo;
                            return YearlyTransport;
                        },
                        new { Departure_Id = portId },
                        splitOn: "Cargo_Type_Name"
                    );

                return yearTransport.ToList();
            }
        }

        public async Task<List<YearlyTransportDTO>> GetYearlyCollection(int portId)
        {
            using (var connection = DBConnection.getConnection())
            {
                connection.Open();

                const string query = $@"SELECT 
                                        YEAR(T.DEPARTURE_DATE) Year,
                                        CASE WHEN R.DEPARTURE_PORT_ID = @portId THEN ""EXPORT"" ELSE ""IMPORT"" END AS Cargo_Type_Name,
                                        SUM(WEIGHT_IN_TONNES) AS Transported_Weight
                                        FROM CARGOTRANSPORT CT
                                        INNER JOIN TRANSPORT T ON CT.CARGO_TRANSPORT_ID = T.CARGO_TRANSPORT_ID
                                        INNER JOIN ROUTE R ON CT.ROUTE_ID = R.ROUTE_ID
                                        INNER JOIN CARGO C ON C.TRANSPORT_ID = T.TRANSPORT_ID
                                        WHERE R.ARRIVAL_PORT_ID = @portId OR R.DEPARTURE_PORT_ID = @portId
                                        GROUP BY YEAR, CARGO_TYPE_NAME
                                        ORDER BY YEAR";

                var yearTransport = await connection.QueryAsync<YearlyTransportDTO, TransportedCargoDTO, YearlyTransportDTO>
                    (
                        query,
                        (YearlyTransport, TransportedCargo) =>
                        {
                            YearlyTransport.Transported = TransportedCargo;
                            return YearlyTransport;
                        },
                        new { portId = portId },
                        splitOn: "Cargo_Type_Name"
                    );

                return yearTransport.ToList();
            }
        }

    }
}
