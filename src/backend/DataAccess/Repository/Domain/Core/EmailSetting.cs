using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core
{
    [Alias("EmailServers")]
    public class EmailSetting
    {
        public byte Id { get; set; }
        public bool EnableSsl { get; set; }
        public string Host { get; set; }
        public bool IsBodyHtml { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public byte Status { get; set; }
    }
}
