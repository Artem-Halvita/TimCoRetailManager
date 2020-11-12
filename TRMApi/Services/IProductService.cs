using System.Collections.Generic;
using System.Threading.Tasks;
using TRMDataManager.Library.Models;

namespace TRMApi.Services
{
    public interface IProductService
    {
        Task AddProductAsync(ProductModel product);
        List<ProductModel> GetProducts();
    }
}