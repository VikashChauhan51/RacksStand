using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enums.Core
{
    public enum ResponseStatus
    {
        /// <summary>
        /// The request has been completed successfully.
        /// </summary>
        Succeed = 1,
        /// <summary>
        /// Failed to complete request.
        /// </summary>
        Failed = 2,
        /// <summary>
        /// Something went wrong.
        /// </summary>
        Error = 3,
        /// <summary>
        ///  The request not authenticated.
        /// </summary>
        Unauthenticated = 4,
        /// <summary>
        ///  The request user not authorized.
        /// </summary>
        Unauthorized = 5
    }
}
