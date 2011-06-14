using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDD.DomainModel
{
    public class SalesTaxCalculator : ISalesTaxCalculator
    {
        private ISalesTaxRules _salesTax;
        private void CreateTaxInstance()
        {
            ImportedItemsRule importTax = new ImportedItemsRule(null);
            TaxableItemRule taxable = new TaxableItemRule(importTax);
            _salesTax = new NonTaxableRule(taxable);
        }
        public Money CalculateTax(OrderLine orderLine)
        {
            if (_salesTax == null)
                CreateTaxInstance();

            return _salesTax.GetTax(orderLine);

        }
    }
}
