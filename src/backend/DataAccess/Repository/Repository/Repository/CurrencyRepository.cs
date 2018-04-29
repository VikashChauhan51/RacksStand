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
    public class CurrencyRepository : ICurrencyRepository
    {
        #region Fields

        private readonly IDbConnectionProvider _provider;

        #endregion
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyRepository" /> class.
        /// </summary>
        /// <param name="provider">The provider.</param>      
        public CurrencyRepository(IDbConnectionProvider provider)
        {
            this._provider = provider;
            if (this._provider == null)
                throw new ArgumentNullException("CurrencyRepository.IDbConnectionProvider");
        }
        #endregion
        public void Add(Currency entity)
        {
            _provider.Perform(con => con.Save(entity, true));
        }

        public void Delete(string id)
        {
            _provider.Perform(con =>con.DeleteById<Currency>(id));
        }

        public void Delete(Expression<Func<Currency, bool>> predicate)
        {
            _provider.Perform(con => con.Delete(predicate));
        }

        public void Delete(Currency entity)
        {
            _provider.Perform(con => con.Delete(entity));
        }

        public bool Exists(Expression<Func<Currency, bool>> predicate)
        {
            return _provider.Perform(con => con.Exists(predicate));
        }

        public IQueryable<Currency> Find(Expression<Func<Currency, bool>> predicate)
        {
            return _provider.Perform(con => con.Select(predicate)).AsQueryable();
        }

        public Currency Get(string id)
        {
            return _provider.Perform(con => con.Single<Currency>(q => q.Id == id));
        }

        public void Update(Currency entity)
        {
            _provider.Perform(con => con.Update(entity));
        }

        public void Update(Expression<Func<Currency>> updateFields, Expression<Func<Currency, bool>> predicate)
        {
            _provider.Perform(con => con.UpdateOnly(updateFields, predicate));
        }
    }
}
