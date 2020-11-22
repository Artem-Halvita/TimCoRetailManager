using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TRMApi.Repository;
using TRMDataManager.Library;
using TRMDataManager.Library.Models;

namespace TRMApi.Services
{
    public class SaleService : ISaleService
    {
        private readonly IRepository<SaleAndSaleDetailDBModel, int> _saleRepository;
        private readonly IProductService _productService;
        private readonly IConfiguration _configuration;

        public SaleService(IRepository<SaleAndSaleDetailDBModel, int> saleRepository,
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

            SaleAndSaleDetailDBModel saleAndSaleDetail = new SaleAndSaleDetailDBModel()
            {
                Sale = sale,
                SaleDetails = details
            };

            await _saleRepository.InsertAsync(saleAndSaleDetail);
        }
    }
}