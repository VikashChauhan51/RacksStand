using Domain.Core;
using Enums.Core;
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
using Utility.Miscellaneous;

namespace RacksStand.WebAPI.Controllers
{
    [RoutePrefix("api/Customer")]
    public class CustomerController : ApiController
    {
        #region Fields

        private readonly ICustomerService _customerService;
        #endregion
        #region Ctor
        public CustomerController(ICustomerService customerService)
        {
            if (customerService == null)
                throw new ArgumentNullException("CustomerController.customerService");

            this._customerService = customerService;

        }
        #endregion
        #region Action
        [Route("Get")]
        [HttpPost]
        public async Task<HttpResponseMessage> Get(CustomerSearchFilter filter)
        {
            if (filter != null)
            {
                var session = (Session)ContextOperator.Get(ContextKeys.SESSION_ID);
                filter.CompanyId = session.CompanyId;
                var collection = await Task.Run(() => { return this._customerService.Get(filter); });
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(true, "Succeeded", collection));
            }
            return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(false, MessageString.INVALID_REQUEST_PARMS, null));

        }
        [Route("GetCustomer")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetCustomer(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var item = await Task.Run(() => { return this._customerService.GetById(id); });
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(true, "Succeeded", item));
            }
            return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(false, MessageString.INVALID_REQUEST_PARMS, null));

        }
        [Route("Add")]
        [HttpPost]
        public async Task<HttpResponseMessage> Add(CustomerModel item)
        {
            if (ModelState.IsValid)
            {
                var session = (Session)ContextOperator.Get(ContextKeys.SESSION_ID);
                item.CompanyId = session.CompanyId;
                item.CreatedBy = session.UserId;
                await Task.Run(() => { this._customerService.Add(item); });
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(true, "Success", null));
            }

            return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(false, MessageString.INVALID_REQUEST_PARMS, null));
        }
        [Route("Update")]
        [HttpPost]
        public async Task<HttpResponseMessage> Update(CustomerModel item)
        {
            if (ModelState.IsValid)
            {
                var session = (Session)ContextOperator.Get(ContextKeys.SESSION_ID);
                item.CompanyId = session.CompanyId;
                item.UpdatedBy = session.UserId;
                await Task.Run(() => { this._customerService.Update(item); });
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(true, "Success", null));
            }

            return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(false, MessageString.INVALID_REQUEST_PARMS, null));
        }
        [Route("Delete")]
        [HttpGet]
        public async Task<HttpResponseMessage> Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                await Task.Run(() => { this._customerService.Delete(id); });
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(true, "Succeeded", null));
            }
            return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(false, MessageString.INVALID_REQUEST_PARMS, null));
        }
        #endregion
    }
}
