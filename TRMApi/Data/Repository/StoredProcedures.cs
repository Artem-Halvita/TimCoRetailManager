using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TRMApi.Data.Repository
{
    public static class StoredProcedures
    {
        public const string GetInventories = "dbo.spInventory_GetAll";
        public const string InsertInventory = "dbo.spInventory_Insert";
        public const string GetProducts = "dbo.spProduct_GetAll";
        public const string GetProductById = "dbo.spProduct_GetById";
        public const string InsertSale = "dbo.spSale_Insert";
        public const string GetSaleByCahierIdAndDate = "spSale_Lookup";

        public const string InsertSaleDetail = "dbo.spSaleDetail_Insert";
        public const string SaleReport = "dbo.spSale_SaleReport";
        public const string GetUserById = "dbo.spUserLookup";

    }
}
