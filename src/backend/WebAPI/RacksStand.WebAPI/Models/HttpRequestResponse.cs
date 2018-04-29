using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Net;
using Models.Core;

namespace RacksStand.WebAPI
{
    public static class HttpRequestResponse
    {
        /// <summary>
        /// Get header cookie.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="key"></param>
        /// <returns>(<see cref="CookieHeaderValue"/>) cookie object.</returns>
        public static CookieHeaderValue GetCookie(this HttpRequestMessage request, string key)
        {
            return request.Headers.GetCookies(key).SingleOrDefault();
        }

        /// <summary>
        /// Get user session id from http request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>(<see cref="string"/>) value.</returns>
        public static string GetSessionId(this HttpRequestMessage request)
        {
            var item = request.Headers.Where(x => x.Key == ContextKeys.AUTHORIZATION_KEY).Select(x => x.Value).SingleOrDefault();
            return item != null ? item.FirstOrDefault() : null;
        }

        /// <summary>
        /// Set http header cookie.
        /// </summary>
        /// <param name="header"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetCookie(this HttpResponseHeaders header, string key, string value)
        {
            var cooki = new CookieHeaderValue(key, value);
            cooki.Expires = DateTimeOffset.Now.AddDays(1);
            cooki.Path = "/";
            header.AddCookies(new CookieHeaderValue[] { cooki });
        }

        /// <summary>
        /// Get cookie value by cookie name/key.
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="name"></param>
        /// <returns>(<see cref="string"/>) value.</returns>
        public static string GetCookieValue(this CookieHeaderValue cookie, string name)
        {
            return cookie.Cookies.Where(x => x.Name == name).Select(x => x.Value).SingleOrDefault();
        }

        /// <summary>
        /// Get basic data from the http request context.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>(<see cref="Request"/>)request info object.</returns>
        public static Request GetRequestInfo(this HttpRequestMessage request)
        {


            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                var httpRequestBase = ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request;
                if (httpRequestBase == null)
                    return null;
                var browser = httpRequestBase.Browser;
                return new Request(browser.Browser, browser.Platform, browser.Version, Dns.GetHostEntry(httpRequestBase.ServerVariables["REMOTE_ADDR"]).HostName, httpRequestBase.UserHostAddress, httpRequestBase.Url.AbsoluteUri, browser.Crawler, browser.IsMobileDevice);
            }
            else if (request.Properties.ContainsKey(ContextKeys.REMOTE_ENDPOINT_MESSAGE_KEY))
            {
                var httpRequestBase = ((HttpContextWrapper)request.Properties[ContextKeys.REMOTE_ENDPOINT_MESSAGE_KEY]).Request;
                if (httpRequestBase == null)
                    return null;
                var browser = httpRequestBase.Browser;
                return new Request(browser.Browser, browser.Platform, browser.Version, Dns.GetHostEntry(httpRequestBase.ServerVariables["REMOTE_ADDR"]).HostName, httpRequestBase.UserHostAddress, httpRequestBase.Url.AbsoluteUri, browser.Crawler, browser.IsMobileDevice);
            }
            else if (HttpContext.Current != null)
            {
                var httpRequestBase = HttpContext.Current.Request;
                if (httpRequestBase == null)
                    return null;
                var browser = httpRequestBase.Browser;
                return new Request(browser.Browser, browser.Platform, browser.Version, Dns.GetHostEntry(httpRequestBase.ServerVariables["REMOTE_ADDR"]).HostName, httpRequestBase.UserHostAddress, httpRequestBase.Url.AbsoluteUri, browser.Crawler, browser.IsMobileDevice);
            }
            // return default null value.
            return null;
        }
    }
}