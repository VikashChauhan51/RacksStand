using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Core
{
    /// <summary>
    /// Get the basic information from the http request.
    /// </summary>
    public sealed class Request
    {
        #region Fields
        private string _browser;
        private bool _isCrawler;
        private string _platform;
        private string _version;
        private bool _isMobileDevice;
        private string _hostName;
        private string _hostAddress;
        private string _uri;
        #endregion
        #region Ctor

        /// <summary>
        /// Initializes the instance of <see cref="Request"/> class.
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="platform"></param>
        /// <param name="version"></param>
        /// <param name="hostName"></param>
        /// <param name="hostAddress"></param>
        /// <param name="uri"></param>
        /// <param name="isCrawler"></param>
        /// <param name="isMobileDevice"></param>
        public Request(string browser, string platform, string version, string hostName, string hostAddress, string uri, bool isCrawler, bool isMobileDevice)
        {
            this._browser = browser;
            this._platform = platform;
            this._version = version;
            this._hostName = hostName;
            this._hostAddress = hostAddress;
            this._uri = uri;
            this._isCrawler = isCrawler;
            this._isMobileDevice = isMobileDevice;

        }
        #endregion
        #region Properties

        /// <summary>
        /// Get browser name.
        /// </summary>
        public string Browser { get { return _browser; } }
        /// <summary>
        /// Gets a value indicating whether the browser is a search engine Web crawler.
        /// </summary>
        public bool IsCrawler { get { return _isCrawler; } }
        /// <summary>
        /// Get the name of the operating system that the client is using, if known.
        /// </summary>
        public string Platform { get { return _platform; } }
        /// <summary>
        /// Get the full version number of the browser as a string.
        /// </summary>
        public string Version { get { return _version; } }
        /// <summary>
        /// Get a value that indicates whether the browser is a recognized mobile device.
        /// </summary>
        public bool IsMobileDevice { get { return _isMobileDevice; } }
        /// <summary>
        /// Get host device IPAddress.
        /// </summary>
        public string HostName { get { return _hostName; } }
        /// <summary>
        /// Get host device name.
        /// </summary>
        public string HostAddress { get { return _hostAddress; } }
        /// <summary>
        /// Gets the absolute URI.
        /// </summary>
        public string URI { get { return _uri; } }
        #endregion

    }
}
