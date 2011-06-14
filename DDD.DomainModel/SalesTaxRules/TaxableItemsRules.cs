using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDD.DomainModel
{
    public class TaxableItemRule: ISalesTaxRules
    {
        private const int TAX_AMT = 10;
        private ISalesTaxRules _nextTaxRule;
        public TaxableItemRule(ISalesTaxRules nextTax)
        {
            _nextTaxRule = nextTax;
        }
        public Money GetTax(OrderLine orderLine)
        {
            Money tax = Money.Empty();
            if(orderLine.TaxSpecification.IsTaxable())
                tax = new Money((orderLine.GetCost().Value * TAX_AMT) / 100);
            if(_nextTaxRule!=null)
                tax=tax + _nextTaxRule.GetTax(orderLine);

            return tax;
        }
    }
}
