namespace FrisianPortsREST_API
{
    public class QueryBuilder : IBuilder<QueryBuilder>
    {
        private string _query;

        public QueryBuilder(string query) 
        {
            _query = query;
        }

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

        public QueryBuilder AddGroupByClause(string groupByClause)
        {
            _query += $" GROUP BY {groupByClause}";
            return this;
        }

        public QueryBuilder AddOrderByClause(string orderByClause) 
        {
            _query += $"ORDER BY {orderByClause}";
            return this;
        }

        public string Build() 
        {
            return _query;
        }
    }
}
