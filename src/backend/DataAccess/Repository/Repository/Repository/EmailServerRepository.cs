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
    public class EmailServerRepository : IEmailServerRepository
    {

        #region Fields

        private readonly IDbConnectionProvider _provider;

        #endregion
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailServerRepository" /> class.
        /// </summary>
        /// <param name="provider">The provider.</param>      
        public EmailServerRepository(IDbConnectionProvider provider)
        {
            this._provider = provider;
            if (this._provider == null)
                throw new ArgumentNullException("EmailServerRepository.IDbConnectionProvider");
        }
        #endregion

        public void Add(EmailSetting entity)
        {
            _provider.Perform(con => con.Save(entity, true));
        }

        public void Delete(Expression<Func<EmailSetting, bool>> predicate)
        {
            _provider.Perform(con => con.Delete(predicate));
        }

        public void Delete(EmailSetting entity)
        {
            _provider.Perform(con => con.Delete(entity));
        }

        public bool Exists(Expression<Func<EmailSetting, bool>> predicate)
        {
            return _provider.Perform(con => con.Exists(predicate));
        }

        public IQueryable<EmailSetting> Find(Expression<Func<EmailSetting, bool>> predicate)
        {
            return _provider.Perform(con => con.Select(predicate)).AsQueryable();
        }

        public EmailSetting Get(byte id)
        {
            return _provider.Perform(con => con.Single<EmailSetting>(q => q.Id == id));
        }

        public EmailSetting Get(string id)
        {
            throw new NotImplementedException();
           
        }

        public EmailSetting GetSingle(Expression<Func<EmailSetting, bool>> predicate)
        {
            return _provider.Perform(con => con.Single(predicate));
        }

        public void Update(EmailSetting entity)
        {
            _provider.Perform(con => con.Update(entity));
        }

        public void Update(Expression<Func<EmailSetting>> updateFields, Expression<Func<EmailSetting, bool>> predicate)
        {
            _provider.Perform(con => con.UpdateOnly(updateFields, predicate));
        }
    }
}
