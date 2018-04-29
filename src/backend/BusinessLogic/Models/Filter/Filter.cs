using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Filter
{
    public abstract class Filter
    {
        public string Keyword { get; set; }
        public int Start { get; set; }
        public int Take { get; set; }
        public string CompanyId { get; set; }
        public string UserId { get; set; }
    }
}
