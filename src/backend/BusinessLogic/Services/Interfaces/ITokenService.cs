using Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
  public  interface ITokenService
    {
        /// <summary>
        /// This is used to check whether provided token  is valid or not.
        /// </summary>
        /// <param name="token">(string) token.</param>
        /// <returns>(bool and string)Is exist and userId .</returns>
        Tuple<bool, string> IsValid(string token);
        /// <summary>
        /// Delete user all pervious forgot password token and add new one.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>(string) token id.</returns>
        string Add(string userId);
        /// <summary>
        /// Deactivate token after used it.
        /// </summary>
        /// <param name="id"></param>
        void Deactivate(string id);
    }
}
