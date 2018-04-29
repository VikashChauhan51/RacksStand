using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Core;
using System.Linq.Expressions;
using Repository.Core;
using ServiceStack.OrmLite;

namespace Repository.Repository
{
    public class ActivityLogRepository : IActivityLogRepository
    {
        #region Fields

        private readonly IDbConnectionProvider _provider;

        #endregion
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityLogRepository" /> class.
        /// </summary>
        /// <param name="provider">The provider.</param>      
        public ActivityLogRepository(IDbConnectionProvider provider)
        {
            this._provider = provider;
            if (this._provider == null)
                throw new ArgumentNullException("ActivityLogRepository.IDbConnectionProvider");
        }
        #endregion
        public void Add(ActivityLog entity)
        {
            _provider.Perform(con => con.Save(entity, true));
        }

        public void Delete(Expression<Func<ActivityLog, bool>> predicate)
        {
            _provider.Perform(con => con.Delete(predicate));
        }

        public void Delete(ActivityLog entity)
        {
            _provider.Perform(con => con.Delete(entity));
        }

        public bool Exists(Expression<Func<ActivityLog, bool>> predicate)
        {
            return _provider.Perform(con => con.Exists(predicate));
        }

        public IQueryable<ActivityLog> Find(Expression<Func<ActivityLog, bool>> predicate)
        {
            return _provider.Perform(con => con.Select(predicate)).AsQueryable();
        }

        public ActivityLog Get(string id)
        {
            return _provider.Perform(con => con.Single<ActivityLog>(q => q.Id == id));
        }

        public void Update(ActivityLog entity)
        {
            _provider.Perform(con => con.Update(entity));
        }

        public void Update(Expression<Func<ActivityLog>> updateFields, Expression<Func<ActivityLog, bool>> predicate)
        {
            _provider.Perform(con => con.UpdateOnly(updateFields, predicate));
        }
    }
}
