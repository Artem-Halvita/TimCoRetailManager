using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TRMDataManager.Library.DataAccess;
using TRMDataManager.Library.Models;

namespace TRMApi.Repository
{
    public class ProductRepository : IRepository<ProductModel, int>
    {
        private readonly IConfiguration _configuration;

        public ProductRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<ProductModel> GetAll()
        {
            ProductData productData = new ProductData(_configuration);
            var output = productData.GetProducts();

            return output;
        }

        public Task<ProductModel> GetByIdAsync(int id)
        {
            ProductData productData = new ProductData(_configuration);
            var output = productData.GetProductByIdAsync(id);

            return output;
        }

        public Task InsertAsync(ProductModel entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ProductModel entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(ProductModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
