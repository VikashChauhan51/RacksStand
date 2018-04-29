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
using Utility.Extension;

namespace Services.Services
{
    public class InventoryService : IInventoryService
    {
        #region Fields

        private readonly IInventoryRepository _inventoryRepository;
        #endregion
        #region Ctor

        public InventoryService(IInventoryRepository inventoryRepository)
        {
            this._inventoryRepository = inventoryRepository;

            if (this._inventoryRepository == null)
                throw new ArgumentNullException("InventoryService.IInventoryRepository");

        }
        #endregion
        #region Public
        public void Add(InventoryModel item)
        {
            try
            {
                if (item == null)
                    throw new ArgumentNullException("item");

                var entity = new Inventory
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    RackBoxId = item.RackBoxId,
                    CompanyId = item.CompanyId,
                    CustomerId = item.CustomerId,
                    Index = item.Index,
                    AccessCode = item.AccessCode,
                    CreatedBy = item.CreatedBy,
                    CreatedOn = DateTime.UtcNow,
                    Status = (byte)StatusType.Active
                };
                this._inventoryRepository.Add(entity);
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

                var item = this._inventoryRepository.Get(id);
                if (item != null)
                {
                    item.Status = (byte)StatusType.Deleted;
                    this._inventoryRepository.Update(item);
                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public IEnumerable<Inventory> Get(InventorySearchFilter filter)
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

                Expression<Func<Inventory, bool>> predicate = x =>
                   x.CompanyId == filter.CompanyId &&
   (filter.Keyword == "*" || x.Name.StartsWith(filter.Keyword));

                if (!string.IsNullOrEmpty(filter.RackBoxId))
                    predicate = predicate.And(x => x.RackBoxId == filter.RackBoxId);

                if (!string.IsNullOrEmpty(filter.CustomerId))
                    predicate = predicate.And(x => x.CustomerId == filter.CustomerId);

                return this._inventoryRepository.Find(predicate).Skip(filter.Start).Take(filter.Take).ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public Inventory GetById(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    throw new ArgumentNullException("id");

                return this._inventoryRepository.Get(id);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void Update(InventoryModel item)
        {
            try
            {
                if (item == null)
                    throw new ArgumentNullException("item");

                Expression<Func<Inventory>> entity = () => new Inventory
                {
                    Name = item.Name,
                    Description = item.Description,
                    RackBoxId = item.RackBoxId,
                    Index = item.Index,
                    UpdatedBy = item.UpdatedBy,
                    UpdatedOn = DateTime.UtcNow,
                    Status = item.Status
                };
                this._inventoryRepository.Update(entity, x => x.Id == item.Id);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion
    }
}
