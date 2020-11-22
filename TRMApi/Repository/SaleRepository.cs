using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TRMDataManager.Library.Internal.DataAccess;
using TRMDataManager.Library.Models;

namespace TRMApi.Repository
{
    public class SaleRepository : IRepository<SaleAndSaleDetailDBModel, int>
    {
        private readonly IConfiguration _configuration;

        public SaleRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Task DeleteAsync(SaleAndSaleDetailDBModel entity)
        {
            throw new NotImplementedException();
        }

        public List<SaleAndSaleDetailDBModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<SaleAndSaleDetailDBModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task InsertAsync(SaleAndSaleDetailDBModel sale)
        {
            using (SqlDataAccess sql = new SqlDataAccess(_configuration))
            {
                try
                {
                    sql.StartTransaction("TRMData");

                    // Save the sale model
                    await sql.SaveDataInTransactionAsync("dbo.spSale_Insert", sale.Sale);

                    // Get the ID from the sale mode
                    sale.Sale.Id = sql.LoadDataInTransactionAsync<int, object>("spSale_Lookup", new { sale.Sale.CashierId, sale.Sale.SaleDate }).Result.FirstOrDefault();

                    // Finish filling in the sale detail models
                    foreach (var item in sale.SaleDetails)
                    {
                        item.SaleId = sale.Sale.Id;
                        // Save the sale detail models
                        await sql.SaveDataInTransactionAsync("dbo.spSaleDetail_Insert", item);
                    }

                    sql.CommitTransaction();
                }
                catch
                {
                    sql.RollbackTransaction();
                    throw;
                }
            }
        }

        public Task UpdateAsync(SaleAndSaleDetailDBModel entity)
        {
            throw new NotImplementedException();
        }
    }
}