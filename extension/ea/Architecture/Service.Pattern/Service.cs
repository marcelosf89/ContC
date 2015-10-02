using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.Repositories;

namespace Service.Pattern
{
    public abstract class Service<TEntity> : IService<TEntity> where TEntity : class, IObjectState
    {
        #region Private Fields
        public IRepository<TEntity> _repository;
        #endregion Private Fields

        public virtual TEntity Find(object keyValues) { return _repository.Find(keyValues); }

        public virtual IList<TEntity> SelectQuery(string query, params object[] parameters) { return _repository.SelectQuery(query, parameters); }

        public virtual void Insert(TEntity entity) { _repository.Insert(entity); }

        public virtual void InsertRange(IEnumerable<TEntity> entities) { _repository.InsertRange(entities); }

        public virtual void Update(TEntity entity) { _repository.Update(entity); }

        public virtual void UpdateRange(IEnumerable<TEntity> entities) { _repository.UpdateRange(entities); }

        public virtual void Delete(object id) { _repository.Delete(id); }

        public virtual void Delete(TEntity entity) { _repository.Delete(entity); }

    }
}