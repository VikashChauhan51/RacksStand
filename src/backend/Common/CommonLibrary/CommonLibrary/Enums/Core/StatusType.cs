using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enums.Core
{
    /// <summary>
    /// This class contains types of status which used in the application.
    /// </summary>
    public enum StatusType : byte
    {
        /// <summary>
        ///  The entity in new state.
        /// </summary>
        New = 0,
        /// <summary>
        ///  The entity in active state.
        /// </summary>
        Active = 1,
        /// <summary>
        /// The entity in deleted state.
        /// </summary>
        Deleted = 2,
        /// <summary>
        /// The entity in unactive state.
        /// </summary>
        UnActive = 3,
        /// <summary>
        /// The entity verification token has been sent.
        /// </summary>
        InVerification = 4,
        /// <summary>
        /// The request in pending state.
        /// </summary>
        Pending = 5,
        /// <summary>
        ///  The request has been approved.
        /// </summary>
        Approved=6,
        /// <summary>
        /// The request has been rejected/declined.
        /// </summary>
        Declined = 7,
        /// <summary>
        /// The entity validity has been expired.
        /// </summary>
        Expired = 8,
        /// <summary>
        /// The entity already userd/expired state.
        /// </summary>
        Consumed = 9
    }
}
