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
  public  class LoginHistoryRepository : ILoginHistoryRepository
    {
        #region Fields

        private readonly IDbConnectionProvider _provider;

        #endregion
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginHistoryRepository" /> class.
        /// </summary>
        /// <param name="provider">The provider.</param>      
        public LoginHistoryRepository(IDbConnectionProvider provider)
        {
            this._provider = provider;
            if (this._provider == null)
                throw new ArgumentNullException("LoginHistoryRepository.IDbConnectionProvider");
        }
        #endregion
        public  void Add(LoginHistory entity)
        {
            _provider.Perform(con => con.Save(entity, true));
        }

        public void Delete(Expression<Func<LoginHistory, bool>> predicate)
        {
            _provider.Perform(con => con.Delete(predicate));
        }

        public  void Delete(LoginHistory entity)
        {
            _provider.Perform(con => con.Delete(entity));
        }
 

        public bool Exists(Expression<Func<LoginHistory, bool>> predicate)
        {
            return  _provider.Perform(con => con.Exists(predicate));
        }

        public IQueryable<LoginHistory> Find(Expression<Func<LoginHistory, bool>> predicate)
        {
            return  _provider.Perform(con => con.Select(predicate)).AsQueryable();
        }

        public LoginHistory Get(string id)
        {
            return  _provider.Perform(con => con.Single<LoginHistory>(q => q.Id == id));
        }

        public  void Update(LoginHistory entity)
        {
            _provider.Perform(con => con.Update(entity));
        }

        public void Update(Expression<Func<LoginHistory>> updateFields, Expression<Func<LoginHistory, bool>> predicate)
        {
            _provider.Perform(con => con.UpdateOnly(updateFields, predicate));
        }
    }
}
