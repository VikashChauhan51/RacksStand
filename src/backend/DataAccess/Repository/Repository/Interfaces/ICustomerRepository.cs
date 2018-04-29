using Domain.Core;


namespace Repository.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer, string>
    {
      void Add(Customer entity, ActivityLog actionLog);
        void Update(Customer entity, ActivityLog actionLog);
        Customer GetById(string id, bool withAddresses = true);
    }
}
