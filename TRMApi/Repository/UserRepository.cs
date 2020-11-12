using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TRMDataManager.Library.DataAccess;
using TRMDataManager.Library.Models;

namespace TRMApi.Repository
{
    public class UserRepository : IRepository<UserModel, string>
    {
        private readonly IConfiguration _configuration;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<UserModel> GetByIdAsync(string id)
        {
            UserData userData = new UserData(_configuration);
            var result = await userData.GetUserByIdAsync(id);

            return result;
        }

        public Task DeleteAsync(UserModel entity)
        {
            throw new NotImplementedException();
        }

        public List<UserModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(UserModel entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(UserModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
