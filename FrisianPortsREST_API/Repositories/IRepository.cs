namespace FrisianPortsREST_API.Repositories
{
    //Repository Pattern
    public interface IRepository<T>
    {
        Task<List<T>> Get();

        Task<T> GetById(int idOfItem);

        Task<int> Add(T itemToAdd);

        Task<int> Update(T itemToUpdate);

        int Delete(int idOfItemToDelete);
    }
}
