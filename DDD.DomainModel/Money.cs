using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDD.DomainModel
{
    public class Money
    {
        public Money() { }
        public Money(double value)
        {
            _value = value;
        }
        private double _value;
        public void SetValue(double value)
        {
            _value = Math.Round(value, 2);
        }

        private double GetValue()
        {
            double tmp = Math.Round(_value, 2);
            if ((tmp - _value) < 0)
            {
                tmp += 0.5;
            }
            _value = tmp;
            return _value;
        }
        public double Value
        {
            get { return Math.Round(_value, 2); }
        }
        public static Money operator +(Money m1, Money m2)
        {
            m1.SetValue (m1.GetValue() + m2.GetValue());
            return m1;
        }
        public static Money operator * (Money m1, Money m2)
        {
            m1.SetValue(m1.GetValue() * m2.GetValue());
            return m1;
        }

        public static Money Empty()
        {
            return new Money(0);
        }
    }
}
