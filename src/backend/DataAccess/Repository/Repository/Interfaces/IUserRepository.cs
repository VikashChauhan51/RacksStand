using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core;
using System.Linq.Expressions;
using Domain.DTO;

namespace Repository.Interfaces
{
    public interface IUserRepository : IRepository<User, string> 
    {
        /// <summary>
        /// This is used to login the user into the application.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>(<see cref="LoginResult"/>) response.</returns>
        LoginResult Login(Expression<Func<User, bool>> predicate);
        /// <summary>
        /// Get the user some brief information.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>(<see cref="UserInfo"/>) response.</returns>
        UserInfo GetUserInfo(Expression<Func<User, bool>> predicate);
        /// <summary>
        /// Get user details based on predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>(<see cref="LoginResult"/>) response.</returns>
        LoginResult GetSingle(Expression<Func<User, bool>> predicate);
        /// <summary>
        ///  Reset user password.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="UpdatedOn"></param>
        /// <param name="userId"></param>
        void ChangePassword(string password,DateTime UpdatedOn, string userId);
    }
}
