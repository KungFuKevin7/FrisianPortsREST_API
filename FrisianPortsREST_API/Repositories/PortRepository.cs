using Dapper;
using FrisianPortsREST_API.Models;
using MySql.Data.MySqlClient;

namespace FrisianPortsREST_API.Repositories
{
    public class PortRepository : IRepository<Port>
    {

        public static MySqlConnection Connection = DBConnection.getConnection();
        
        public async Task<List<Port>> Get() 
        {
            string query = "SELECT * FROM Port";
            var list = await Connection.QueryAsync<Port>(query);
            return list.ToList();
        }
        public Task<List<Port>> GetById()
        {
            throw new NotImplementedException();
        }

        public Task<int> Add(Port itemToAdd)
        {
            throw new NotImplementedException();
        }

        public int Delete(Port itemToRemove)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(Port itemToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
