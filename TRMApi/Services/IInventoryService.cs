using System.Collections.Generic;
using System.Threading.Tasks;
using TRMApi.Data.Models;

namespace TRMApi.Services
{
    public interface IInventoryService
    {
        Task AddInventoryAsync(InventoryModel inventory);
        List<InventoryModel> GetInventories();
    }
}