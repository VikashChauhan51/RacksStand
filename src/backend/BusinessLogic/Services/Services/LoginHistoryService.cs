using Services.Interfaces;
using System;
using Domain.Core;
using Repository.Interfaces;

namespace Services.Services
{
    public class LoginHistoryService : ILoginHistoryService
    {
        #region Fields

        private readonly ILoginHistoryRepository _loginHistoryRepository;

        #endregion
        #region Ctor
        public LoginHistoryService(ILoginHistoryRepository loginHistoryRepository)
        {
            this._loginHistoryRepository = loginHistoryRepository;
            if (this._loginHistoryRepository == null)
                throw new ArgumentNullException("LoginHistoryService.ILoginHistoryRepository");
        }
        #endregion  


        public void AddLog(LoginHistory item)
        {
            if (item == null)
                throw new ArgumentNullException("LoginHistory");
            if (item.Id == null)
                throw new ArgumentNullException("LoginHistory.Id");
            if (item.UserId == null)
                throw new ArgumentNullException("LoginHistory.UserId");

              this._loginHistoryRepository.Add(item);
        }
         
    }
}
