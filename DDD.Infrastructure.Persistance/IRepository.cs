using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Data;
using System.Linq;

namespace DDD.Infrastructure.Persistance
{
    public interface IRepository:IDisposable
    {
        bool IsProxyCreationEnabled { get; set; }
        IQueryable<TEntity> GetQuery<TEntity>() where TEntity : class;
        IList<TEntity> GetAll<TEntity>() where TEntity : class;
        IList<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class;
        TEntity Single<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class;
        TEntity First<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class;
        void Add<TEntity>(TEntity entity) where TEntity : class;
        void Delete<TEntity>(TEntity entity) where TEntity : class;
        void Delete<TEntity>(IEnumerable<TEntity> entites) where TEntity : class;
        void Attach<TEntity>(TEntity entity) where TEntity : class;
        void Attach<TEntity>(IEnumerable<TEntity> entites) where TEntity : class;
        void SaveChanges();
        bool EnableEagerLoading { get; set; }
        void Update<TEntity>(TEntity entity, TEntity old) where TEntity : class;
        void InitializeAndLoad(object entityToLoad);
        void BeginTransaction();
        void CommitTransaction();
        void Rollback();
    }
}
