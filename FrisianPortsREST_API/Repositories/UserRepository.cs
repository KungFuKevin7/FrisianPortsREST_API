﻿using FrisianPortsREST_API.Models;

namespace FrisianPortsREST_API.Repositories
{
    public class UserRepository : IRepository<Users>
    {
        public Task<int> Add(Users itemToAdd)
        {
            throw new NotImplementedException();
        }

        public int Delete(int itemToRemove)
        {
            throw new NotImplementedException();
        }

        public Task<List<Users>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<List<Users>> GetById(int idOfItem)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(Users itemToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
