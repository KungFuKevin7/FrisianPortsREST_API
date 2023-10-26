using FrisianPortsREST_API.Models;

namespace FrisianPortsREST_API.Repositories
{
    public class TransportRepository : IRepository<Transport>
    {
        public Task<int> Add(Transport itemToAdd)
        {
            throw new NotImplementedException();
        }

        public int Delete(Transport itemToRemove)
        {
            throw new NotImplementedException();
        }

        public Task<List<Transport>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<List<Transport>> GetById()
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(Transport itemToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
