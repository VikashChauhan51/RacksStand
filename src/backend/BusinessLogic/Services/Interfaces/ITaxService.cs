using Domain.Core;
using Models.Core;
using Models.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ITaxService
    {
        void Add(TaxModel item);
        IEnumerable<Tax> Get(TaxSearchFilter filter);
        Tax GetById(string id);
        void Update(TaxModel item);
        void Delete(string id);
    }
}
