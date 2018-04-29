using Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
   public interface IActionLogService
    {
        /// <summary>
        /// Log appplication activity.
        /// </summary>
        /// <param name="item"></param>
        void Add(ActivityLog item);
    }
}
