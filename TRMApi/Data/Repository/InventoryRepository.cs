using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TRMApi.Data.Repository.DataAccess;
using TRMApi.Data.Models;

namespace TRMApi.Data.Repository
{
    public class InventoryRepository : IRepository<InventoryModel, int>
    {
        private readonly IConfiguration _configuration;

        public InventoryRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<InventoryModel> GetAll()
        {
            SqlDataAccess sql = new SqlDataAccess(_configuration);

            var output = sql.LoadData<InventoryModel, object>("dbo.spInventory_GetAll", new { }, "TRMData");

            return output;
        }

        public async Task InsertAsync(InventoryModel entity)
        {
            SqlDataAccess sql = new SqlDataAccess(_configuration);

            await sql.SaveDataAsync("dbo.spInventory_Insert", entity, "TRMData");
        }

        public Task DeleteAsync(InventoryModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<InventoryModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(InventoryModel entity)
        {
            throw new NotImplementedException();
        }
    }
}