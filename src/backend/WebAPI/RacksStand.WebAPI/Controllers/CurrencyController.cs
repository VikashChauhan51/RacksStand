using Domain.Core;
using Models.Core;
using Models.Filter;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RacksStand.WebAPI.Controllers
{
    [RoutePrefix("api/Currency")]
    public class CurrencyController : ApiController
    {
        #region Fields

        private readonly ICurrencyService _currencyService;
        #endregion
        #region Ctor
        public CurrencyController(ICurrencyService currencyService)
        {
            if (currencyService == null)
                throw new ArgumentNullException("CurrencyController.ICurrencyService");

            this._currencyService = currencyService;

        }
        #endregion
        #region Action
        [Route("Gets")]
        [HttpPost]
        public async Task<HttpResponseMessage> Get(CurrencySearchFilter filter)
        {
            HttpResponseMessage response = null;
            if (filter != null)
            {
                var session = (Session)ContextOperator.Get(ContextKeys.SESSION_ID);
                filter.CompanyId = session.CompanyId;
                var collection = this._currencyService.Get(filter);
                response = Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(true, "Succeeded", collection));
              
            }
            else
            response = Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(false, MessageString.INVALID_REQUEST_PARMS, null));
            return await Task.Run(() => response);
        }
        [Route("Get")]
        [HttpGet]
        public async Task<HttpResponseMessage> Get(string id)
        {
            HttpResponseMessage response = null;
            if (!string.IsNullOrEmpty(id))
            {
                var customer = this._currencyService.GetById(id);
                response = Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(true, "Succeeded", customer));
            }
            else
                response = Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(false, MessageString.INVALID_REQUEST_PARMS, null));
            return await Task.Run(() => response);

        }
        [Route("Add")]
        [HttpPost]
        public async Task<HttpResponseMessage> Add(CurrencyModel item)
        {
            if (ModelState.IsValid)
            {
                var session = (Session)ContextOperator.Get(ContextKeys.SESSION_ID);
                if (session == null)
                    return await Task.Run(() => Request.CreateResponse(HttpStatusCode.Unauthorized, new ResponseMessage<object>(false, MessageString.INVALID_REQUEST_PARMS, null)));

                item.CompanyId = session.CompanyId;
                this._currencyService.Add(item);
                return await Task.Run(() => Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(true, "Success", null)));
            }

            return await Task.Run(() => Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(false, MessageString.INVALID_REQUEST_PARMS, null)));
        }
        [Route("Update")]
        [HttpPost]
        public async Task<HttpResponseMessage> Update(CurrencyModel item)
        {
            if (ModelState.IsValid)
            {
                var session = (Session)ContextOperator.Get(ContextKeys.SESSION_ID);
                if (session == null)
                    return await Task.Run(() => Request.CreateResponse(HttpStatusCode.Unauthorized, new ResponseMessage<object>(false, MessageString.INVALID_REQUEST_PARMS, null)));

                item.CompanyId = session.CompanyId;
                this._currencyService.Update(item);
                return await Task.Run(() => Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(true, "Success", null)));
            }

            return await Task.Run(() => Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(false, MessageString.INVALID_REQUEST_PARMS, null)));
        }
        [Route("Delete")]
        [HttpGet]
        public async Task<HttpResponseMessage> Delete(string id)
        {
            HttpResponseMessage response = null;
            if (!string.IsNullOrEmpty(id))
            {
                this._currencyService.Delete(id);
                response = Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(true, "Succeeded", null));
            }
            else
                response = Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(false, MessageString.INVALID_REQUEST_PARMS, null));
            return await Task.Run(() => response);

        }
        #endregion
    }
}
