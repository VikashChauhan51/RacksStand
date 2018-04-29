using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RacksStand.WebAPI
{
    /// <summary>
    /// Request response message string.
    /// </summary>
    public class MessageString
    {
        public const string FORGOT_PASSWORD_EMAIL_SUBJECT = "Forgot Password";
        public const string INVALID_REQUEST_PARMS = "Invalid provided request parameters.";
        public const string FORGOT_PASSWORD = "Password reset links has been sent to register email.Please check.";
        public const string CHANGED_PASSWORD = "Password has been changed successfully.";
        public const string LOGGEDIN = "User has been loggedIn successfully.";
        public const string USER_EXISTS = "User already exists.";
        public const string USER_NOT_EXISTS = "User not exists.";
        public const string EMAIL_EXISTS = "Email already exists.";
        public const string EMAIL_NOT_EXISTS = "User not exists.";
        public const string INVALID_TOKEN = "Invalid token.";
    }
}