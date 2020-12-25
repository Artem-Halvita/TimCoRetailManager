using System;
using System.Configuration;

namespace TRMDataManager.Library
{
    public class ConfigHelper
    {
        // TODO : Move this from config to the API
        public static decimal GetTaxRate()
        {
            decimal output = 0;

            // TODO : Doesnt work fix that
            //string rateText = ConfigurationManager.AppSettings["taxRate"];

            string rateText = "8,75";

            bool isValidTaxRate = Decimal.TryParse(rateText, out output);

            if (isValidTaxRate == false)
            {
                throw new ConfigurationErrorsException("The tax rate is not set up properly");
            }

            return output;
        }
    }
}
