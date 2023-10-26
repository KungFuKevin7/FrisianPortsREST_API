using FrisianPortsREST_API.Models;

namespace FrisianPortsREST_API.Repositories
{
    public class TransportRepository : IRepository<Transport>
    {
        public Task<int> Add(Transport itemToAdd)
        {
            throw new NotImplementedException();
        }

        public int Delete(int itemToRemove)
        {
            throw new NotImplementedException();
        }

        public Task<List<Transport>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<List<Transport>> GetById(int idOfItem)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(Transport itemToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
