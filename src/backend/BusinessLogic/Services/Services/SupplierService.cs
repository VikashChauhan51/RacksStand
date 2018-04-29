using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core;
using Models.Core;
using Models.Filter;
using Repository.Interfaces;
using Enums.Core;
using System.Linq.Expressions;

namespace Services.Services
{
    public class SupplierService : ISupplierService
    {
        #region Fields

        private readonly ISupplierRepository _supplierRepository;
        #endregion
        public SupplierService(ISupplierRepository supplierRepository)
        {
            this._supplierRepository = supplierRepository;

            if (this._supplierRepository == null)
                throw new ArgumentNullException("SupplierService.ISupplierRepository");

        }
        public void Add(SupplierModel item)
        {
            try
            {
                if (item == null)
                    throw new ArgumentNullException("item");

                var supplier = new Supplier
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
                //add address of supplier
                foreach (var entity in item.Addresses ?? new List<SupplierAddressModel>())
                {
                    var address = new SupplierAddress
                    {
                        Id = entity.Id,
                        SupplierId = supplier.Id,
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
                    supplier.Addresses.Add(address);

                }

                this._supplierRepository.Add(supplier);
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

                var item = this._supplierRepository.GetById(id);
                if (item != null)
                {
                    item.Status = (byte)StatusType.Deleted;
                    this._supplierRepository.Update(item);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IEnumerable<Supplier> Get(SupplierSearchFilter filter)
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

                var status = filter.Status != null ? filter.Status : (byte)StatusType.Active;

                Expression<Func<Supplier, bool>> predicate = x =>
                   x.CompanyId == filter.CompanyId && x.Status == status &&
   (filter.Keyword == "*" || x.FirstName.StartsWith(filter.Keyword) || x.LastName.StartsWith(filter.Keyword) || x.MiddleName.StartsWith(filter.Keyword) || x.Company.StartsWith(filter.Keyword));

                return this._supplierRepository.Find(predicate).Skip(filter.Start).Take(filter.Take).ToList();

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Supplier GetById(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    throw new ArgumentNullException("id");

                return this._supplierRepository.GetById(id);

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void Update(SupplierModel item)
        {
            try
            {
                if (item == null)
                    throw new ArgumentNullException("item");

                var supplier = new Supplier
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
                //add address of supplier
                foreach (var entity in item.Addresses ?? new List<SupplierAddressModel>())
                {
                    var address = new SupplierAddress
                    {
                        Id = entity.Id,
                        SupplierId = supplier.Id,
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
                    supplier.Addresses.Add(address);

                }

                this._supplierRepository.Update(supplier);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
