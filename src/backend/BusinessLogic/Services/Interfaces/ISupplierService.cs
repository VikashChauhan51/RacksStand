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
   public interface ISupplierService
    {
        void Add(SupplierModel item);
        IEnumerable<Supplier> Get(SupplierSearchFilter filter);
        Supplier GetById(string id);
        void Update(SupplierModel item);
        void Delete(string id);
    }
}
