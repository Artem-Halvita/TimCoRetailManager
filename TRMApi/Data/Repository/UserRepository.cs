using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TRMApi.Data.Repository.DataAccess;
using TRMApi.Data.Models;

namespace TRMApi.Data.Repository
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
            SqlDataAccess sql = new SqlDataAccess(_configuration);

            var output = await sql.LoadDataAsync<UserModel, object>(StoredProcedures.GetUserById, new { Id = id }, ConnectionStringName.TRMData);
            var result = output.FirstOrDefault();

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
