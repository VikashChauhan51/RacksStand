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
   public interface IInventoryService
    {
        void Add(InventoryModel item);
        IEnumerable<Inventory> Get(InventorySearchFilter filter);
        Inventory GetById(string id);
        void Update(InventoryModel item);
        void Delete(string id);
    }
}
