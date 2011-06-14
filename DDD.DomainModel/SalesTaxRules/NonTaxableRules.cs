using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDD.DomainModel
{
    public class NonTaxableRule:ISalesTaxRules
    {
        ISalesTaxRules _nextTaxRule;
        public NonTaxableRule(ISalesTaxRules nextTax)
        {
            _nextTaxRule = nextTax;
        }
        public Money GetTax(OrderLine orderLine)
        {
            Money tax = Money.Empty();
            if (_nextTaxRule != null)
                tax += _nextTaxRule.GetTax(orderLine);

            return tax;
        }
    }
}
