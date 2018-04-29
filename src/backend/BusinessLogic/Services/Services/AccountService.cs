using Services.Interfaces;
using System;
using Domain.Core;
using Models.Account;
using Repository.Interfaces;
using Enums.Core;
using Domain.DTO;

namespace Services.Services
{
    public class AccountService : IAccountService
    {
        #region Fields

        private readonly IUserRepository _userRepository;
        #endregion
        public AccountService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;

            if (this._userRepository == null)
                throw new ArgumentNullException("AccountService.IUserRepository");
 
        }

        public LoginResult GetByEmail(string email)
        {

            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException("GetByEmail.email");
         
            return this._userRepository.GetSingle(x => x.Email== email && (x.Status==(byte)StatusType.New || x.Status == (byte)StatusType.Active
            || x.Status == (byte)StatusType.InVerification
            ));
 


        }

        public LoginResult Login(LoginModel item)
        {
            return   this._userRepository.Login(x => x.Email == item.Email && x.Password == item.Password);
        }

     

        public void ResetPassword(string password, string userId)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException("ResetPassword.password");
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentNullException("ResetPassword.userId");

            this._userRepository.ChangePassword(password, DateTime.UtcNow, userId);
             
        }

        public User GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("GetById.id");

            return this._userRepository.Get(id);
        }
    }
}
