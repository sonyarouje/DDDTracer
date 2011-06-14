using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDD.DomainModel
{
    public class ImportedItemsRule:ISalesTaxRules
    {
        public ISalesTaxRules _nextTaxRule;
        
        public ImportedItemsRule(ISalesTaxRules nextTax)
        {
            _nextTaxRule = nextTax;
        }
        public Money GetTax(OrderLine orderLine)
        {
            Money tax = Money.Empty();
            if (orderLine.TaxSpecification.IsImported())
                tax = new Money((orderLine.GetCost().Value * 5) / 100);

            if (_nextTaxRule != null)
                tax += _nextTaxRule.GetTax(orderLine);

            return tax;
        }
    }
}
