namespace FrisianPortsREST_API.Repositories
{
    //Repository Pattern
    public interface IRepository<T>
    {
        Task<List<T>> Get();

        Task<List<T>> GetById();

        Task<int> Add(T itemToAdd);

        Task<int> Update(T itemToUpdate);

        int Delete(T itemToRemove);
    }
}
