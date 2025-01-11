// ---------------------------------------
// Email: quickapp@ebenmonney.com
// Templates: www.ebenmonney.com/templates
// (c) 2024 www.ebenmonney.com/mit-license
// ---------------------------------------

using RacksStand.App.Core.Models.Shop;

namespace RacksStand.App.Core.Services.Shop;
public interface ICustomerService
{
    IEnumerable<Customer> GetTopActiveCustomers(int count);
    IEnumerable<Customer> GetAllCustomersData();
}
