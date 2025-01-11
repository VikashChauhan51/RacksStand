// ---------------------------------------
// Email: quickapp@ebenmonney.com
// Templates: www.ebenmonney.com/templates
// (c) 2024 www.ebenmonney.com/mit-license
// ---------------------------------------

using Microsoft.EntityFrameworkCore;
using RacksStand.App.Core.Infrastructure;
using RacksStand.App.Core.Models.Shop;

namespace RacksStand.App.Core.Services.Shop;
public class CustomerService(ApplicationDbContext dbContext) : ICustomerService
{
    public IEnumerable<Customer> GetTopActiveCustomers(int count) => throw new NotImplementedException();

    public IEnumerable<Customer> GetAllCustomersData() => dbContext.Customers
            .Include(c => c.Orders).ThenInclude(o => o.OrderDetails).ThenInclude(d => d.Product)
            .Include(c => c.Orders).ThenInclude(o => o.Cashier)
            .AsSingleQuery()
            .OrderBy(c => c.Name)
            .ToList();
}
