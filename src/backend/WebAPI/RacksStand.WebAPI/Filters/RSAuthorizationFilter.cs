using Domain.Core;
using Models.Core;
using Services.Interfaces;
using System;
using System.Configuration;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Utility.Security;

namespace RacksStand.WebAPI.Filters
{

    public class RSAuthorizationFilterAttribute : IAuthorizationFilter, IFilter
    {
        #region Fields
        private ISessionService _sessionService;
        #endregion
        #region Properties
           
        public bool AllowMultiple
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        #endregion
        #region Public

        public async Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            //get request information from request.
            var requestInfo = actionContext.Request.GetRequestInfo();
            if (requestInfo != null)
                ContextOperator.Set(ContextKeys.REQUEST_INFO_KEY, requestInfo);

            if (SkipAuthorization(actionContext))
                return await continuation();

            //read session id from request header.
            var sessionId = actionContext.Request.GetSessionId();
            if (!string.IsNullOrEmpty(sessionId))
            {
                //Decrypt sessionId.
                sessionId = EncryptionDecryption.DecryptString(sessionId, AppSettingManager.Password, AppSettingManager.Salt);
                this._sessionService = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(ISessionService)) as ISessionService;

                if (this._sessionService == null)
                    throw new ArgumentException("RSAuthorizationFilterAttribute.ISessionService");

                var value =   this._sessionService.GetById(sessionId);

                if (value != null)
                {
                    ContextOperator.Set(ContextKeys.SESSION_ID, value);
                    return await continuation();
                }
            }


            return actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized, new ResponseMessage<object>(false, "Unauthorized", null));
        }
        #endregion

        #region Private
        private static bool SkipAuthorization(HttpActionContext actionContext)
        {
            Contract.Assert(actionContext != null);

            return actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
                       || actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
        }

        #endregion
    }
}
