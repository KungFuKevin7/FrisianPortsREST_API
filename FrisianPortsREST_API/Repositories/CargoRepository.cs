using FrisianPortsREST_API.Models;

namespace FrisianPortsREST_API.Repositories
{
    public class CargoRepository : IRepository<Cargo>
    {
        public Task<int> Add(Cargo itemToAdd)
        {
            throw new NotImplementedException();
        }

        public int Delete(Cargo itemToRemove)
        {
            throw new NotImplementedException();
        }

        public Task<List<Cargo>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<List<Cargo>> GetById()
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(Cargo itemToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
