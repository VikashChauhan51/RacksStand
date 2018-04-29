using Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RacksStand.WebAPI.Controllers
{
    [RoutePrefix("api/Dashboard")]
    public class DashboardController : ApiController
    {
        [Route("GetData")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetData()
        {
            var response = Request.CreateResponse(HttpStatusCode.OK, new ResponseMessage<object>(true, "Succeeded", "Dashboard info"));
            return await Task.Run(() => response);
        }
    }
}
