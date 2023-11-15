using Dapper;
using FrisianPortsREST_API.DTO;
using FrisianPortsREST_API.Models;

namespace FrisianPortsREST_API.Repositories
{
    public class GoodsFlowRepository
    {

        public async Task<List<GoodsFlowDto>> GetGoodsFlows(string searchQuery)
        {
            using (var connection = DBConnection.getConnection())
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
                                    OR (SELECT PORT_LOCATION FROM PORT WHERE Port_ID = R.ARRIVAL_PORT_ID) LIKE @Query
                                    GROUP BY CT.CARGO_TRANSPORT_ID";

                searchQuery = $"%{searchQuery}%";

                var results = await connection.QueryAsync<GoodsFlowDto>(dbQuery,
                    new
                    {
                        Query = searchQuery
                    });

                return results.ToList();
            }
        }

        public async Task<List<GoodsFlowDto>> GetGoodsFlows(int portId)
        {
            using (var connection = DBConnection.getConnection())
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
    }
}
