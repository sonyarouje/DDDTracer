using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

using NHibernate;
using NHibernate.Linq;
using NHibernate.Cfg;
using NHibernate.Proxy;
using NHibernate.Tool.hbm2ddl;
using System.Reflection;
namespace DDD.Infrastructure.Persistance
{
    public class HibernateRepo:IRepository
    {
        IList _itemsAdded = new System.Collections.ArrayList();//  List();
        private static string _connectionString;

        public HibernateRepo()
        {
            //_connectionString = connectionString;
        }
        private static ISessionFactory CreateSessionFactory()
        {
            Configuration cfg = new Configuration().Configure();//.AddAssembly(typeof(HibernateRepo).Assembly);
            return cfg.BuildSessionFactory();
        }

        ISession _session;
        private ISession Session
        {
            get 
            {
                if (_session == null)
                    _session = CreateSessionFactory().OpenSession();
                if (_session.IsOpen == false)
                    _session = CreateSessionFactory().OpenSession();
                if (_session.Connection.State != System.Data.ConnectionState.Open)
                {
                    _session.Connection.Close();
                    _session.Connection.Open();
                }
                return _session;
            }
        }

        ITransaction _currentTransaction = null;
        public void BeginTransaction()
        {
            _currentTransaction= this.Session.BeginTransaction();
        }
        public void CommitTransaction()
        {
            _currentTransaction.Commit();
        }
        public void Rollback()
        {
            if(_currentTransaction!=null)
                _currentTransaction.Rollback();
        }

        public bool IsProxyCreationEnabled
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

       
        public IQueryable<TEntity> GetQuery<TEntity>() where TEntity : class
        {
            return this.Session.Query<TEntity>();
        }

        public IList<TEntity> GetAll<TEntity>() where TEntity : class
        {
            return this.GetQuery<TEntity>().AsEnumerable<TEntity>().ToList();
        }

        public IList<TEntity> Find<TEntity>(System.Linq.Expressions.Expression<Func<TEntity, bool>> criteria) where TEntity : class
        {
            IList<TEntity> entities= this.GetQuery<TEntity>().Where(criteria).ToList();
            NHibernateUtil.Initialize(entities);
            return entities;
        }

        public TEntity Single<TEntity>(System.Linq.Expressions.Expression<Func<TEntity, bool>> criteria) where TEntity : class
        {
            TEntity entity= this.GetQuery<TEntity>().SingleOrDefault<TEntity>(criteria);
            return entity;
        }

        public void InitializeAndLoad(object entityToLoad)
        {
            //NHibernateUtil.Initialize(entityToLoad);
            //NHibernateProxyUtils.UnproxyObjectTree<HibernateRepo>(this, CreateSessionFactory(), 3);
        }
        public TEntity First<TEntity>(System.Linq.Expressions.Expression<Func<TEntity, bool>> criteria) where TEntity : class
        {
            TEntity entity = this.GetQuery<TEntity>().FirstOrDefault<TEntity>(criteria);
            return entity;
        }

        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
            _itemsAdded.Add(entity);
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            this.Session.Delete(entity);
        }

        public void Delete<TEntity>(IEnumerable<TEntity> entites) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public void Attach<TEntity>(TEntity entity) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public void Attach<TEntity>(IEnumerable<TEntity> entites) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            for (int i = 0; i < _itemsAdded.Count; i++)
            {
                this.Session.SaveOrUpdate(_itemsAdded[i]);
            }
        }

        public bool EnableEagerLoading
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Update<TEntity>(TEntity entity, TEntity old) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            if (this.Session != null)
            {
                _session.Close();
                _session.Dispose();
            }
            if (this._currentTransaction != null)
                _currentTransaction.Dispose();
        }
    }
}
