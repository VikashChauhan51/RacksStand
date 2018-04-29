using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Filter
{
    public class InventorySearchFilter : Filter
    {
        public string RackBoxId { get; set; }
        public string CustomerId { get; set; }
    }
}
