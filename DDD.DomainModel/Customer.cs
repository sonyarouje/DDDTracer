using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDD.DomainModel
{
    public class Customer
    {
        private int _id;
        private string _name;
        private string _addres;
        public Customer(int id, string name, string address)
        {
            this._id = id;
            this._name = name;
            this._addres = address;
        }

        public int GetId()
        {
            return this._id;
        }
        public string GetName()
        {
            return this._name;
        }
        public string GetAddress()
        {
            return this._addres;
        }
    }
}
