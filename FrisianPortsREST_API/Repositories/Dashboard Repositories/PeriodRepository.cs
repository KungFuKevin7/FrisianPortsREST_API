using Dapper;
using FrisianPortsREST_API.DTO;

namespace FrisianPortsREST_API.Repositories
{
    public class PeriodRepository
    {

        /// <summary>
        /// Get all years for the desired port, where data is availible
        /// </summary>
        /// <param name="portId">Id of desired port</param>
        /// <returns>List of all availible years for port</returns>
        public async Task<List<int>> GetAllYears(int portId)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();

                const string query = $@"SELECT DISTINCT YEAR(T.DEPARTURE_DATE) 
                                        FROM TRANSPORT T
                                        INNER JOIN CARGOTRANSPORT CT ON
                                        CT.CARGO_TRANSPORT_ID = T.CARGO_TRANSPORT_ID
                                        INNER JOIN ROUTE R
                                        ON CT.ROUTE_ID = R.ROUTE_ID
                                        WHERE R.DEPARTURE_PORT_ID = @PortId
                                        ORDER BY YEAR(T.DEPARTURE_DATE) ASC;";

                var yearTransport = await connection.QueryAsync<int>
                    (query, new
                        {
                            PortId = portId
                        }
                    );

                return yearTransport.ToList();
            }
        }

        /// <summary>
        /// Get import and export data per port on a yearly basis
        /// </summary>
        /// <param name="portId">Id of desired port</param>
        /// <returns>List of Import and Export per year from port</returns>
        public async Task<List<YearlyTransportDTO>> getYearlyReport(int portId) 
        {
            List<YearlyTransportDTO> yearlyTransportDTOs 
                   = new List<YearlyTransportDTO>();

            var years = await GetAllYears(portId);
            
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();
                const string query = $@"SELECT
                                        YEAR(T.DEPARTURE_DATE) Year,
                                        IF( DEPARTURE_PORT_ID = @PortId,'Export', 'Import') AS Cargo_Type_Name,
                                        SUM(WEIGHT_IN_TONNES) AS Transported_Weight
                                        FROM CARGOTRANSPORT CT
                                        INNER JOIN TRANSPORT T ON CT.CARGO_TRANSPORT_ID = T.CARGO_TRANSPORT_ID
                                        INNER JOIN ROUTE R ON CT.ROUTE_ID = R.ROUTE_ID
                                        INNER JOIN CARGO C ON C.TRANSPORT_ID = T.TRANSPORT_ID
                                        WHERE (R.ARRIVAL_PORT_ID = @PortId AND YEAR(T.DEPARTURE_DATE) = @Year)
                                        OR (R.DEPARTURE_PORT_ID = @PortId AND YEAR (T.DEPARTURE_DATE) = @Year)
                                        GROUP BY CT.CARGO_TRANSPORT_ID";

                //Loop through all the availible years
                for (int i = 0; i < years.Count; i++)
                {
                    YearlyTransportDTO dataOfYear = new YearlyTransportDTO();
                    var transportInYear = await connection.QueryAsync
                        <TransportedCargoDTO>(query, new
                        {
                            PortId = portId,
                            Year = years[i]
                        });

                    dataOfYear.Year = years[i];
                    dataOfYear.Transported = transportInYear.ToList();
                    
                    if (dataOfYear.Transported.Count > 0)   //Prevent false data
                    {
                        //Dummy data In case a field is missing 
                        dataOfYear.Transported.Add(
                            new TransportedCargoDTO("Export", 0));


                        yearlyTransportDTOs.Add(dataOfYear);
                    }
                }
                return yearlyTransportDTOs;
            }
        }

    }
}
