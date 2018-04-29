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
    public class RackRepository : IRackRepository
    {
        #region Fields

        private readonly IDbConnectionProvider _provider;

        #endregion
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="RackRepository" /> class.
        /// </summary>
        /// <param name="provider">The provider.</param>      
        public RackRepository(IDbConnectionProvider provider)
        {
            this._provider = provider;
            if (this._provider == null)
                throw new ArgumentNullException("RackRepository.IDbConnectionProvider");
        }
        #endregion
        #region Public

        public void Add(Rack entity)
        {
            _provider.Perform(con => con.Save(entity, true));
        }
        public void Delete(Expression<Func<Rack, bool>> predicate)
        {
            _provider.Perform(con => con.Delete(predicate));
        }
        public void Delete(Rack entity)
        {
            _provider.Perform(con => con.Delete(entity));
        }
        public bool Exists(Expression<Func<Rack, bool>> predicate)
        {
            return _provider.Perform(con => con.Exists(predicate));
        }
        public IQueryable<RackBox> Find(Expression<Func<RackBox, bool>> predicate)
        {
            return _provider.Perform(con => con.Select(predicate)).AsQueryable();
        }
        public IQueryable<Rack> Find(Expression<Func<Rack, bool>> predicate)
        {
            return _provider.Perform(con => con.Select(predicate)).AsQueryable();
        }
        public Rack Get(string id)
        {
            return _provider.Perform(con => con.Single<Rack>(q => q.Id == id));
        }
        public Rack GetById(string id, bool withBoxes = true)
        {
            return _provider.Perform(con => {
                var item = con.Single<Rack>(q => q.Id == id);
                if (item != null && withBoxes)
                    item.Boxes.AddRange(con.Select<RackBox>(x => x.RackId == item.Id));
                return item;
            });
        }
        public void Update(Expression<Func<RackBox>> updateFields, Expression<Func<RackBox, bool>> predicate)
        {
            _provider.Perform(con => con.UpdateOnly(updateFields, predicate));
        }
        public void Update(Rack entity)
        {
            _provider.Perform(con => con.Update(entity));
        }
        public void Update(Expression<Func<Rack>> updateFields, Expression<Func<Rack, bool>> predicate)
        {
            _provider.Perform(con => con.UpdateOnly(updateFields, predicate));
        }
        #endregion
    }
}
