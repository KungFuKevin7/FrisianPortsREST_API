using Dapper;
using FrisianPortsREST_API.DTO;

namespace FrisianPortsREST_API.Repositories.Dashboard_Repositories
{
    public class CargoDestinationRepository
    {
        public async Task<List<CargoDestinationDTO>> GetImportShips(int idOfPort, int year, int month)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();

                string query =
                    @$"SELECT
                          (SELECT PORT_LOCATION FROM port WHERE PORT_ID = ARRIVAL_PORT_ID) AS LOCATION,
                          COUNT(*) AS SHIP_AMOUNT
                       FROM Transport T
                       INNER JOIN cargotransport CT ON T.CARGO_TRANSPORT_ID = CT.CARGO_TRANSPORT_ID
                       INNER JOIN ROUTE R ON CT.ROUTE_ID = R.ROUTE_ID
                       WHERE DEPARTURE_PORT_ID = @DeparturePort";

                QueryBuilder queryBuilder = new QueryBuilder(query);

                if (year != 0)
                {
                    queryBuilder.AddFilter("YEAR(T.DEPARTURE_DATE) = @selectedYear");
                }
                if (month != 0)
                {
                    queryBuilder.AddFilter("MONTH(T.DEPARTURE_DATE) = @selectedMonth");
                }

                queryBuilder.AddGroupByClause("ARRIVAL_PORT_ID");

                var cargo = await connection.
                    QueryAsync<CargoDestinationDTO>(
                    queryBuilder.Build(),
                    new
                    {
                        DeparturePort = idOfPort,
                        selectedYear = year,
                        selectedMonth = month
                    });

                connection.Close();
                return cargo.ToList();
            }
        }

        public async Task<List<CargoDestinationDTO>> GetExportShips(int idOfPort, int year, int month)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();

                string query =
                    @$"SELECT (SELECT PORT_LOCATION FROM port WHERE PORT_ID = DEPARTURE_PORT_ID) AS LOCATION,
                       COUNT(*) AS SHIP_AMOUNT 
                       FROM Transport T
                       INNER JOIN cargotransport CT ON T.CARGO_TRANSPORT_ID = CT.CARGO_TRANSPORT_ID
                       INNER JOIN ROUTE R ON CT.ROUTE_ID = R.ROUTE_ID
                       WHERE ARRIVAL_PORT_ID = @ArrivalPort";

                QueryBuilder queryBuilder = new QueryBuilder(query);

                if (year != 0)
                {
                    queryBuilder.AddFilter("YEAR(T.DEPARTURE_DATE) = @selectedYear");
                }
                if (month != 0)
                {
                    queryBuilder.AddFilter("MONTH(T.DEPARTURE_DATE) = @selectedMonth");
                }

                queryBuilder.AddGroupByClause("DEPARTURE_PORT_ID");

                var cargo = await connection.
                    QueryAsync<CargoDestinationDTO>(
                    queryBuilder.Build(),
                    new
                    {
                        ArrivalPort = idOfPort,
                        selectedYear = year,
                        selectedMonth = month
                    });

                connection.Close();
                return cargo.ToList();
            }
        }
    }
}
