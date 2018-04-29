using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.OrmLite;
using Repository.Core;
using Repository.Interfaces;
using Domain.Core;
using System.Linq.Expressions;
using Domain.DTO;

namespace Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        #region Fields

        private readonly IDbConnectionProvider _provider;

        #endregion

        #region Ctor

        ///// <summary>
        ///// Initializes a new instance of the <see cref="UserRepository" /> class.
        ///// </summary>
        ///// <param name="provider">The provider.</param>      
        public UserRepository(IDbConnectionProvider provider)
        {
            this._provider = provider;
            if (this._provider == null)
                throw new ArgumentNullException("UserRepository.IDbConnectionProvider");
        }
        #endregion
        public void Add(User entity)
        {
            _provider.Perform(con => con.Save(entity, true));
        }

        public void ChangePassword(string password, DateTime UpdatedOn, string userId)
        {
            _provider.Perform(con => con.UpdateOnly(()=>new User { Password= password, UpdatedOn= UpdatedOn }, where: p => p.Id == userId));
        }

        public void Delete(Expression<Func<User, bool>> predicate)
        {
            _provider.Perform(con => con.Delete(predicate));
        }

        public void Delete(User entity)
        {
            _provider.Perform(con => con.Delete(entity));
        }


        public bool Exists(Expression<Func<User, bool>> predicate)
        {
            return _provider.Perform(con => con.Exists(predicate));
        }

        public IQueryable<User> Find(Expression<Func<User, bool>> predicate)
        {
            return _provider.Perform(con => con.Select(predicate)).AsQueryable();
        }

        public User Get(string id)
        {
            return _provider.Perform(con => con.Single<User>(q => q.Id == id));

        }

        public LoginResult GetSingle(Expression<Func<User, bool>> predicate)
        {
            return _provider.Perform(con => con.Single<LoginResult>(con.From<User>().Where(predicate)));
        }

        public UserInfo GetUserInfo(Expression<Func<User, bool>> predicate)
        {
            return _provider.Perform(con => con.Single<UserInfo>(con.From<User>().Where(predicate)));
        }

        public LoginResult Login(Expression<Func<User, bool>> predicate)
        {
            return _provider.Perform(con => con.Single<LoginResult>(con.From<User>().Where(predicate)));
            

        }

        public void Update(User entity)
        {
            _provider.Perform(con => con.Update(entity));
        }

        public void Update(Expression<Func<User>> updateFields, Expression<Func<User, bool>> predicate)
        {
            _provider.Perform(con => con.UpdateOnly(updateFields, predicate));
        }
    }
}
