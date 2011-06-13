using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Core;
using DDD.DomainModel;
using DDD.Infrastructure.Persistance;
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
            Order order = new Order(new Customer("sony", "blr"), salesCalculator);
            Assert.AreNotEqual(order, null, "Order created successfully");
            order.With(new OrderLine(_productCatalogs[0],1))
                .With(new OrderLine(_productCatalogs[1],1));
            Assert.AreEqual(28.98, order.GetGrandTotal().Value);
            Assert.AreEqual(1.5, order.GetTotalTax().Value );
        }

        [Test]
        public void CanCreateCustomer()
        {
            IRepository repo = new HibernateRepo();

            Customer customer = new Customer("Sony", "Blr");
            repo.BeginTransaction();
            repo.Add<Customer>(customer);
            repo.SaveChanges();
            repo.CommitTransaction();
        }

        [Test]
        public void GetCustomerTest()
        {
            IRepository repo = new HibernateRepo();
            repo.BeginTransaction();
            IList<Customer> custs = repo.GetAll<Customer>();
            repo.CommitTransaction();
            repo.Dispose();
            Assert.AreEqual(1, custs.Count);
        }

        [Test]
        public void CreateProductCatalogs()
        {
            IRepository repo = new HibernateRepo();
            _productCatalogs = new List<ProductCatalog>();
            _productCatalogs.Add(new ProductCatalog("CLR via C#", new Money(12.49), false, false));
            _productCatalogs.Add(new ProductCatalog("Pirates of Cariaben", new Money(14.99), true, false));
            try
            {
                repo.BeginTransaction();
                for (int i = 0; i < _productCatalogs.Count; i++)
                {
                    repo.Add<ProductCatalog>(_productCatalogs[i]);
                }
                repo.SaveChanges();
                repo.CommitTransaction();
            }
            catch (Exception ex)
            {
                repo.Rollback();
                throw ex;
            }
            finally
            {
                repo.Dispose();
            }
        }

        [Test]
        public void CanPersistOrder()
        {
            IRepository repo = new HibernateRepo();
            try
            {
                repo.BeginTransaction();
                Customer customer = repo.First<Customer>(c => c.CustomerId == 1);
                ISalesTaxCalculator salesCalculator = new SalesTaxCalculator();
                Order order = new Order(customer, salesCalculator);
                order.With(new OrderLine(this.GetProduct(repo,3), 1))
                     .With(new OrderLine(this.GetProduct(repo, 4), 12));
                repo.Add<Order>(order);
                repo.SaveChanges();
                repo.CommitTransaction();
            }
            catch (Exception ex)
            {
                repo.Rollback();
                throw ex;
            }
            finally
            {
                repo.Dispose();
            }

        }

        [Test]
        public void CanFetchOrder()
        {
            IRepository repo = new HibernateRepo();
            repo.BeginTransaction();
            Order order = repo.First<Order>(o => o.OrderId == 3);
            Assert.AreEqual(2, order.GetNumberOfItems());
        }

        private ProductCatalog GetProduct(IRepository repo,int id)
        {
            return repo.First<ProductCatalog>(p => p.ProductId == id);
        }
    }
}
