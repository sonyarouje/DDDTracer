using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Core;
using DDD.DomainModel;
namespace DDD.DomainTest
{
    [TestFixture]
    public class TestOrder
    {
        List<ProductCatalog> _productCatalogs;
        [SetUp]
        public void SetupOrder()
        {
            _productCatalogs = new List<ProductCatalog>();
            _productCatalogs.Add(new ProductCatalog("CLR via C#",new Money(12.49),false,false));
            _productCatalogs.Add(new ProductCatalog("Pirates of Cariaben",new Money (14.99), true, false));
        }
        [TearDown]
        public void Cleanup()
        {
            _productCatalogs.Clear();
            _productCatalogs = null;
        }
        [Test]
        public void CreateOrder()
        {
            ISalesTaxCalculator salesCalculator = new SalesTaxCalculator();
            Order order = new Order(new Customer(1, "sony", "blr"), salesCalculator);
            Assert.AreNotEqual(order, null, "Order created successfully");
            order.With(new OrderLine(_productCatalogs[0],1))
                .With(new OrderLine(_productCatalogs[1],1));
            Assert.AreEqual(28.98, order.GetGrandTotal().Value);
            Assert.AreEqual(1.5, order.GetTotalTax().Value );
        }
    }
}
