using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TRMApi.Repository;
using TRMDataManager.Library.Models;

namespace TRMApi.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<ProductModel, int> _productRepository;

        public ProductService(IRepository<ProductModel, int> productRepository)
        {
            _productRepository = productRepository;
        }

        public List<ProductModel> GetProducts() => _productRepository.GetAll();

        public async Task<ProductModel> GetProductByIdAsync(int productId) => await _productRepository.GetByIdAsync(productId);
        public async Task AddProductAsync(ProductModel product) => await _productRepository.InsertAsync(product);
    }
}
