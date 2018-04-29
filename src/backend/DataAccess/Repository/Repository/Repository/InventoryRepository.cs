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
    public class InventoryRepository : IInventoryRepository
    {
        #region Fields

        private readonly IDbConnectionProvider _provider;

        #endregion
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryRepository" /> class.
        /// </summary>
        /// <param name="provider">The provider.</param>      
        public InventoryRepository(IDbConnectionProvider provider)
        {
            this._provider = provider;
            if (this._provider == null)
                throw new ArgumentNullException("InventoryRepository.IDbConnectionProvider");
        }
        #endregion
        public void Add(Inventory entity)
        {
            _provider.Perform(con => con.Save(entity, true));
        }

        public void Delete(string id)
        {
            _provider.Perform(con => con.DeleteById<Inventory>(id));
        }

        public void Delete(Expression<Func<Inventory, bool>> predicate)
        {
            _provider.Perform(con => con.Delete(predicate));
        }

        public void Delete(Inventory entity)
        {
            _provider.Perform(con => con.Delete(entity));
        }

        public bool Exists(Expression<Func<Inventory, bool>> predicate)
        {
            return _provider.Perform(con => con.Exists(predicate));
        }

        public IQueryable<Inventory> Find(Expression<Func<Inventory, bool>> predicate)
        {
            return _provider.Perform(con => con.Select(predicate)).AsQueryable();
        }

        public Inventory Get(string id)
        {
            return _provider.Perform(con => con.Single<Inventory>(q => q.Id == id));
        }

        public void Update(Inventory entity)
        {
            _provider.Perform(con => con.Update(entity));
        }

        public void Update(Expression<Func<Inventory>> updateFields, Expression<Func<Inventory, bool>> predicate)
        {
            _provider.Perform(con => con.UpdateOnly(updateFields, predicate));
        }
    }
}
