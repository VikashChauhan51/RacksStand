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
    public class TaxRepository : ITaxRepository
    {
        #region Fields

        private readonly IDbConnectionProvider _provider;

        #endregion
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxRepository" /> class.
        /// </summary>
        /// <param name="provider">The provider.</param>      
        public TaxRepository(IDbConnectionProvider provider)
        {
            this._provider = provider;
            if (this._provider == null)
                throw new ArgumentNullException("TaxRepository.IDbConnectionProvider");
        }
        #endregion
        public void Add(Tax entity)
        {
            _provider.Perform(con => con.Save(entity, true));
        }

        public void Delete(string id)
        {
            _provider.Perform(con => con.DeleteById<Tax>(id));
        }

        public void Delete(Expression<Func<Tax, bool>> predicate)
        {
            _provider.Perform(con => con.Delete(predicate));
        }

        public void Delete(Tax entity)
        {
            _provider.Perform(con => con.Delete(entity));
        }

        public bool Exists(Expression<Func<Tax, bool>> predicate)
        {
            return _provider.Perform(con => con.Exists(predicate));
        }

        public IQueryable<Tax> Find(Expression<Func<Tax, bool>> predicate)
        {
            return _provider.Perform(con => con.Select(predicate)).AsQueryable();
        }

        public Tax Get(string id)
        {
            return _provider.Perform(con => con.Single<Tax>(q => q.Id == id));
        }

        public void Update(Tax entity)
        {
            _provider.Perform(con => con.Update(entity));
        }

        public void Update(Expression<Func<Tax>> updateFields, Expression<Func<Tax, bool>> predicate)
        {
            _provider.Perform(con => con.UpdateOnly(updateFields, predicate));
        }
    }
}
