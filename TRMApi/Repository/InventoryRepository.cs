using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TRMDataManager.Library.DataAccess;
using TRMDataManager.Library.Models;
using Microsoft.Extensions.Configuration;
using TRMApi.Repository;

namespace TRMApi
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
            InventoryData inventoryData = new InventoryData(_configuration);
            var output = inventoryData.GetInventory();

            return output;
        }

        public async Task InsertAsync(InventoryModel entity)
        {
            InventoryData inventoryData = new InventoryData(_configuration);
            await inventoryData.SaveInventoryRecordAsync(entity);
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