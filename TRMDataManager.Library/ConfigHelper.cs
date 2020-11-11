using System;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace TRMDataManager.Library
{
    public class ConfigHelper
    {
        // TODO : Move this from config to the API
        public static decimal GetTaxRate(IConfiguration configuration)
        {
            decimal output = 0;

            string rateText = configuration["TaxRate"];

            bool isValidTaxRate = Decimal.TryParse(rateText, out output);

            if (isValidTaxRate == false)
            {
                throw new ConfigurationErrorsException("The tax rate is not set up properly");
            }

            return output;
        }
    }
}
