using Domain.Core;

namespace Repository.Interfaces
{
    public interface ITaxRepository : IRepository<Tax, string>
    {
        void Delete(string id);
    }
}
