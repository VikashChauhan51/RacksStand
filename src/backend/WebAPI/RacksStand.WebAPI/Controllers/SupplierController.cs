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
    [RoutePrefix("api/Supplier")]
    public class SupplierController : ApiController
    {
        #region Fields

        private readonly ISupplierService _supplierService;
        #endregion
        #region Ctor
        public SupplierController(ISupplierService supplierService)
        {
            if (supplierService == null)
                throw new ArgumentNullException("SupplierController.supplierService");

            this._supplierService = supplierService;

        }
        #endregion
        #region Action
        [Route("Get")]
        [HttpPost]
        public async Task<HttpResponseMessage> Get(SupplierSearchFilter filter)
        {
            HttpResponseMessage response = null;
            if (filter != null)
            {
                var session = (Session)ContextOperator.Get(ContextKeys.SESSION_ID);
                filter.CompanyId = session.CompanyId;
                var collection = this._supplierService.Get(filter);
                response = Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(true, "Succeeded", collection));
            }
            else
                response = Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(false, MessageString.INVALID_REQUEST_PARMS, null));
            return await Task.Run(() => response);

        }
        [Route("GetSupplier")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetSupplier(string id)
        {
            HttpResponseMessage response = null;
            if (!string.IsNullOrEmpty(id))
            {
                var supplier = this._supplierService.GetById(id);
                response = Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(true, "Succeeded", supplier));
            }
            else
                response = Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(false, MessageString.INVALID_REQUEST_PARMS, null));
            return await Task.Run(() => response);

        }
        [Route("Add")]
        [HttpPost]
        public async Task<HttpResponseMessage> Add(SupplierModel item)
        {
            if (ModelState.IsValid)
            {
                var session = (Session)ContextOperator.Get(ContextKeys.SESSION_ID);
                item.CompanyId = session.CompanyId;
                item.CreatedBy = session.UserId;
                this._supplierService.Add(item);
                return await Task.Run(() => Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(true, "Success", null)));
            }

            return await Task.Run(() => Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(false, MessageString.INVALID_REQUEST_PARMS, null)));
        }
        [Route("Update")]
        [HttpPost]
        public async Task<HttpResponseMessage> Update(SupplierModel item)
        {
            if (ModelState.IsValid)
            {
                var session = (Session)ContextOperator.Get(ContextKeys.SESSION_ID);
                item.CompanyId = session.CompanyId;
                item.UpdatedBy = session.UserId;
                this._supplierService.Update(item);
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
                this._supplierService.Delete(id);
                response = Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(true, "Succeeded", null));
            }
            else
                response = Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(false, MessageString.INVALID_REQUEST_PARMS, null));
            return await Task.Run(() => response);

        }
        #endregion
    }
}
