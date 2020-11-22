using System.Collections.Generic;

namespace TRMDataManager.Library.Models
{
    public class SaleAndSaleDetailDBModel
    {
        public SaleDBModel Sale { get; set; }
        public List<SaleDetailDBModel> SaleDetails { get; set; }
    }
}