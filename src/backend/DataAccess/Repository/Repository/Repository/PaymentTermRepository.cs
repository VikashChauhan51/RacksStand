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
    public class PaymentTermRepository : IPaymentTermRepository
    {
        #region Fields

        private readonly IDbConnectionProvider _provider;

        #endregion
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentTermRepository" /> class.
        /// </summary>
        /// <param name="provider">The provider.</param>      
        public PaymentTermRepository(IDbConnectionProvider provider)
        {
            this._provider = provider;
            if (this._provider == null)
                throw new ArgumentNullException("PaymentTermRepository.IDbConnectionProvider");
        }
        #endregion
        public void Add(PaymentTerm entity)
        {
            _provider.Perform(con => con.Save(entity, true));
        }

        public void Delete(Expression<Func<PaymentTerm, bool>> predicate)
        {
            _provider.Perform(con => con.Delete(predicate));
        }

        public void Delete(PaymentTerm entity)
        {
            _provider.Perform(con => con.Delete(entity));
        }

        public bool Exists(Expression<Func<PaymentTerm, bool>> predicate)
        {
            return _provider.Perform(con => con.Exists(predicate));
        }

        public IQueryable<PaymentTerm> Find(Expression<Func<PaymentTerm, bool>> predicate)
        {
            return _provider.Perform(con => con.Select(predicate)).AsQueryable();
        }

        public PaymentTerm Get(string id)
        {
            return _provider.Perform(con => con.Single<PaymentTerm>(q => q.Id == id));
        }

        public void Update(PaymentTerm entity)
        {
            _provider.Perform(con => con.Update(entity));
        }

        public void Update(Expression<Func<PaymentTerm>> updateFields, Expression<Func<PaymentTerm, bool>> predicate)
        {
            _provider.Perform(con => con.UpdateOnly(updateFields, predicate));
        }
    }
}
