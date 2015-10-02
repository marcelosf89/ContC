#region

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using LinqKit;
using Repository.Pattern.DataContext;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using NHibernate;
using ContC.Repositories.Mapping.UnitOfWork;

#endregion

namespace Repository.Pattern.Ef6
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IObjectState
    {

        public ISession SessaoAtual
        {
            get { return UnitOfWorkNHibernate.GetInstancia().SessaoAtual; }
        }

        public virtual TEntity Find(object keyValues)
        {
            return SessaoAtual.Get<TEntity>(keyValues);
        }

        public virtual IList<TEntity> SelectQuery(string query, params object[] parameters)
        {
            ISQLQuery isql = SessaoAtual.CreateSQLQuery(query);

            int idx = 0;
            foreach (object obj in parameters)
            {
                isql.SetParameter(0, obj);
                idx++;
            }
            return isql.List<TEntity>();
        }

        public virtual void Insert(TEntity entity)
        {
            entity.ObjectState = ObjectState.Added;
            SessaoAtual.Save(entity);
            SessaoAtual.Flush();
            //SessaoAtual.syn.SyncObjectState(entity);
        }

        public virtual void InsertRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Insert(entity);
            }
        }

        public virtual void Update(TEntity entity)
        {
            entity.ObjectState = ObjectState.Modified;
            SessaoAtual.SaveOrUpdate(entity);
            SessaoAtual.Flush();
            //_context.SyncObjectState(entity);
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Update(entity);
            }
        }

        public virtual void Delete(object id)
        {
            var entity = SessaoAtual.Get<TEntity>(id);
            Delete(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            entity.ObjectState = ObjectState.Deleted;
            SessaoAtual.Delete(entity);
            SessaoAtual.Flush();
            //_context.SyncObjectState(entity);
        }
    }
}