using Repository.Interfaces;
using System;
using System.Linq;
using Domain.Core;
using System.Linq.Expressions;
using Repository.Core;
using ServiceStack.OrmLite;
using System.Collections.Generic;

namespace Repository.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        #region Fields

        private readonly IDbConnectionProvider _provider;

        #endregion
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerRepository" /> class.
        /// </summary>
        /// <param name="provider">The provider.</param>      
        public CustomerRepository(IDbConnectionProvider provider)
        {
            this._provider = provider;
            if (this._provider == null)
                throw new ArgumentNullException("CustomerRepository.IDbConnectionProvider");
        }
        #endregion
        public void Add(Customer entity)
        {
            _provider.Perform(con => con.Save(entity, true));
        }

        public void Add(Customer entity, ActivityLog actionLog)
        {
            _provider.Perform(con =>
            {
                using (var tran = con.OpenTransaction())
                {
                    //add customer.
                    con.Save(entity,true);
                    //add action log.
                    con.Save(actionLog);
                    tran.Commit();
                }
            });

        }

        public void Delete(Expression<Func<Customer, bool>> predicate)
        {
            _provider.Perform(con => con.Delete(predicate));
        }

        public void Delete(Customer entity)
        {
            _provider.Perform(con => con.Delete(entity));
        }

        public bool Exists(Expression<Func<Customer, bool>> predicate)
        {
            return _provider.Perform(con => con.Exists(predicate));
        }

        public IQueryable<Customer> Find(Expression<Func<Customer, bool>> predicate)
        {
            return _provider.Perform(con => con.Select(predicate)).AsQueryable();
        }

        public Customer Get(string id)
        {
            return _provider.Perform(con => con.Single<Customer>(q => q.Id == id));
        }

        public Customer GetById(string id, bool withAddresses = true)
        {
          return  _provider.Perform(con => {
              var customer = con.Single<Customer>(q => q.Id == id);
              if (customer != null && withAddresses)
                  customer.Addresses.AddRange(con.Select<CustomerAddress>(x => x.CustomerId == customer.Id));
              return customer;
              });
        }

        public void Update(Customer entity)
        {
            var ids = new List<string>();
            ids.AddRange(entity.Addresses.Select(x => x.Id));
            _provider.Perform(con =>
            {
                using (var tran = con.OpenTransaction())
                {
                    con.Delete<CustomerAddress>(x => x.CustomerId == entity.Id && !ids.Contains(x.Id));
                    con.UpdateAll(entity.Addresses);
                    con.Update(entity);
                    tran.Commit();
                }
            });
        }

        public void Update(Expression<Func<Customer>> updateFields, Expression<Func<Customer, bool>> predicate)
        {
            _provider.Perform(con => con.UpdateOnly(updateFields, predicate));
        }

        public void Update(Customer entity, ActivityLog actionLog)
        {
            var ids = new List<string>();
            ids.AddRange(entity.Addresses.Select(x => x.Id));
            _provider.Perform(con =>
            {
                using (var tran = con.OpenTransaction())
                {
                    con.SaveAll(entity.Addresses);
                    con.Delete<CustomerAddress>(x => x.CustomerId == entity.Id && !ids.Contains(x.Id));
                    //update customer.
                    con.Update(entity);
                    //add action log.
                    con.Save(actionLog);
                    tran.Commit();
                }
            });
        }
    }
}
