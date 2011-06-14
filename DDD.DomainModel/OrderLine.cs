using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDD.DomainModel
{
    public class OrderLine
    {
        private ProductCatalog _product=null;
        private double _quantity=0;
        private Money _taxAmt = Money.Empty();
        private int _orderLineId;
        private Order _order;
        private TaxSpecification _taxSpecification;
        public OrderLine(){}
        public OrderLine(ProductCatalog product, double quantity)
        {
            this._product = product;
            this._quantity = quantity;
            this._taxSpecification = new TaxSpecification(this);
        }
        public TaxSpecification TaxSpecification
        {
            get { return _taxSpecification; }
        }
        public int OrderLineId
        {
            get { return _orderLineId; }
        }
        internal void SetOrder(Order order)
        {
            _order = order;
        }
        public double GetQuantity()
        {
            return this._quantity;
        }
        public ProductCatalog GetProduct()
        {
            return _product;
        }

        public Money GetCost()
        {
            return  new Money(_product.GetPrice().Value * this._quantity);
        }
        public Money GetSubTotal()
        {
            return this.GetCost() + _taxAmt;
        }
        internal void SetTaxAmount(Money taxAmt)
        {
            _taxAmt = taxAmt;
        }
        public Money GetTaxAmount()
        {
            return _taxAmt;
        }
    }
}
