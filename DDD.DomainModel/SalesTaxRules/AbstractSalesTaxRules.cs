using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDD.DomainModel
{
    public class AbstractSalesTaxRules
    {
        public virtual Money GetTax(OrderLine orderLine)
        {
            return new Money(0);
        }
    }
}
