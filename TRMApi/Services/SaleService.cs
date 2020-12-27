using System.Collections.Generic;
using System.Threading.Tasks;
using TRMApi.Data.Repository;
using TRMApi.Data.Models;

namespace TRMApi.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository<SaleModel> _saleRepository;

        public SaleService(ISaleRepository<SaleModel> saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task AddSaleAsync(SaleModel saleInfo, string cashierId) => await _saleRepository.InsertInTransationAsync(saleInfo, cashierId);

        public List<SaleReportModel> GetSaleReport() => _saleRepository.GetSaleReport();
    }
}