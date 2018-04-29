using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core;
using Models.Core;
using Repository.Interfaces;
using Enums.Core;
using Utility.Miscellaneous;
using Models.Filter;
using System.Linq.Expressions;
using Utility.Extension;
namespace Services.Services
{
    public class CustomerService : ICustomerService
    {
        #region Fields

        private readonly ICustomerRepository _customerRepository;
        #endregion
        public CustomerService(ICustomerRepository customerRepository)
        {
            this._customerRepository = customerRepository;

            if (this._customerRepository == null)
                throw new ArgumentNullException("CustomerService.ICustomerRepository");

        }
        public void Add(CustomerModel item)
        {
            try
            {
                if (item == null)
                    throw new ArgumentNullException("item");

                var customer = new Customer
                {
                    Id = item.Id,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Title = item.Title,
                    MiddleName = item.MiddleName,
                    Email = item.Email,
                    Phone = item.Phone,
                    Mobile = item.Mobile,
                    Fax = item.Fax,
                    PanNo = item.PanNo,
                    Website = item.Website,
                    Bio = item.Bio,
                    Company = item.Company,
                    CompanyId = item.CompanyId,
                    CreatedBy = item.CreatedBy,
                    CreatedOn = DateTime.UtcNow,
                    Status = (byte)StatusType.Active
                };
                //add address of customer
                foreach (var entity in item.Addresses ?? new List<CustomerAddressModel>())
                {
                    var address = new CustomerAddress
                    {
                        Id = entity.Id,
                        CustomerId = customer.Id,
                        Street = entity.Street,
                        Email = entity.Email,
                        Phone = entity.Phone,
                        City = entity.City,
                        ZipCode = entity.ZipCode,
                        Fax = entity.Fax,
                        Country = entity.Country,
                        Remark = entity.Remark,
                        State = entity.State,
                        Status = (byte)StatusType.Active
                    };
                    customer.Addresses.Add(address);

                }

                this._customerRepository.Add(customer);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void Delete(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    throw new ArgumentNullException("id");

              var item=  this._customerRepository.GetById(id);
                if (item != null)
                {
                    item.Status = (byte)StatusType.Deleted;
                    this._customerRepository.Update(item);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IEnumerable<Customer> Get(CustomerSearchFilter filter)
        {
            try
            {
                if (filter == null)
                    throw new ArgumentNullException("filter");

                if (string.IsNullOrEmpty(filter.CompanyId))
                    throw new ArgumentNullException("CompanyId");

                if (string.IsNullOrEmpty(filter.Keyword))
                    filter.Keyword = "*";

                if (filter.Take <= 0)
                    filter.Take = 100;

                var status= filter.Status!=null? filter.Status: (byte)StatusType.Active;

                Expression <Func<Customer, bool>> predicate = x =>
                    x.CompanyId == filter.CompanyId && x.Status== status &&
    (filter.Keyword == "*" || x.FirstName.StartsWith(filter.Keyword) || x.LastName.StartsWith(filter.Keyword) || x.MiddleName.StartsWith(filter.Keyword) || x.Company.StartsWith(filter.Keyword));

                return this._customerRepository.Find(predicate).Skip(filter.Start).Take(filter.Take).ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Customer GetById(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    throw new ArgumentNullException("id");

                return this._customerRepository.GetById(id);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void Update(CustomerModel item)
        {
            try
            {
                if (item == null)
                    throw new ArgumentNullException("item");

                var customer = new Customer
                {
                    Id = item.Id,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Title = item.Title,
                    MiddleName = item.MiddleName,
                    Email = item.Email,
                    Phone = item.Phone,
                    Mobile = item.Mobile,
                    Fax = item.Fax,
                    PanNo = item.PanNo,
                    Website = item.Website,
                    Bio = item.Bio,
                    Company = item.Company,
                    CompanyId = item.CompanyId,
                    CreatedBy = item.CreatedBy,
                    CreatedOn = item.CreatedOn,
                    UpdatedOn = DateTime.UtcNow,
                    UpdatedBy = item.UpdatedBy,
                    Status = (byte)StatusType.Active
                };
                //add address of customer
                foreach (var entity in item.Addresses ?? new List<CustomerAddressModel>())
                {
                    var address = new CustomerAddress
                    {
                        Id = entity.Id,
                        CustomerId = customer.Id,
                        Street = entity.Street,
                        Email = entity.Email,
                        Phone = entity.Phone,
                        City = entity.City,
                        ZipCode = entity.ZipCode,
                        Fax = entity.Fax,
                        Country = entity.Country,
                        Remark = entity.Remark,
                        State = entity.State,
                        Status = (byte)StatusType.Active
                    };
                    customer.Addresses.Add(address);

                }

                this._customerRepository.Update(customer);

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
