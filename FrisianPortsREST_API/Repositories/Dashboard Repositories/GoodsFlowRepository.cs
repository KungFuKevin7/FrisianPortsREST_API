using Dapper;
using FrisianPortsREST_API.DTO;
using FrisianPortsREST_API.Models;
using System.Text.RegularExpressions;

namespace FrisianPortsREST_API.Repositories
{
    public class GoodsFlowRepository
    {

        public async Task<List<GoodsFlowDto>> GetGoodsFlows(string searchQuery)
        {
            using (var connection = DBConnection.GetConnection())
            {

                string dbQuery = $@"SELECT
                                    CT.CARGO_TRANSPORT_ID,
                                    (SELECT PORT_LOCATION FROM PORT WHERE Port_ID = R.DEPARTURE_PORT_ID)  DEPARTURE_LOCATION,
                                    (SELECT PORT_LOCATION FROM PORT WHERE Port_ID = R.ARRIVAL_PORT_ID) ARRIVAL_LOCATION,
                                     CT.FREQUENCY,
                                     SUM(WEIGHT_IN_TONNES) AS TOTAL_WEIGHT FROM Route R
                                    INNER JOIN CARGOTRANSPORT CT ON CT.ROUTE_ID = R.ROUTE_ID
                                    INNER JOIN TRANSPORT T ON T.CARGO_TRANSPORT_ID = CT.CARGO_TRANSPORT_ID
                                    INNER JOIN CARGO C ON C.TRANSPORT_ID = T.TRANSPORT_ID
                                    WHERE (SELECT PORT_LOCATION FROM PORT WHERE Port_ID = R.DEPARTURE_PORT_ID) LIKE @Query
                                    OR (SELECT PORT_LOCATION FROM PORT WHERE Port_ID = R.ARRIVAL_PORT_ID) LIKE @Query";

                QueryBuilder queryBuilder = new QueryBuilder(dbQuery);
                queryBuilder.AddGroupByClause("CT.CARGO_TRANSPORT_ID");

                var results = await connection.QueryAsync<GoodsFlowDto>(dbQuery,
                    new
                    {
                        Query = $"%{searchQuery}%"
                    });

                return results.ToList();
            }
        }

        public async Task<List<GoodsFlowDto>> GetGoodsFlows(string searchQuery, string[] filters)
        {
            using (var connection = DBConnection.GetConnection())
            {
                string dbQuery = $@"SELECT
                                    CT.CARGO_TRANSPORT_ID,
                                    (SELECT PORT_LOCATION FROM PORT WHERE Port_ID = R.DEPARTURE_PORT_ID)  DEPARTURE_LOCATION,
                                    (SELECT PORT_LOCATION FROM PORT WHERE Port_ID = R.ARRIVAL_PORT_ID) ARRIVAL_LOCATION,
                                     CT.FREQUENCY,
                                     SUM(WEIGHT_IN_TONNES) AS TOTAL_WEIGHT FROM Route R
                                    INNER JOIN CARGOTRANSPORT CT ON CT.ROUTE_ID = R.ROUTE_ID
                                    INNER JOIN TRANSPORT T ON T.CARGO_TRANSPORT_ID = CT.CARGO_TRANSPORT_ID
                                    INNER JOIN CARGO C ON C.TRANSPORT_ID = T.TRANSPORT_ID
                                    WHERE (
                                            (SELECT PORT_LOCATION FROM PORT WHERE Port_ID = R.DEPARTURE_PORT_ID) LIKE @SearchQuery
                                            OR (SELECT PORT_LOCATION FROM PORT WHERE Port_ID = R.ARRIVAL_PORT_ID) LIKE @SearchQuery
                                        )
                                    AND (SELECT PROVINCE_NAME FROM PROVINCE WHERE PROVINCE_ID =
                                            (SELECT PROVINCE_ID FROM Port WHERE PORT_ID = R.DEPARTURE_PORT_ID)) IN @Filters OR
                                          (SELECT PROVINCE_NAME FROM PROVINCE WHERE PROVINCE_ID =
                                            (SELECT PROVINCE_ID FROM Port WHERE PORT_ID = R.ARRIVAL_PORT_ID)) IN @Filters";

                QueryBuilder queryBuilder = new QueryBuilder(dbQuery);
                queryBuilder.AddGroupByClause("CT.CARGO_TRANSPORT_ID");

                var results = await connection.QueryAsync<GoodsFlowDto>(dbQuery,
                    new
                    {
                        SearchQuery = $"%{searchQuery}%",
                        Filters = filters
                    });

                if (results.ElementAt(0).CargoTransportId == 0) //In some cases, Dapper returns a list with an empty object
                {
                    var empty = results.ToList();
                    empty.Clear();
                    return empty;
                }

                return results.ToList();
            }
        }

        public async Task<List<GoodsFlowDto>> GetGoodsFlows(int portId)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();

                const string query = $@"SELECT 
                                    CT.CARGO_TRANSPORT_ID,
                                    (SELECT PORT_LOCATION FROM PORT WHERE Port_ID = R.DEPARTURE_PORT_ID) AS DEPARTURE_LOCATION,
                                    (SELECT PORT_LOCATION FROM PORT WHERE Port_ID = R.ARRIVAL_PORT_ID) AS ARRIVAL_LOCATION,
                                     CT.FREQUENCY,
                                     SUM(WEIGHT_IN_TONNES) AS TOTAL_WEIGHT FROM Route R
                                    INNER JOIN CARGOTRANSPORT CT ON CT.ROUTE_ID = R.ROUTE_ID
                                    INNER JOIN TRANSPORT T ON T.CARGO_TRANSPORT_ID = CT.CARGO_TRANSPORT_ID
                                    INNER JOIN CARGO C ON C.TRANSPORT_ID = T.TRANSPORT_ID
                                    WHERE R.DEPARTURE_PORT_ID = @portId OR R.ARRIVAL_PORT_ID = @portId
                                    GROUP BY R.ROUTE_ID";

                var goodFlows = await connection.QueryAsync<GoodsFlowDto>(query,
                    new
                    {
                        portId = portId
                    });

                return goodFlows.ToList();
            }
        }

        public async Task<GoodsFlowDto> GetGoodsFlowsById(int cargoTransportId)
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();

                const string query = $@"SELECT 
                                    CT.CARGO_TRANSPORT_ID,
                                    (SELECT PORT_LOCATION FROM PORT WHERE Port_ID = R.DEPARTURE_PORT_ID) AS DEPARTURE_LOCATION,
                                    (SELECT PORT_LOCATION FROM PORT WHERE Port_ID = R.ARRIVAL_PORT_ID) AS ARRIVAL_LOCATION,
                                     CT.FREQUENCY,
                                     SUM(WEIGHT_IN_TONNES) AS TOTAL_WEIGHT FROM Route R
                                    INNER JOIN CARGOTRANSPORT CT ON CT.ROUTE_ID = R.ROUTE_ID
                                    INNER JOIN TRANSPORT T ON T.CARGO_TRANSPORT_ID = CT.CARGO_TRANSPORT_ID
                                    INNER JOIN CARGO C ON C.TRANSPORT_ID = T.TRANSPORT_ID
                                    WHERE CT.CARGO_TRANSPORT_ID = @cargoTransportId
                                    GROUP BY R.ROUTE_ID";

                var goodFlows = await connection.QuerySingleOrDefaultAsync<GoodsFlowDto>(query,
                    new
                    {
                        cargoTransportId = cargoTransportId
                    });

                return goodFlows;
            }
        }


        public async Task<List<GoodsFlowDto>> GetAllGoodsFlows()
        {
            using (var connection = DBConnection.GetConnection())
            {
                connection.Open();

                const string query = $@"SELECT CT.CARGO_TRANSPORT_ID,
                                       (SELECT PORT_LOCATION FROM PORT WHERE Port_ID = DEPARTURE_PORT_ID) AS DEPARTURE_LOCATION,
                                       (SELECT PORT_LOCATION FROM PORT WHERE Port_ID = ARRIVAL_PORT_ID) AS ARRIVAL_LOCATION,
                                       CT.FREQUENCY FROM ROUTE R
                                       INNER JOIN CARGOTRANSPORT CT ON CT.ROUTE_ID = R.ROUTE_ID";

                var goodFlows = await connection.QueryAsync<GoodsFlowDto>(query);

                return goodFlows.ToList();
            }
        }
    }
}
