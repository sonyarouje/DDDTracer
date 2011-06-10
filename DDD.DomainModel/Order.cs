using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDD.DomainModel
{
    public class Order
    {
        private IList<OrderLine> _orderLines;
        private ISalesTaxCalculator _salesCalculator;
        //For persistance another test
        protected Order() { } 
        public Order(Customer customer, ISalesTaxCalculator salesCalculator)
        {
            this._orderLines = new List<OrderLine>();
            this._salesCalculator = salesCalculator;
        }

        public Order With(OrderLine orderLine)
        {
            Money tax = _salesCalculator.CalculateTax(orderLine);
            orderLine.SetTaxAmount(tax);
            _orderLines.Add(orderLine);
            return this;
        }
        public Money GetTotalTax()
        {
            Money totalTax = Money.Empty();
            for (int i = 0; i < _orderLines.Count; i++)
                totalTax += _orderLines[i].GetTaxAmount();
            return totalTax;
        }
        public Money GetGrandTotal()
        {
            Money total = Money.Empty();
            for (int i = 0; i < _orderLines.Count; i++)
                total += _orderLines[i].GetSubTotal();
            return total;
        }
        
        public int GetNumberOfItems()
        {
            return this._orderLines.Count();
        }
    }
}
