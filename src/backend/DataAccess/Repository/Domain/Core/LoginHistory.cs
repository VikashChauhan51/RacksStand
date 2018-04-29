using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core
{
    [Alias("LoginHistory")]
    public class LoginHistory
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Browser { get; set; }
        public string Platform { get; set; }
        public string Version { get; set; }
        public string HostName { get; set; }
        public string HostAddress { get; set; }
        public string URI { get; set; }
        public bool? IsMobileDevice { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
