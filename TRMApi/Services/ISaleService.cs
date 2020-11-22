using System.Threading.Tasks;
using TRMDataManager.Library.Models;

namespace TRMApi.Services
{
    public interface ISaleService
    {
        Task AddSaleAsync(SaleModel saleInfo, string cashierId);
    }
}