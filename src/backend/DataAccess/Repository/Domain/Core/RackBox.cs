using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core
{
    [Alias("RackBoxes")]
    public class RackBox
    {
        public string Id { get; set; }
        public string RackId { get; set; }
        public string CompanyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public int? Index { get; set; }
        public byte SecondaryStatus { get; set; }
        public int? CurrentSize { get; set; }
    }
}
