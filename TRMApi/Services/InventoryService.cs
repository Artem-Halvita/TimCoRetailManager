using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TRMApi.Repository;
using TRMDataManager.Library.Models;

namespace TRMApi.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IRepository<InventoryModel, int> _inventoryRepository;

        public InventoryService(IRepository<InventoryModel, int> inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public List<InventoryModel> GetInventories() => _inventoryRepository.GetAll();

        public async Task AddInventoryAsync(InventoryModel inventory) => await _inventoryRepository.InsertAsync(inventory);
    }
}
