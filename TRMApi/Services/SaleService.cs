using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TRMApi.Data.Repository;
using TRMApi.Data.Repository.DataAccess;
using TRMApi.Data.Models;

namespace TRMApi.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository<SaleModel> _saleRepository;
        private readonly IProductService _productService;
        private readonly IConfiguration _configuration;

        public SaleService(ISaleRepository<SaleModel> saleRepository,
                            IProductService productService,
                            IConfiguration configuration)
        {
            _saleRepository = saleRepository;
            _productService = productService;
            _configuration = configuration;
        }

        public async Task AddSaleAsync(SaleModel saleInfo, string cashierId)
        {
            List<SaleDetailDBModel> details = new List<SaleDetailDBModel>();
            var taxRate = ConfigHelper.GetTaxRate(_configuration) / 100;

            foreach (var item in saleInfo.SaleDetails)
            {
                var detail = new SaleDetailDBModel
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };

                // Get the information about this product
                var productInfo = await _productService.GetProductByIdAsync(detail.ProductId);

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
                try
                {
                    sql.StartTransaction("TRMData");

                    // Save the sale model
                    sql.SaveDataInTransaction("dbo.spSale_Insert", sale);

                    // Get the ID from the sale mode
                    sale.Id = sql.LoadDataInTransaction<int, object>("spSale_Lookup", new { sale.CashierId, sale.SaleDate }).FirstOrDefault();

                    // Finish filling in the sale detail models
                    foreach (var item in details)
                    {
                        item.SaleId = sale.Id;
                        // Save the sale detail models
                        sql.SaveDataInTransaction("dbo.spSaleDetail_Insert", item);
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

        public List<SaleReportModel> GetSaleReport() => _saleRepository.GetSaleReport();
    }
}