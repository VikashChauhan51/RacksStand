using Domain.Core;
using Domain.DTO;
using Models.Account;


namespace Services.Interfaces
{
   public interface IAccountService
    {
        /// <summary>
        /// Login user with email address and password. The user password is case sanstive.
        /// </summary>
        /// <param name="item">object of <see cref="LoginModel"/> class. </param>
        /// <returns>(<see cref="LoginResult"/>) object.</returns>
        LoginResult Login(LoginModel item);
        /// <summary>
        /// Get user by email.
        /// </summary>
        /// <param name="email">(string) user register email address.</param>
        /// <returns>(<see cref="LoginResult"/>) response type.</returns>
        LoginResult GetByEmail(string email);
     
        /// <summary>
        /// Reset user password.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="userId"></param>
        void ResetPassword(string password, string userId);
        /// <summary>
        /// Get user by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        User GetById(string id);

    }
}
