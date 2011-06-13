using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDD.DomainModel
{
    public class Order
    {
        private IList<OrderLine> _lineItems;
        private Customer _customer;
        private ISalesTaxCalculator _salesCalculator;
        private int _orderId;

        protected Order() 
        {
            //For persistance 
        } 
        public Order(Customer customer, ISalesTaxCalculator salesCalculator)
        {
            this._customer = customer;
            this._lineItems = new List<OrderLine>();
            this._salesCalculator = salesCalculator;
        }

        public int OrderId
        {
            get { return _orderId; }
        }
        public Order With(OrderLine orderLine)
        {
            Money tax = _salesCalculator.CalculateTax(orderLine);
            orderLine.SetTaxAmount(tax);
            orderLine.SetOrder(this);
            _lineItems.Add(orderLine);
            return this;
        }
        public Money GetTotalTax()
        {
            Money totalTax = Money.Empty();
            for (int i = 0; i < _lineItems.Count; i++)
                totalTax += _lineItems[i].GetTaxAmount();
            return totalTax;
        }
        public Money GetGrandTotal()
        {
            Money total = Money.Empty();
            for (int i = 0; i < _lineItems.Count; i++)
                total += _lineItems[i].GetSubTotal();
            return total;
        }
        
        public int GetNumberOfItems()
        {
            return this._lineItems.Count();
        }
        public virtual IList<OrderLine> LineItems
        {
            get { return _lineItems; }
        }
        public virtual Customer Customer
        {
            get
            {
                return _customer;
            }
        }
    }
}
