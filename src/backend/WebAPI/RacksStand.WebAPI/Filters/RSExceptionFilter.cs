using Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
namespace RacksStand.WebAPI.Filters
{
    public class RSExceptionFilterAttribute : ExceptionFilterAttribute
    {
        // Create a logger for use in this class
        log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override void OnException(HttpActionExecutedContext context)
        {
            Exception ex = context.Exception;
            while (ex!=null)
            {
                logger.Error(ex);
                ex = ex.InnerException;
            }

            context.Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, new ResponseMessage<object>(false, "Exception", context.Exception.Message));
           


        }
    }
}