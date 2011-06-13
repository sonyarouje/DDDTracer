using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDD.DomainModel
{
    public interface ISalesTaxCalculator
    {
        Money CalculateTax(OrderLine orderLine);
    }
}
