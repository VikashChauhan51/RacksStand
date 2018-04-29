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

    [RoutePrefix("api/Rack")]
    public class RackController : ApiController
    {
        #region Fields

        private readonly IRackService _rackService;
        #endregion
        #region Ctor
        public RackController(IRackService rackService)
        {
            if (rackService == null)
                throw new ArgumentNullException("RackController.rackService");

            this._rackService = rackService;

        }
        #endregion
        #region Action
        [Route("Get")]
        [HttpPost]
        public async Task<HttpResponseMessage> Get(RackSearchFilter filter)
        {

            if (filter != null)
            {
                var session = (Session)ContextOperator.Get(ContextKeys.SESSION_ID);
                filter.CompanyId = session.CompanyId;
                filter.UserId = session.UserId;
                var collection = await Task.Run(() => { return this._rackService.Get(filter); });
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(true, "Succeeded", collection));
            }
            return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(false, MessageString.INVALID_REQUEST_PARMS, null));

        }
        [Route("GetRack")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetRack(string id)
        {

            if (!string.IsNullOrEmpty(id))
            {
                var item = await Task.Run(() => { return this._rackService.GetById(id); });
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(true, "Succeeded", item));
            }
            return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(false, MessageString.INVALID_REQUEST_PARMS, null));

        }
        [Route("Add")]
        [HttpPost]
        public async Task<HttpResponseMessage> Add(RackModel item)
        {
            if (ModelState.IsValid)
            {
                var session = (Session)ContextOperator.Get(ContextKeys.SESSION_ID);
                item.CreatedBy = session.UserId;
                item.CompanyId = session.CompanyId;
                await Task.Run(() => { this._rackService.Add(item); });
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(true, "Success", null));
            }
            return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(false, MessageString.INVALID_REQUEST_PARMS, null));
        }
        [Route("Update")]
        [HttpPost]
        public async Task<HttpResponseMessage> Update(RackModel item)
        {
            if (ModelState.IsValid)
            {
                var session = (Session)ContextOperator.Get(ContextKeys.SESSION_ID);
                item.UpdatedBy = session.UserId;
                await Task.Run(() => { this._rackService.Update(item); });
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
                await Task.Run(() => { this._rackService.Delete(id); });
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(true, "Succeeded", null));
            }
            return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(false, MessageString.INVALID_REQUEST_PARMS, null));

        }
        #endregion
    }
}
