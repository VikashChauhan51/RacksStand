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
using System.Data;

namespace Repository.Repository
{
    public class StoreRepository : IStoreRepository
    {
        #region Fields

        private readonly IDbConnectionProvider _provider;

        #endregion
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="StoreRepository" /> class.
        /// </summary>
        /// <param name="provider">The provider.</param>      
        public StoreRepository(IDbConnectionProvider provider)
        {
            this._provider = provider;
            if (this._provider == null)
                throw new ArgumentNullException("StoreRepository.IDbConnectionProvider");
        }
        #endregion
        #region Public
        public void Add(Store entity)
        {
            _provider.Perform(con =>
            {
                using (var tran = con.OpenTransaction())
                {
                    con.Save(entity);
                    SaveRooms(con, entity.Rooms);
                    tran.Commit();
                }
            });
        }
        public void Delete(Store entity)
        {
            _provider.Perform(con => con.Delete(entity));
        }
        public void Delete(Expression<Func<Store, bool>> predicate)
        {
            _provider.Perform(con => con.Delete(predicate));
        }
        public bool Exists(Expression<Func<Store, bool>> predicate)
        {
            return _provider.Perform(con => con.Exists(predicate));
        }
        public IQueryable<Store> Find(Expression<Func<Store, bool>> predicate)
        {
            return _provider.Perform(con => con.Select(predicate)).AsQueryable();
        }
        public Store Get(string id)
        {
            return _provider.Perform(con => con.Single<Store>(q => q.Id == id));
        }
        public void Update(Store entity)
        {
            _provider.Perform(con => con.Update(entity));
        }
        public void Update(Expression<Func<Store>> updateFields, Expression<Func<Store, bool>> predicate)
        {
            _provider.Perform(con => con.UpdateOnly(updateFields, predicate));
        }
        #endregion
        #region Private
        private void SaveRooms(IDbConnection dbConn, List<Room> rooms)
        {
            foreach (var item in rooms)
            {
                dbConn.Save(item);
                SaveRacks(dbConn, item.Racks);
            }
        }
        private void SaveRacks(IDbConnection dbConn, List<Rack> racks)
        {
            foreach (var item in racks)
                dbConn.Save(item, true);
        }
        #endregion
    }
}
