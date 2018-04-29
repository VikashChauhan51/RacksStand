using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core;
using System.Linq.Expressions;
using Repository.Core;
using ServiceStack.OrmLite;

namespace Repository.Repository
{
    public class SessionRepository : ISessionRepository
    {
        #region Fields

        private readonly IDbConnectionProvider _provider;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="SessionRepository" /> class.
        /// </summary>
        /// <param name="provider">The provider.</param>      
        public SessionRepository(IDbConnectionProvider provider)
        {
            this._provider = provider;
            if (this._provider == null)
                throw new ArgumentNullException("SessionRepository.IDbConnectionProvider");
        }
        #endregion

        public void Add(Session entity)
        {
            _provider.Perform(con => con.Save(entity, true));
        }

        public void Delete(Expression<Func<Session, bool>> predicate)
        {
            _provider.Perform(con => con.Delete(predicate));
        }

        public void Delete(string id)
        {
            _provider.Perform(con => con.DeleteById<Session>(id));
        }

        public void Delete(Session entity)
        {
            _provider.Perform(con => con.Delete(entity));
        }



        public bool Exists(Expression<Func<Session, bool>> predicate)
        {
            return _provider.Perform(con => con.Exists(predicate));
        }

        public IQueryable<Session> Find(Expression<Func<Session, bool>> predicate)
        {
            return _provider.Perform(con => con.Select(predicate)).AsQueryable();
        }

        public Session Get(string id)
        {
            return _provider.Perform(con => con.Single<Session>(q => q.Id == id));
        }

        public void Update(Session entity)
        {
            _provider.Perform(con => con.Update(entity));
        }

        public void Update(Expression<Func<Session>> updateFields, Expression<Func<Session, bool>> predicate)
        {
            _provider.Perform(con => con.UpdateOnly(updateFields, predicate));
        }
    }
}
