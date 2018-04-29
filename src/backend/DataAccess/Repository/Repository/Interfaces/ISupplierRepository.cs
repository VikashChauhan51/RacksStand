using Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
  public  interface ISupplierRepository : IRepository<Supplier, string>
    {
        Supplier GetById(string id, bool withAddresses = true);
    }
}
