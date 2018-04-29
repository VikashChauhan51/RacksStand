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
using Services.Core;

namespace Services.Services
{
    public class StoreService : IStoreService
    {

        #region Fields

        private readonly IStoreRepository _storeRepository;
        #endregion
        public StoreService(IStoreRepository storeRepository)
        {
            this._storeRepository = storeRepository;

            if (this._storeRepository == null)
                throw new ArgumentNullException("StoreService.IStoreRepository");

        } 

        public void Add(StoreModel item)
        {

            try
            {
                if (item == null)
                    throw new ArgumentNullException("item");

                var entity = new Store
                {
                    Id = item.Id,
                    Name = item.Name,
                    Country = item.Country,
                    Street = item.Street,
                    Email = item.Email,
                    Phone = item.Phone,
                    City = item.City,
                    ZipCode = item.ZipCode,
                    Fax = item.Fax,
                    State = item.State,
                    Description = item.Description,
                    CompanyId = item.CompanyId,
                    CreatedBy = item.CreatedBy,
                    CreatedOn = DateTime.UtcNow,
                    Status = (byte)StatusType.Active,
                    SecondaryStatus = (byte)SecondaryStatusType.Empty
                };
                //add rooms of store
                entity.Rooms = RackHalper.GenerateRooms(item.Rooms);
                    this._storeRepository.Add(entity);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void AddDefault(StoreModel item)
        {
            try
            {
                if (item == null)
                    throw new ArgumentNullException("item");

                var entity = new Store
                {
                    Id = item.Id,
                    Name = item.Name,
                    Country = item.Country,
                    Street = item.Street,
                    Email = item.Email,
                    Phone = item.Phone,
                    City = item.City,
                    ZipCode = item.ZipCode,
                    Fax = item.Fax,
                    State = item.State,
                    Description = item.Description,
                    CompanyId = item.CompanyId,
                    CreatedBy = item.CreatedBy,
                    CreatedOn = DateTime.UtcNow,
                    Status = (byte)StatusType.Active,
                    SecondaryStatus = (byte)SecondaryStatusType.Empty
                };
                //add default room of store
                entity.Rooms.Add(RackHalper.GenerateRoom(item.Id, item.CompanyId, item.CreatedBy));
                this._storeRepository.Add(entity);
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

                var item = this._storeRepository.Get(id);
                if (item != null)
                {
                    item.Status = (byte)StatusType.Deleted;
                    this._storeRepository.Update(item);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IEnumerable<Store> Get(StoreSearchFilter filter)
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

                Expression<Func<Store, bool>> predicate = x =>
                   x.CompanyId == filter.CompanyId && x.Status == (byte)StatusType.Active &&
   (filter.Keyword == "*" || x.Name.StartsWith(filter.Keyword) || x.Street.StartsWith(filter.Keyword) || x.City.StartsWith(filter.Keyword) || x.State.StartsWith(filter.Keyword));

                return this._storeRepository.Find(predicate).Skip(filter.Start).Take(filter.Take).ToList();

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Store GetById(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    throw new ArgumentNullException("id");

                return this._storeRepository.Get(id);

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void Update(StoreModel item)
        {
            try
            {
                if (item == null)
                    throw new ArgumentNullException("item");

                Expression<Func<Store>> entity =()=> new Store
                {
                    Name = item.Name,
                    Country = item.Country,
                    Street = item.Street,
                    Email = item.Email,
                    Phone = item.Phone,
                    City = item.City,
                    ZipCode = item.ZipCode,
                    Fax = item.Fax,
                    State = item.State,
                    Status=item.Status,
                    SecondaryStatus=item.SecondaryStatus,
                    Description = item.Description,
                    UpdatedBy=item.UpdatedBy,
                    UpdatedOn= DateTime.UtcNow
                };
                this._storeRepository.Update(entity,x=>x.Id==item.Id);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
         
    }
}
