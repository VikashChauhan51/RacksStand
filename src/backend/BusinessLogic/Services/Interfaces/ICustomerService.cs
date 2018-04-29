using Domain.Core;
using Models.Core;
using Models.Filter;
using System.Collections.Generic;

namespace Services.Interfaces
{
    public interface ICustomerService
    {
        void Add(CustomerModel item);
        IEnumerable<Customer> Get(CustomerSearchFilter filter);
        Customer GetById(string id);
        void Update(CustomerModel item);
        void Delete(string id);
    }
}
