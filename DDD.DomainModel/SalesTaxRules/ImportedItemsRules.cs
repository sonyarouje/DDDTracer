using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDD.DomainModel
{
    public class ImportedItemsRule:AbstractSalesTaxRules
    {
        public AbstractSalesTaxRules _nextTaxRule;
        
        public ImportedItemsRule(AbstractSalesTaxRules nextTax)
        {
            _nextTaxRule = nextTax;
        }
        public override Money GetTax(OrderLine orderLine)
        {
            Money tax = Money.Empty();
            if (orderLine.IsImported())
                tax = new Money((orderLine.GetCost().Value * 5) / 100);

            if (_nextTaxRule != null)
                tax += _nextTaxRule.GetTax(orderLine);

            return tax;
        }
    }
}
