using Repository.Interfaces;
using System;
using System.Linq;
using Domain.Core;
using System.Linq.Expressions;
using Repository.Core;
using ServiceStack.OrmLite;

namespace Repository.Repository
{
    public class AddressRepository : IAddressRepository
    {
        #region Fields

        private readonly IDbConnectionProvider _provider;

        #endregion
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressRepository" /> class.
        /// </summary>
        /// <param name="provider">The provider.</param>      
        public AddressRepository(IDbConnectionProvider provider)
        {
            this._provider = provider;
            if (this._provider == null)
                throw new ArgumentNullException("AddressRepository.IDbConnectionProvider");
        }
        #endregion
        public void Add(CustomerAddress entity)
        {
            _provider.Perform(con => con.Save(entity, true));
        }

        public void Delete(Expression<Func<CustomerAddress, bool>> predicate)
        {
            _provider.Perform(con => con.Delete(predicate));
        }

        public void Delete(CustomerAddress entity)
        {
            _provider.Perform(con => con.Delete(entity));
        }

        public bool Exists(Expression<Func<CustomerAddress, bool>> predicate)
        {
            return _provider.Perform(con => con.Exists(predicate));
        }

        public IQueryable<CustomerAddress> Find(Expression<Func<CustomerAddress, bool>> predicate)
        {
            return _provider.Perform(con => con.Select(predicate)).AsQueryable();
        }

        public CustomerAddress Get(string id)
        {
            return _provider.Perform(con => con.Single<CustomerAddress>(q => q.Id == id));
        }

        public void Update(CustomerAddress entity)
        {
            _provider.Perform(con => con.Update(entity));
        }

        public void Update(Expression<Func<CustomerAddress>> updateFields, Expression<Func<CustomerAddress, bool>> predicate)
        {
            _provider.Perform(con => con.UpdateOnly(updateFields, predicate));
        }
    }
}
