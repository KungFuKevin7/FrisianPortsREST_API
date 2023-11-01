namespace FrisianPortsREST_API.Repositories
{
    //Repository Pattern
    public interface IRepository<T>
    {
        /// <summary>
        /// Retrieves all items from the database of the desired type
        /// </summary>
        /// <returns>List of desired Type</returns>
        Task<List<T>> Get();

        /// <summary>
        /// Retrieves an item from the database by their unique id
        /// </summary>
        /// <param name="idOfItem">id number of desired item</param>
        /// <returns>Single object with corresponding id</returns>
        Task<T> GetById(int idOfItem);

        /// <summary>
        /// Adds an item to the database
        /// </summary>
        /// <param name="itemToAdd">Item of the given type</param>
        /// <returns>Id of the newly created resource</returns>
        Task<int> Add(T itemToAdd);

        /// <summary>
        /// Updates an already existing item
        /// </summary>
        /// <param name="itemToUpdate">The renewed item</param>
        /// <returns>Number of rows effected by execution</returns>
        Task<int> Update(T itemToUpdate);

        /// <summary>
        /// Deletes an item from the database
        /// </summary>
        /// <param name="idOfItemToDelete">Id of item to be deleted</param>
        /// <returns>Number of rows effected by execution</returns>
        int Delete(int idOfItemToDelete);
    }
}
