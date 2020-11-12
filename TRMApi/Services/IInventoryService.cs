using System.Collections.Generic;
using System.Threading.Tasks;
using TRMDataManager.Library.Models;

namespace TRMApi.Services
{
    public interface IInventoryService
    {
        Task AddInventoryAsync(InventoryModel inventory);
        List<InventoryModel> GetInventories();
    }
}