using System;
using Domain.Core;
using Services.Interfaces;
using Repository.Interfaces;
using Enums.Core;

namespace Services.Services
{
    public class EmailServerSettingService : IEmailServerSettingService
    {
        #region Fields

        private readonly IEmailServerRepository _emailServerRepository;

        #endregion
        #region Ctor
        public EmailServerSettingService(IEmailServerRepository emailServerRepository)
        {
            this._emailServerRepository = emailServerRepository;
            if (this._emailServerRepository == null)
                throw new ArgumentNullException("EmailServerSettingService.IEmailServerRepository");
        }
        #endregion  

        public EmailSetting GetServerSetting()
        {
            return this._emailServerRepository.GetSingle(x => x.Status == (byte)StatusType.Active);
        }
    }
}
