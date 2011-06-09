using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDD.DomainModel
{
    public class TaxableItemRule: AbstractSalesTaxRules
    {
        private const int TAX_AMT = 10;
        private AbstractSalesTaxRules _nextTaxRule;
        public TaxableItemRule(AbstractSalesTaxRules nextTax)
        {
            _nextTaxRule = nextTax;
        }
        public override Money GetTax(OrderLine orderLine)
        {
            Money tax = Money.Empty();
            if(orderLine.IsTaxable())
                tax = new Money((orderLine.GetCost().Value * TAX_AMT) / 100);
            if(_nextTaxRule!=null)
                tax=tax + _nextTaxRule.GetTax(orderLine);

            return tax;
        }
    }
}
