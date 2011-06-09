using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDD.DomainModel
{
    public static class SalesTaxCalculator
    {
        static AbstractSalesTaxRules _salesTax;
        private static void CreateTaxInstance()
        {
            ImportedItemsRule importTax = new ImportedItemsRule(null);
            TaxableItemRule taxable = new TaxableItemRule(importTax);
            _salesTax = new NonTaxableRule(taxable);
        }
        public static Money GetTaxAmount(OrderLine orderLine)
        {
            if (_salesTax == null)
                CreateTaxInstance();

            return _salesTax.GetTax(orderLine);

        }
    }
}
