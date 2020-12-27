using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TRMApi.Data.Repository.DataAccess;
using TRMApi.Data.Models;

namespace TRMApi.Data.Repository
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
            SqlDataAccess sql = new SqlDataAccess(_configuration);

            var output = sql.LoadData<ProductModel, object>(StoredProcedures.GetProducts, new { }, ConnectionStringName.TRMData);

            return output;
        }

        public async Task<ProductModel> GetByIdAsync(int id)
        {
            SqlDataAccess sql = new SqlDataAccess(_configuration);

            var output = await sql.LoadDataAsync<ProductModel, object>(StoredProcedures.GetProductById, new { Id = id }, ConnectionStringName.TRMData);
            var result = output.FirstOrDefault();

            return result;
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
