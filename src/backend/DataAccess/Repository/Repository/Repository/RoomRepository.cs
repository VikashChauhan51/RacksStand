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
    public class RoomRepository : IRoomRepository
    {
        #region Fields

        private readonly IDbConnectionProvider _provider;

        #endregion
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="RoomRepository" /> class.
        /// </summary>
        /// <param name="provider">The provider.</param>      
        public RoomRepository(IDbConnectionProvider provider)
        {
            this._provider = provider;
            if (this._provider == null)
                throw new ArgumentNullException("RoomRepository.IDbConnectionProvider");
        }
        #endregion
        #region Public
        public void Add(Room entity)
        {
            _provider.Perform(con => {
                using (var tran = con.OpenTransaction())
                {
                    con.Save(entity);
                    SaveRacks(con, entity.Racks);
                    tran.Commit();
                }
            });
        }
        public void Delete(Expression<Func<Room, bool>> predicate)
        {
            _provider.Perform(con => con.Delete(predicate));
        }
        public void Delete(Room entity)
        {
            _provider.Perform(con => con.Delete(entity));
        }
        public bool Exists(Expression<Func<Room, bool>> predicate)
        {
            return _provider.Perform(con => con.Exists(predicate));
        }
        public IQueryable<Room> Find(Expression<Func<Room, bool>> predicate)
        {
            return _provider.Perform(con => con.Select(predicate)).AsQueryable();
        }
        public Room Get(string id)
        {
            return _provider.Perform(con => con.Single<Room>(q => q.Id == id));
        }
        public void Update(Room entity)
        {
            _provider.Perform(con => con.Update(entity));
        }
        public void Update(Expression<Func<Room>> updateFields, Expression<Func<Room, bool>> predicate)
        {
            _provider.Perform(con => con.UpdateOnly(updateFields, predicate));
        }
        #endregion
        #region Private

        private void SaveRacks(IDbConnection dbConn, List<Rack> racks)
        {
            foreach (var item in racks)
                dbConn.Save(item, true);
        }
        #endregion
    }
}
