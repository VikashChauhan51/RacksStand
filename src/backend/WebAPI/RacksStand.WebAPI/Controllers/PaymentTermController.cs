using Domain.Core;
using Models.Core;
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
    [RoutePrefix("api/PaymentTerm")]
    public class PaymentTermController : ApiController
    {
        #region Fields

        private readonly IPaymentTermService _paymentTermService;
        #endregion
        #region Ctor
        public PaymentTermController(IPaymentTermService paymentTermService)
        {
            if (paymentTermService == null)
                throw new ArgumentNullException("PaymentTermController.IPaymentTermService");

            this._paymentTermService = paymentTermService;

        }
        #endregion
        #region Action
        [Route("Gets")]
        [HttpGet]
        public async Task<HttpResponseMessage> Get(string keyword, int start)
        {
            HttpResponseMessage response = null;
            var session = (Session)ContextOperator.Get(ContextKeys.SESSION_ID);
            var customerCollection = this._paymentTermService.Get(keyword, session.CompanyId, start);
            response = Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(true, "Succeeded", customerCollection));
            return await Task.Run(() => response);

        }
        [Route("Get")]
        [HttpGet]
        public async Task<HttpResponseMessage> Get(string id)
        {
            HttpResponseMessage response = null;
            if (!string.IsNullOrEmpty(id))
            {
                var customer = this._paymentTermService.GetById(id);
                response = Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(true, "Succeeded", customer));
            }
            else
                response = Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(false, MessageString.INVALID_REQUEST_PARMS, null));
            return await Task.Run(() => response);

        }
        [Route("Add")]
        [HttpPost]
        public async Task<HttpResponseMessage> Add(PaymentTermModel item)
        {
            if (ModelState.IsValid)
            {
                var session = (Session)ContextOperator.Get(ContextKeys.SESSION_ID);
                var requestInfo = ContextOperator.Get(ContextKeys.REQUEST_INFO_KEY) as Request;

                this._paymentTermService.Add(item, requestInfo, session);
                return await Task.Run(() => Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(true, "Success", null)));
            }

            return await Task.Run(() => Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(false, MessageString.INVALID_REQUEST_PARMS, null)));
        }
        [Route("Update")]
        [HttpPost]
        public async Task<HttpResponseMessage> Update(PaymentTermModel item)
        {
            if (ModelState.IsValid)
            {
                var session = (Session)ContextOperator.Get(ContextKeys.SESSION_ID);
                var requestInfo = ContextOperator.Get(ContextKeys.REQUEST_INFO_KEY) as Request;

                this._paymentTermService.Update(item, requestInfo, session);
                return await Task.Run(() => Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(true, "Success", null)));
            }

            return await Task.Run(() => Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(false, MessageString.INVALID_REQUEST_PARMS, null)));
        }
        #endregion
    }
}
