using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDD.DomainModel
{
    public class ProductCatalog
    {
        private string _productName;
        private Money _price;
        private bool _isTaxable;
        private bool _isImported;
        public ProductCatalog(string productName, Money price, bool isTaxable, bool isImported)
        {
            this._productName = productName;
            this._price = price;
            this._isImported = isImported;
            this._isTaxable = isTaxable;
        }
        public Money GetPrice()
        {
            return _price;
        }
        public bool IsTaxable()
        {
            return _isTaxable;
        }
        public bool IsImported()
        {
            return _isImported;
        }
    }
}
