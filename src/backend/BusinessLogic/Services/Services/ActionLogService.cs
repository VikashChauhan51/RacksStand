using Services.Interfaces;
using System;
using Domain.Core;
using Repository.Interfaces;

namespace Services.Services
{
    public class ActionLogService : IActionLogService
    {
        #region Fields

        private readonly IActivityLogRepository _activityLogRepository;

        #endregion
        #region Ctor
        public ActionLogService(IActivityLogRepository activityLogRepository)
        {
            this._activityLogRepository = activityLogRepository;
            if (this._activityLogRepository == null)
                throw new ArgumentNullException("ActionLogService.IActivityLogRepository");
        }
        #endregion  
        public void Add(ActivityLog item)
        {
            if (item == null)
                throw new ArgumentNullException("ActivityLog");
            if (item.Id == null)
                throw new ArgumentNullException("ActivityLog.Id");
            if (item.Message == null)
                throw new ArgumentNullException("ActivityLog.Message");
            if (item.TargetObjectId == null)
                throw new ArgumentNullException("ActivityLog.TargetObjectId");
            if (item.CreatedBy == null)
                throw new ArgumentNullException("ActivityLog.CreatedBy");

            this._activityLogRepository.Add(item);
        }
    }
}
