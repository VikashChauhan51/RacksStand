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
using System.Linq.Expressions;
using Enums.Core;
using Services.Core;
using Utility.Extension;
namespace Services.Services
{
    public class RoomService : IRoomService
    {
        #region Fields

        private readonly IRoomRepository _roomRepository;
        #endregion
        public RoomService(IRoomRepository roomRepository)
        {
            this._roomRepository = roomRepository;

            if (this._roomRepository == null)
                throw new ArgumentNullException("RoomService.IRoomRepository");

        }

        public void Add(RoomModel item)
        {

            try
            {
                if (item == null)
                    throw new ArgumentNullException("item");

                var entity = new Room
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    StoreId = item.StoreId,
                    CompanyId = item.CompanyId,
                    CreatedBy = item.CreatedBy,
                    CreatedOn = DateTime.UtcNow,
                    Status = (byte)StatusType.Active,
                    SecondaryStatus = (byte)SecondaryStatusType.Empty
                };
                //add racks of room.
                entity.Racks = RackHalper.GenerateRacks(item.Racks);
                this._roomRepository.Add(entity);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void AddDefault(RoomModel item)
        {

            try
            {
                if (item == null)
                    throw new ArgumentNullException("item");

                var entity = new Room
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    StoreId = item.StoreId,
                    CompanyId = item.CompanyId,
                    CreatedBy = item.CreatedBy,
                    CreatedOn = DateTime.UtcNow,
                    Status = (byte)StatusType.Active,
                    SecondaryStatus = (byte)SecondaryStatusType.Empty
                };
                //add default rack of room.
                entity.Racks.Add(RackHalper.GenerateRack(entity.Id, entity.CompanyId, entity.CreatedBy));
                this._roomRepository.Add(entity);
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

                var item = this._roomRepository.Get(id);
                if (item != null)
                {
                    item.Status = (byte)StatusType.Deleted;
                    this._roomRepository.Update(item);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IEnumerable<Room> Get(RoomSearchFilter filter)
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

                Expression<Func<Room, bool>> predicate = x =>
                   x.CompanyId == filter.CompanyId && x.Status == (byte)StatusType.Active &&
   (filter.Keyword == "*" || x.Name.StartsWith(filter.Keyword));

                if (!string.IsNullOrEmpty(filter.StoreId))
                    predicate=predicate.And(x => x.StoreId == filter.StoreId);

                return this._roomRepository.Find(predicate).Skip(filter.Start).Take(filter.Take).ToList();

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Room GetById(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    throw new ArgumentNullException("id");

                return this._roomRepository.Get(id);

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void Update(RoomModel item)
        {
            try
            {
                if (item == null)
                    throw new ArgumentNullException("item");

                Expression<Func<Room>> entity = () => new Room
                {
                    Name = item.Name,
                    Description = item.Description,
                    UpdatedBy = item.UpdatedBy,
                    UpdatedOn = DateTime.UtcNow,
                    Status = item.Status,
                    SecondaryStatus = item.SecondaryStatus
                };
                this._roomRepository.Update(entity, x => x.Id == item.Id);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
