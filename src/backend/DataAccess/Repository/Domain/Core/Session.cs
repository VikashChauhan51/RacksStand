using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core
{

    [Alias("Session")]
  public  class Session
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string CompanyId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
