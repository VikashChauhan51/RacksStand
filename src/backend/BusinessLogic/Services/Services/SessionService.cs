using Services.Interfaces;
using System;
using Domain.Core;
using Repository.Interfaces;

namespace Services.Services
{
    public class SessionService : ISessionService
    {
        #region Fields

        private readonly ISessionRepository _sessionRepository;

        #endregion
        #region Ctor
        public SessionService(ISessionRepository sessionRepository)
        {
            this._sessionRepository = sessionRepository;
            if (this._sessionRepository == null)
                throw new ArgumentNullException("SessionService.ISessionRepository");
        }
        #endregion

        public void Add(Session item)
        {
            if (item == null)
                throw new ArgumentNullException("Session");
            if (item.Id == null)
                throw new ArgumentNullException("Session.Id");
            if (item.UserId == null)
                throw new ArgumentNullException("Session.UserId");
            if (item.CompanyId == null)
                throw new ArgumentNullException("Session.CompanyId");
            //remove user old sessions.
            this._sessionRepository.Delete(x => x.UserId == item.UserId);
            //create user new session.
            this._sessionRepository.Add(item);

        }

        public void Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("id");

            this._sessionRepository.Delete(id);
        }

        public void Delete(Session item)
        {
            if (item == null)
                throw new ArgumentNullException("Session");
            if (item.Id == null)
                throw new ArgumentNullException("Session.Id");

            this._sessionRepository.Delete(item);
        }

        public void DeleteByUserId(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentNullException("userId");

            this._sessionRepository.Delete(x => x.UserId == userId);
        }

        public Session GetById(string sessionId)
        {
            if (string.IsNullOrEmpty(sessionId))
                throw new ArgumentNullException("userId");

            return this._sessionRepository.Get(sessionId);
        }
    }
}
