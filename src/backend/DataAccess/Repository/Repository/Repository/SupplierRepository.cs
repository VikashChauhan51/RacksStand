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
    public class SupplierRepository : ISupplierRepository
    {
        #region Fields

        private readonly IDbConnectionProvider _provider;

        #endregion
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="SupplierRepository" /> class.
        /// </summary>
        /// <param name="provider">The provider.</param>      
        public SupplierRepository(IDbConnectionProvider provider)
        {
            this._provider = provider;
            if (this._provider == null)
                throw new ArgumentNullException("SupplierRepository.IDbConnectionProvider");
        }
        #endregion
        public void Add(Supplier entity)
        {
            _provider.Perform(con => con.Save(entity, true));
        }

        public void Delete(Expression<Func<Supplier, bool>> predicate)
        {
            _provider.Perform(con => con.Delete(predicate));
        }

        public void Delete(Supplier entity)
        {
            _provider.Perform(con => con.Delete(entity));
        }

        public bool Exists(Expression<Func<Supplier, bool>> predicate)
        {
            return _provider.Perform(con => con.Exists(predicate));
        }

        public IQueryable<Supplier> Find(Expression<Func<Supplier, bool>> predicate)
        {
            return _provider.Perform(con => con.Select(predicate)).AsQueryable();
        }

        public Supplier Get(string id)
        {
            return _provider.Perform(con => con.Single<Supplier>(q => q.Id == id));
        }

        public Supplier GetById(string id, bool withAddresses = true)
        {
            return _provider.Perform(con => {
                var item = con.Single<Supplier>(q => q.Id == id);
                if (item != null && withAddresses)
                    item.Addresses.AddRange(con.Select<SupplierAddress>(x => x.SupplierId == item.Id));
                return item;
            });
        }

        public void Update(Supplier entity)
        {
            var ids = new List<string>();
            ids.AddRange(entity.Addresses.Select(x => x.Id));
            _provider.Perform(con =>
            {
                using (var tran = con.OpenTransaction())
                {
                    con.Delete<SupplierAddress>(x => x.SupplierId == entity.Id && !ids.Contains(x.Id));
                    con.UpdateAll(entity.Addresses);
                    con.Update(entity);
                    tran.Commit();
                }
            });
        }

        public void Update(Expression<Func<Supplier>> updateFields, Expression<Func<Supplier, bool>> predicate)
        {
            _provider.Perform(con => con.UpdateOnly(updateFields, predicate));
        }
    }
}
