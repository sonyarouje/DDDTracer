using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDD.DomainModel
{
    public class NonTaxableRule:AbstractSalesTaxRules
    {
        AbstractSalesTaxRules _nextTaxRule;
        public NonTaxableRule(AbstractSalesTaxRules nextTax)
        {
            _nextTaxRule = nextTax;
        }
        public override Money GetTax(OrderLine orderLine)
        {
            Money tax = Money.Empty();
            tax= base.GetTax(orderLine);
            if (_nextTaxRule != null)
                tax += _nextTaxRule.GetTax(orderLine);

            return tax;
        }
    }
}
