using System.Collections.Generic;
using System.Threading.Tasks;
using TRMApi.Data.Models;

namespace TRMApi.Services
{
    public interface IProductService
    {
        Task AddProductAsync(ProductModel product);
        Task<ProductModel> GetProductByIdAsync(int productId);
        List<ProductModel> GetProducts();
    }
}