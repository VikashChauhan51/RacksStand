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
    [RoutePrefix("api/RackBox")]
    public class RackBoxController : ApiController
    {
        #region Fields

        private readonly IRackService _rackService;
        #endregion
        #region Ctor
        public RackBoxController(IRackService rackService)
        {
            if (rackService == null)
                throw new ArgumentNullException("RackBoxController.rackService");

            this._rackService = rackService;

        }
        #endregion
        #region Action
        [Route("Get")]
        [HttpPost]
        public async Task<HttpResponseMessage> Get(RackBoxSearchFilter filter)
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

        [Route("Update")]
        [HttpPost]
        public async Task<HttpResponseMessage> Update(RackBoxModel item)
        {
            if (ModelState.IsValid)
            {
                await Task.Run(() => { this._rackService.Update(item); });
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(true, "Success", null));
            }

            return Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(false, MessageString.INVALID_REQUEST_PARMS, null));
        }

        #endregion

    }
}