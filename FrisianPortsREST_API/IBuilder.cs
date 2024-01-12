
namespace FrisianPortsREST_API
{
    public interface IBuilder<T>
    {
        T AddFilter(string partToAdd);
        
        T AddGroupByClause(string partToAdd);

        T AddOrderByClause(string partToAdd);
        string Build();
    }
}
