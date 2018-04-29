using Microsoft.Extensions.Configuration;
using Microsoft.Win32;

namespace Repository.Core
{
    /// <summary>
    /// Retrieves the sql connection string
    /// </summary>
    public class ConnectionStringProvider : IConnectionStringProvider
    {
        #region Fields

        private readonly string _connectionString;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionStringProvider"/> class.
        /// </summary>
        public ConnectionStringProvider()
        {
            var connection = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            if (connection == null) return;

            this._connectionString = connection;
        }

        #endregion

        #region IConnectionStringProvider

        /// <summary>
        /// Is there a valid connection string configured?
        /// </summary>
        public bool HasConnectionString => !string.IsNullOrEmpty(_connectionString);

        /// <summary>
        /// The connection string
        /// </summary>
        public string ConnectionString => _connectionString;

        #endregion

        
    }
}
