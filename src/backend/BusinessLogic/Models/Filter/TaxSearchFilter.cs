using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Filter
{
   public class TaxSearchFilter: Filter
    {
        public bool? Active { get; set; }
        public bool? IsCompound { get; set; }
    }
}
