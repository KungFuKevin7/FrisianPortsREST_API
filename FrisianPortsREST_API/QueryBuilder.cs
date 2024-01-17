namespace FrisianPortsREST_API
{
    public class QueryBuilder : IBuilder<QueryBuilder>
    {
        private string _query;

        public QueryBuilder(string query) 
        {
            _query = query;
        }

        /// <summary>
        /// Adds a WHERE-clause to the query
        /// </summary>
        /// <param name="filter">filter to use in the where clause</param>
        /// <returns>Query with the added where clause</returns>
        public QueryBuilder AddFilter(string filter) 
        {
            if (_query.Contains("WHERE"))
            {
                _query += $" AND {filter}";
            }
            else
            {
                _query += $" WHERE {filter}";
            }
            return this;
        }

        /// <summary>
        /// Adds a GROUP BY-clause to the query
        /// </summary>
        /// <param name="groupByClause">clause to group by</param>
        /// <returns>Query with the added group by clause</returns>
        public QueryBuilder AddGroupByClause(string groupByClause)
        {
            _query += $" GROUP BY {groupByClause}";
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderByClause"></param>
        /// <returns></returns>
        public QueryBuilder AddOrderByClause(string orderByClause) 
        {
            _query += $"ORDER BY {orderByClause}";
            return this;
        }

        /// <summary>
        /// return the entirely constructed query
        /// </summary>
        /// <returns>Final query</returns>
        public string Build() 
        {
            return _query;
        }
    }
}
