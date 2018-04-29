using Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ISessionService
    {
        /// <summary>
        /// Add user session.It will remove current user all pervious sessions.
        /// </summary>
        /// <param name="item"></param>
        void Add(Session item);
        /// <summary>
        /// Get session by id.
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        Session GetById(string sessionId);
        /// <summary>
        /// Remove session by session object.
        /// </summary>
        /// <param name="item"></param>
        void Delete(Session item);
        /// <summary>
        /// Remove all session of user.
        /// </summary>
        /// <param name="userId"></param>
        void DeleteByUserId(string userId);
        /// <summary>
        /// Remove session by session id.
        /// </summary>
        /// <param name="id"></param>
        void Delete(string id);

    }
}
