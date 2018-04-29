using Domain.Core;
using System;
using System.Linq.Expressions;

namespace Repository.Interfaces
{
    public interface IEmailServerRepository : IRepository<EmailSetting, string>
    {
        /// <summary>
        /// Get email server details based on predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        EmailSetting GetSingle(Expression<Func<EmailSetting, bool>> predicate);
        /// <summary>
        /// Get entity by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        EmailSetting Get(byte id);
    }
}
