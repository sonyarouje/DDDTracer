using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDD.DomainModel
{
    public class Customer
    {
        private int _customerId;
        private string _name;
        private string _address;
        private IList<Order> _orders;

        public Customer (){}
        public Customer(string name, string address)
        {
            this._name = name;
            this._address = address;
        }

        public int CustomerId
        {
            get { return this._customerId; }
        }
        public string GetName()
        {
            return this._name;
        }
        public string GetAddress()
        {
            return this._address;
        }

        public virtual IList<Order> Orders
        {
            get { return _orders; }
        }
    }
}
