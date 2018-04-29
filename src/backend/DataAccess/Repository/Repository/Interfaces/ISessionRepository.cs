using Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
   public interface ISessionRepository: IRepository<Session, string>
    {
        /// <summary>
        /// Delete session by id.
        /// </summary>
        /// <param name="id"></param>
        void Delete(string id);
    }
}
