using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enums.Core
{
    public enum TokenType : byte
    {
        /// <summary>
        /// The forgot password token. 
        /// </summary>
        ForgotPassword = 1,
        /// <summary>
        /// The email verification token.
        /// </summary>
        EmailVerification = 2,

    }
}
