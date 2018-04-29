using RacksStand.WebAPI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace RacksStand.WebAPI
{
    public static class FilterConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new RSExceptionFilterAttribute());
            config.Filters.Add(new RSAuthorizationFilterAttribute());
        }
    }
}