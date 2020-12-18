using System.Collections.Generic;
using System.Threading.Tasks;
using TRMApi.Data.Models;

namespace TRMApi.Data.Repository
{
    public interface ISaleRepository<T> where T : class
    {
        Task InsertInTransationAsync(T entity, string cashierId);

        public List<SaleReportModel> GetSaleReport();
    }
}
