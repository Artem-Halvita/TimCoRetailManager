using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TRMApi.Data.Repository.DataAccess;
using TRMApi.Data.Models;
using TRMApi.Services;

namespace TRMApi.Data.Repository
{
    public class SaleRepository : ISaleRepository<SaleModel>
    {
        private readonly IConfiguration _configuration;

        public SaleRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task InsertInTransationAsync(SaleModel saleInfo, string cashierId)
        {
            // TODO : Make this SOLID/DRY/Better
            // Start filling in the sale detail models we will save to the database
            List<SaleDetailDBModel> details = new List<SaleDetailDBModel>();
            ProductRepository products = new ProductRepository(_configuration);
            var taxRate = ConfigHelper.GetTaxRate(_configuration) / 100;

            foreach (var item in saleInfo.SaleDetails)
            {
                var detail = new SaleDetailDBModel
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };

                // Get the information about this product
                var productInfo = await products.GetByIdAsync(detail.ProductId);

                if (productInfo == null)
                {
                    throw new Exception($"The product Id of { detail.ProductId } could not be found in the database.");
                }

                detail.PurchasePrice = (productInfo.RetailPrice * detail.Quantity);

                if (productInfo.IsTaxable)
                {
                    detail.Tax = (detail.PurchasePrice * taxRate);
                }

                details.Add(detail);
            }

            // Create the Sale model
            SaleDBModel sale = new SaleDBModel()
            {
                SubTotal = details.Sum(x => x.PurchasePrice),
                Tax = details.Sum(x => x.Tax),
                CashierId = cashierId
            };

            sale.Total = sale.SubTotal + sale.Tax;

            using (SqlDataAccess sql = new SqlDataAccess(_configuration))
            {
                sql.RunInTransaction(action =>
                {
                    // Save the sale model
                    action.SaveDataInTransaction(StoredProcedures.InsertSale, sale);

                    // Get the ID from the sale mode
                    sale.Id = action.LoadDataInTransaction<int, object>(StoredProcedures.GetSaleByCahierIdAndDate, new { sale.CashierId, sale.SaleDate }).FirstOrDefault();

                    // Finish filling in the sale detail models
                    foreach (var item in details)
                    {
                        item.SaleId = sale.Id;
                        // Save the sale detail models
                        action.SaveDataInTransaction(StoredProcedures.InsertSaleDetail, item);
                    }
                }, "TRMData");
            }
        }

        public List<SaleReportModel> GetSaleReport()
        {
            SqlDataAccess sql = new SqlDataAccess(_configuration);

            var output = sql.LoadData<SaleReportModel, object>(StoredProcedures.SaleReport, new { }, ConnectionStringName.TRMData);

            return output;
        }
    }
}