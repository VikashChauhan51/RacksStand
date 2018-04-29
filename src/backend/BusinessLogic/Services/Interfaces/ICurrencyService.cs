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
    public interface ICurrencyService
    {
        void Add(CurrencyModel item);
        IEnumerable<Currency> Get(CurrencySearchFilter filter);
        Currency GetById(string id);
        void Update(CurrencyModel item);
        void Delete(string id);
    }
}
