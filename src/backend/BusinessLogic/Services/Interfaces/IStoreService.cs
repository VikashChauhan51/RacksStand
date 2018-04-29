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
   public interface IStoreService
    {
        void Add(StoreModel item);
        void AddDefault(StoreModel item);
        IEnumerable<Store> Get(StoreSearchFilter filter);
        Store GetById(string id);
        void Update(StoreModel item);
        void Delete(string id);
    }
}
