using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Models.Account;
using System.Threading.Tasks;
using Models.Core;
using Services.Interfaces;
using Domain.Core;
using Utility.Security;
using Enums.Core;
using EmailFactory;
using Utility.Miscellaneous;

namespace RacksStand.WebAPI.Controllers
{

    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        #region Fields

        private readonly IAccountService _accountService;
        private readonly ISessionService _sessionService;
        private readonly ILoginHistoryService _loginHistoryService;
        private readonly IEmailServerSettingService _emailServerSettingService;
        private readonly IActionLogService _actionLogService;
        private readonly ITokenService _tokenService;

        #endregion
        #region Ctor
        public AccountController(IAccountService accountService, ISessionService sessionService, ILoginHistoryService loginHistoryService, IEmailServerSettingService emailServerSettingService,
           IActionLogService actionLogService, ITokenService tokenService)
        {
            if (accountService == null)
                throw new ArgumentNullException("AccountController.accountService");
            if (sessionService == null)
                throw new ArgumentNullException("AccountController.sessionService");
            if (loginHistoryService == null)
                throw new ArgumentNullException("AccountController.loginHistoryService");
            if (emailServerSettingService == null)
                throw new ArgumentNullException("AccountController.emailServerSettingService");
            if (actionLogService == null)
                throw new ArgumentNullException("AccountController.actionLogService");
            if (tokenService == null)
                throw new ArgumentNullException("AccountController.tokenService");

            this._accountService = accountService;
            this._sessionService = sessionService;
            this._loginHistoryService = loginHistoryService;
            this._emailServerSettingService = emailServerSettingService;
            this._actionLogService = actionLogService;
            this._tokenService = tokenService;
        }
        #endregion
        #region Action
        [Route("Login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<HttpResponseMessage> Login(LoginModel item)
        {
            if (ModelState.IsValid)
            {
                item.Password = EncryptionDecryption.EncryptString(item.Password, item.Email, AppSettingManager.Salt);
                var loginInfo = this._accountService.Login(item);
                if (loginInfo != null)
                {
                    var session = new Session
                    {
                        Id = Guid.NewGuid().ToString("N"),
                        UserId = loginInfo.Id,
                        CompanyId = loginInfo.CompanyId,
                        CreatedOn = DateTime.UtcNow
                    };
                    this._sessionService.Add(session);

                    //hash userId, companyId and sessionId
                    loginInfo.Id = EncryptionDecryption.EncryptString(loginInfo.Id, AppSettingManager.Password, AppSettingManager.Salt);
                    loginInfo.CompanyId = EncryptionDecryption.EncryptString(loginInfo.CompanyId, AppSettingManager.Password, AppSettingManager.Salt);
                    loginInfo.SessionId = EncryptionDecryption.EncryptString(session.Id, AppSettingManager.Password, AppSettingManager.Salt);


                    var requestInfo = ContextOperator.Get(ContextKeys.REQUEST_INFO_KEY) as Request;
                    var loginHistory = new LoginHistory
                    {
                        Id = Guid.NewGuid().ToString("N"),
                        UserId = loginInfo.Id,
                        CreatedOn = DateTime.UtcNow,
                        Platform = requestInfo?.Platform,
                        Browser = requestInfo?.Browser,
                        HostAddress = requestInfo?.HostAddress,
                        HostName = requestInfo?.HostName,
                        IsMobileDevice = requestInfo?.IsMobileDevice,
                        Version = requestInfo?.Version,
                        URI = requestInfo?.URI
                    };
                    this._loginHistoryService.AddLog(loginHistory);

                    var response = Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(true, MessageString.LOGGEDIN, loginInfo));

                    return await Task.Run(() => response);
                }
            }

            return await Task.Run(() => Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(false, MessageString.INVALID_REQUEST_PARMS, null)));
        }
        [Route("Forgot")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<HttpResponseMessage> Forgot(string email)
        {

            if (!string.IsNullOrEmpty(email) && email.EmailPatternIsValid())
            {

                //check whether user exists or not.
                var user = this._accountService.GetByEmail(email);
                if (user != null)
                {
                    //create forgot password token.
                    var tokenId = this._tokenService.Add(user.Id);
                    //get email setting to send email to user.
                    var emailSetting = this._emailServerSettingService.GetServerSetting();
                    if (emailSetting != null)
                    {
                        //hash token value in url.  
                        string url = $"{AppSettingManager.BaseURL}resetPassword?token={tokenId}";
                        var credentials = new Credentials
                        {
                            UserName = emailSetting.UserName,
                            Password = emailSetting.Password,
                            Host = emailSetting.Host,
                            Port = emailSetting.Port,
                            EnableSsl = emailSetting.EnableSsl,
                            IsBodyHtml = emailSetting.IsBodyHtml
                        };
                        //async sent email.No need wait for response.
                        Task.Run(() => Email.TrySendEmail(credentials, emailSetting.UserName, MessageString.FORGOT_PASSWORD_EMAIL_SUBJECT, EmailTemplates.GetForgetPasswordMessage(user.FirstName, user.LastName, url), new List<string>() { user.Email }, null, null, null));
                    }
                    return await Task.Run(() => Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(true, MessageString.FORGOT_PASSWORD, null)));
                }
                //user not exists.
                return await Task.Run(() => Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(false, MessageString.EMAIL_NOT_EXISTS, null)));
            }
            //invalid request.
            return await Task.Run(() => Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(false, MessageString.INVALID_REQUEST_PARMS, null)));
        }

        [Route("Reset")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<HttpResponseMessage> Reset(string token, string password)
        {
            if (!string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(password))
            {

                //validate token.
                Tuple<bool, string> result = this._tokenService.IsValid(token);
                if (result != null)
                {
                    if (result.Item1 && !string.IsNullOrEmpty(result.Item2))
                    {
                        //check user exist or not.
                        var user = this._accountService.GetById(result.Item2);
                        if (user != null)
                        {
                            //encrypte the password.
                            var encryptedPassword = EncryptionDecryption.EncryptString(password, user.Email, AppSettingManager.Salt);
                            this._accountService.ResetPassword(encryptedPassword, user.Id);
                            //update async token
                            Task.Run(() => this._tokenService.Deactivate(token));
                            //log change password activity.
                            var requestInfo = ContextOperator.Get(ContextKeys.REQUEST_INFO_KEY) as Request;
                            var actionLog = new ActivityLog
                            {
                                Id = Guid.NewGuid().ToString("N"),
                                ActivityType = (byte)ActivityType.System,
                                TargetObjectId = user.Id,
                                CreatedBy = user.Id,
                                Message = ActionLogMessage.CHANGED_PASSWORD,
                                CreatedOn = DateTime.UtcNow,
                                Platform = requestInfo?.Platform,
                                Browser = requestInfo?.Browser,
                                HostAddress = requestInfo?.HostAddress,
                                HostName = requestInfo?.HostName,
                                IsMobileDevice = requestInfo?.IsMobileDevice,
                                Version = requestInfo?.Version,
                                URI = requestInfo?.URI
                            };
                            //add async action log.
                            Task.Run(() => this._actionLogService.Add(actionLog));

                            return await Task.Run(() => Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(true, MessageString.CHANGED_PASSWORD, null)));
                        }
                        return await Task.Run(() => Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(false, MessageString.USER_NOT_EXISTS, null)));
                    }
                }
                return await Task.Run(() => Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(false, MessageString.INVALID_TOKEN, null)));
            }
            return await Task.Run(() => Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(false, MessageString.INVALID_REQUEST_PARMS, null)));

        }
        [Route("Logout")]
        [HttpGet]
        public async Task<HttpResponseMessage> Logout()
        {
            var session = (Session)ContextOperator.Get(ContextKeys.SESSION_ID);
            if (session!=null && !string.IsNullOrEmpty(session.Id))
            {
                this._sessionService.Delete(session.Id);
                return await Task.Run(() => Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(true, "Logout", null)));
            }

            return await Task.Run(() => Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(false, MessageString.INVALID_REQUEST_PARMS, null)));
        }
        #endregion

    }
}
