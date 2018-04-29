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

    [RoutePrefix("api/Room")]
    public class RoomController : ApiController
    {
        #region Fields

        private readonly IRoomService _roomService;
        #endregion
        #region Ctor
        public RoomController(IRoomService roomService)
        {
            if (roomService == null)
                throw new ArgumentNullException("RoomController.roomService");

            this._roomService = roomService;

        }
        #endregion
        #region Action
        [Route("Get")]
        [HttpPost]
        public async Task<HttpResponseMessage> Get(RoomSearchFilter filter)
        {

            if (filter != null)
            {
                var session = (Session)ContextOperator.Get(ContextKeys.SESSION_ID);
                filter.CompanyId = session.CompanyId;
                filter.UserId = session.UserId;
                var collection = await Task.Run(() => { return this._roomService.Get(filter); });
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(true, "Succeeded", collection));
            }
            return await Task.Run(() => Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(false, MessageString.INVALID_REQUEST_PARMS, null)));

        }
        [Route("GetRoom")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetRoom(string id)
        {

            if (!string.IsNullOrEmpty(id))
            {
                var item = await Task.Run(() => { return this._roomService.GetById(id); });
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(true, "Succeeded", item));
            }

            return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(false, MessageString.INVALID_REQUEST_PARMS, null));

        }
        [Route("Add")]
        [HttpPost]
        public async Task<HttpResponseMessage> Add(RoomModel item)
        {
            if (ModelState.IsValid)
            {
                var session = (Session)ContextOperator.Get(ContextKeys.SESSION_ID);
                item.CreatedBy = session.UserId;
                item.CompanyId = session.CompanyId;
                await Task.Run(() => { this._roomService.AddDefault(item); });
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(true, "Success", null));
            }
            return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(false, MessageString.INVALID_REQUEST_PARMS, null));
        }
        [Route("Update")]
        [HttpPost]
        public async Task<HttpResponseMessage> Update(RoomModel item)
        {
            if (ModelState.IsValid)
            {
                var session = (Session)ContextOperator.Get(ContextKeys.SESSION_ID);
                item.UpdatedBy = session.UserId;
                await Task.Run(() => { this._roomService.Update(item); });
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
                await Task.Run(() => { this._roomService.Delete(id); });
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(true, "Succeeded", null));
            }

            return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(false, MessageString.INVALID_REQUEST_PARMS, null));

        }
        #endregion
    }
}
