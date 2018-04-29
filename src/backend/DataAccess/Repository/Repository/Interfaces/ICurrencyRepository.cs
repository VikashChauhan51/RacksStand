using Domain.Core;

namespace Repository.Interfaces
{
    public interface ICurrencyRepository : IRepository<Currency, string>
    {
        void Delete(string id);
    }
}
