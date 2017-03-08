using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RacksStand.Utility.Miscellaneous
{
    /// <summary>
    /// This class contains application business logic messages.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Please Enter the Correct {0}.
        /// </summary>
        public const string Incorrect = "Please Enter the Correct {0}.";
        /// <summary>
        /// Invalid Data.
        /// </summary>
        public const string Invalid_Model = "Invalid Data.";
        /// <summary>
        /// Welcome
        /// </summary>
        public const string Login_Success = "Welcome";
        /// <summary>
        /// {0} has been sent successfully.
        /// </summary>
        public const string Sent = "{0} has been sent successfully.";
        /// <summary>
        /// Your account has been locked.Please contact Administrator.
        /// </summary>
        public const string Account_Locked = "Your account has been locked.Please contact Administrator.";
        /// <summary>
        /// Unsupported File.
        /// </summary>
        public const string Wrong_File = "Unsupported File.";
        /// <summary>
        /// Your information has been submitted successfully.
        /// </summary>
        public const string Added = "Your information has been submitted successfully.";
        /// <summary>
        /// Failed to complete process.Please try again later.
        /// </summary>
        public const string Failed = "Failed to complete process.Please try again later.";
        /// <summary>
        /// Something went wrong.Please try again later.
        /// </summary>
        public const string Error = "Something went wrong.Please try again later.";
        //Your information has been updated successfully.
        public const string Updated = "Your information has been updated successfully.";
        /// <summary>
        /// Your information has been deleted successfully.
        /// </summary>
        public const string Deleted = "Your information has been deleted successfully.";
        /// <summary>
        /// Specified {0} doesn't exist.
        /// </summary>
        public const string Not_Exists = "Specified {0} doesn't exist.";
        /// <summary>
        /// The {0} already exists.
        /// </summary>
        public const string Already_Exists = "The {0} already exists.";
        public const string Account_Created = "Your account has been creatd successfully. Please check your email for account verification. ";

    }
}
