using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDD.DomainModel
{
    public class TaxSpecification
    {
        private OrderLine _lineItem;
        public TaxSpecification(OrderLine lineItem)
        {
            this._lineItem = lineItem;
        }

        public bool IsTaxable()
        {
            return _lineItem.GetProduct().IsTaxable();
        }

        public bool IsImported()
        {
            return _lineItem.GetProduct().IsImported();
        }
    }
}
