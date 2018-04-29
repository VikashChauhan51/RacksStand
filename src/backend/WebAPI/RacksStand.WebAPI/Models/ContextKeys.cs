using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RacksStand.WebAPI
{
    public class ContextKeys
    {
        /// <summary>
        /// Session id key for store session data with key name.
        /// </summary>
        public const string SESSION_ID = "RS_Session_Id";
        /// <summary>
        /// Session key to store session object.
        /// </summary>
        public const string SESSION_KEY = "RS_Session_Key";
        /// <summary>
        /// Header session key name.
        /// </summary>
        public const string AUTHORIZATION_KEY = "Authorization";
        /// <summary>
        /// Request information key to store request basic information. 
        /// </summary>
        public const string REQUEST_INFO_KEY = "RS_Request_Info";
        /// <summary>
        /// Remote endpoint message property key.
        /// </summary>
        public const string REMOTE_ENDPOINT_MESSAGE_KEY = "System.ServiceModel.Channels.RemoteEndpointMessageProperty";

    }
}