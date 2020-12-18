using System.Collections.Generic;
using System.Threading.Tasks;
using TRMApi.Data.Models;

namespace TRMApi.Services
{
    public interface ISaleService
    {
        Task AddSaleAsync(SaleModel saleInfo, string cashierId);

        public List<SaleReportModel> GetSaleReport();
    }
}