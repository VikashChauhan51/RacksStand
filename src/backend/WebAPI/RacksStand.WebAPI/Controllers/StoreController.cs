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

    [RoutePrefix("api/Store")]
    public class StoreController : ApiController
    {
        #region Fields

        private readonly IStoreService _storeService;
        #endregion
        #region Ctor
        public StoreController(IStoreService storeService)
        {
            if (storeService == null)
                throw new ArgumentNullException("StoreController.storeService");

            this._storeService = storeService;

        }
        #endregion
        #region Action
        [Route("Get")]
        [HttpPost]
        public async Task<HttpResponseMessage> Get(StoreSearchFilter filter)
        {
            if (filter != null)
            {
                var session = (Session)ContextOperator.Get(ContextKeys.SESSION_ID);
                filter.CompanyId = session.CompanyId;
                filter.UserId = session.UserId;
                var collection = await Task.Run(() => { return this._storeService.Get(filter); });
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(true, "Succeeded", collection));
            }
            return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(false, MessageString.INVALID_REQUEST_PARMS, null));

        }
        [Route("GetStore")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetStore(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var item = await Task.Run(() => { return this._storeService.GetById(id); });
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(true, "Succeeded", item));
            }
            return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(false, MessageString.INVALID_REQUEST_PARMS, null));

        }
        [Route("Add")]
        [HttpPost]
        public async Task<HttpResponseMessage> Add(StoreModel item)
        {
            if (ModelState.IsValid)
            {
                var session = (Session)ContextOperator.Get(ContextKeys.SESSION_ID);
                item.CompanyId = session.CompanyId;
                item.CreatedBy = session.UserId;
                await Task.Run(() => { this._storeService.AddDefault(item); });
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(true, "Success", null));
            }

            return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(false, MessageString.INVALID_REQUEST_PARMS, null));
        }
        [Route("Update")]
        [HttpPost]
        public async Task<HttpResponseMessage> Update(StoreModel item)
        {
            if (ModelState.IsValid)
            {
                var session = (Session)ContextOperator.Get(ContextKeys.SESSION_ID);
                item.UpdatedBy = session.UserId;
                await Task.Run(() => { this._storeService.Update(item); });
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
                await Task.Run(() => { this._storeService.Delete(id); });
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(true, "Succeeded", null));
            }
            return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(false, MessageString.INVALID_REQUEST_PARMS, null));

        }
        #endregion
    }
}
